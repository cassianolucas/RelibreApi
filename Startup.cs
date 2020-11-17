using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Repositories;
using RelibreApi.Services;
using RelibreApi.Utils;
using static RelibreApi.Utils.Constants;

namespace RelibreApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;

            // Util.SendEmailAsync(configuration, "teste", 
            //     "lucas_cassiano@live.com", "Lucas", HtmlEmailType.NewAccount);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = new Setting();
            Configuration.GetSection(Constants.Configuration).Bind(settings);

            services.AddCors(x =>
            {
                x.AddPolicy("policy", x =>
                {
                    x.AllowAnyHeader();
                    x.AllowAnyMethod();
                    x.AllowAnyOrigin();
                });
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;

                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
                };

                x.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Headers.ContainsKey("Authorization"))
                        {
                            var accessToken = context.Request.Headers["Authorization"].ToString();
                            context.Token = accessToken.Replace("Bearer", "").Trim();
                        }

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = async context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = Constants.DefaultContentType;

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            Message = Constants.MessageExceptionNotAuthorize
                        }));
                    }
                };
            });

            services.AddDbContext<RelibreContext>(x =>
                        x.UseNpgsql(settings.ConnectionString));

            services.AddAuthorization(x => new CustomPolicies(x));
            services.AddTransient<IAuthorizationHandler, CustomRequirementHandler>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUser, UserRepository>();
            services.AddTransient<IProfile, ProfileRepository>();
            services.AddTransient<IType, TypeRepository>();
            services.AddTransient<ILibrary, LibraryRepository>();
            services.AddTransient<IBook, BookRepository>();
            services.AddTransient<ICategory, CategoryRepository>();
            services.AddTransient<IAuthor, AuthorRepository>();
            services.AddTransient<ILibraryBook, LibraryBookRepository>();
            services.AddTransient<IContact, ContactRepository>();
            services.AddTransient<IEmailVerification, EmailVerificationRepository>();
            services.AddTransient<INotification, NotificationRepository>();
            services.AddTransient<INotificationPerson, NotificationPersonRepository>();
            services.AddTransient<ISubscription, SubscriptionRepository>();

            services.AddMvc(x =>
            {
                var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                defaultAuthBuilder.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);

                var defaultAuthPolicy = defaultAuthBuilder
                    .RequireAuthenticatedUser()
                    .Build();

                x.Filters.Add(new AuthorizeFilter(defaultAuthPolicy));
            })
            .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.Formatting = Formatting.None;
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                // x.SerializerSettings.Culture = System.Globalization.CultureInfo.CurrentCulture;
                // x.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                // x.SerializerSettings.DateFormatString = Constants.FormatDateTimeDefault;
            })
            .ConfigureApiBehaviorOptions(x =>
            {
                x.InvalidModelStateResponseFactory = context =>
                {
                    var result = new BadRequestObjectResult(context.ModelState);

                    result.ContentTypes.Add(MediaTypeNames.Application.Json);

                    return result;
                };
            });

            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Relibre Api",
                        Version = "v1",
                        Description = "Projeto Relibre API REST criada com o ASP.NET Core",
                        Contact = new OpenApiContact
                        {
                            Email = "projeto.relibre@gmail.com",
                            Url = new Uri("https://relibre.herokuapp.com/")
                        }
                    });

                x.ResolveConflictingActions(x => x.First());

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;

                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                //Caso exista arquivo então adiciona-lo
                if (File.Exists(caminhoXmlDoc))
                {
                    x.IncludeXmlComments(caminhoXmlDoc);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RelibreContext context)
        {
            // context.Database.Migrate();

            // var supportedCultures = "pt-BR";
            // var localizationOptions = new RequestLocalizationOptions()
            //     .SetDefaultCulture(supportedCultures)
            //     .AddSupportedCultures(supportedCultures)
            //     .AddSupportedUICultures(supportedCultures);

            // app.UseRequestLocalization(localizationOptions);

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Relibre Api");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("policy");

            app.UseExceptionHandler(x =>
            {
                x.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = Constants.DefaultContentType;
                        var exception = context.Features.Get<IExceptionHandlerFeature>();

                        if (exception != null)
                        {                            
                            await context.Response.WriteAsync(
                                Util.ReturnException((Exception)exception).ToString())
                                .ConfigureAwait(false);
                        }
                    });
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
