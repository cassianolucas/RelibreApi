using Microsoft.EntityFrameworkCore;
using RelibreApi.Maps;
using RelibreApi.Models;

namespace RelibreApi.Data
{
    public class RelibreContext: DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<AccessProfile> AccessProfile { get; set; }
        public DbSet<Address> Addresse { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Type> Type { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<LibraryBook> LibraryBook { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryBook> CategoryBook { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<AuthorBook> AuthorBook { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<LibraryBookType> LibraryBookType { get; set; }
        public DbSet<EmailVerification> EmailVerification { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationPerson> NotificationPerson { get; set; }
        public DbSet<ContactBook> ContactBook { get; set; }

        public RelibreContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new AccessProfileMap());
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new LibraryMap());
            modelBuilder.ApplyConfiguration(new PhoneMap());
            modelBuilder.ApplyConfiguration(new ProfileMap());
            modelBuilder.ApplyConfiguration(new TypeMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ImageMap());
            modelBuilder.ApplyConfiguration(new LibraryBookMap());
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CategoryBookMap());
            modelBuilder.ApplyConfiguration(new AuthorMap());
            modelBuilder.ApplyConfiguration(new AuthorBookMap());
            modelBuilder.ApplyConfiguration(new ContactMap());
            modelBuilder.ApplyConfiguration(new LibraryBookTypeMap());
            modelBuilder.ApplyConfiguration(new EmailVerificationMap());
            modelBuilder.ApplyConfiguration(new NotificationMap());            
            modelBuilder.ApplyConfiguration(new NotificationPersonMap());
            modelBuilder.ApplyConfiguration(new ContactBookMap());
            
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}