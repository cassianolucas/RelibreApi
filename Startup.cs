using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Repositories;
using RelibreApi.Services;
using RelibreApi.Utils;

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
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = new Setting();
            Configuration.GetSection(Constants.Configuration).Bind(settings);

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
                x.SerializerSettings.Culture = System.Globalization.CultureInfo.CurrentCulture;
                x.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                x.SerializerSettings.DateFormatString = Constants.FormatDateTimeDefault;
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = "pt-BR";
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures)
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();

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
                            // "Erro interno!"
                            // var mensagemErro = new
                            // {
                            //     mensagem = Constants.MessageExceptionDefault,
                            //     status = 500
                            // };

                            Util.ReturnException((Exception)exception);

                            await context.Response.WriteAsync(
                                Util.ReturnException((Exception)exception).ToString())
                                .ConfigureAwait(false);
                                                        
                            // await context.Response.WriteAsync(mensagemErro.ToString()).ConfigureAwait(false);
                        }
                    });
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // endpoints.MapControllerRoute(
                //     name: "default",
                //     pattern: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
