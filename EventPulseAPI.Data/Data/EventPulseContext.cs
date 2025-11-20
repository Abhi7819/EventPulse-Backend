using EventPulseAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace EventPulseAPI.Data.Data
{
    public class EventPulseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public EventPulseContext(DbContextOptions<EventPulseContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public EventPulseContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString;

                if (_configuration != null)
                {
                    connectionString = _configuration.GetConnectionString("DefaultConnection");
                }
                else
                {
                    //var config = new ConfigurationBuilder()
                    //    .SetBasePath(AppContext.BaseDirectory)
                    //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    //    .Build();
                    //connectionString = config.GetConnectionString("DefaultConnection");
                    connectionString = _configuration.GetConnectionString("DefaultConnection");
                }

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(CreateSoftDeleteFilter(entityType.ClrType));
                }
            }

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<int>();

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Owner)
                .WithMany()
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Event)
                .WithMany(e => e.Sessions)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Session)
                .WithMany(s => s.Feedbacks)
                .HasForeignKey(f => f.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Attendee)
                .WithMany()
                .HasForeignKey(f => f.AttendeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static LambdaExpression CreateSoftDeleteFilter(Type type)
        {
            var parameter = Expression.Parameter(type, "e");
            var prop = Expression.Property(parameter, "IsDeleted");
            var condition = Expression.Equal(prop, Expression.Constant(false));
            return Expression.Lambda(condition, parameter);
        }
    }
}
