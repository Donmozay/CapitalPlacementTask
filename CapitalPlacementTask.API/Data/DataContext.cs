using CapitalPlacementTask.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask.API.Data
{
    public class DataContext : IdentityUserContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ApplicationModel> Applications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToContainer("User").HasPartitionKey(x => x.Id);
            modelBuilder.Entity<ApplicationUser>()
            .Property(e => e.ConcurrencyStamp)
            .IsETagConcurrency();

            modelBuilder.Entity<Question>().ToContainer("Questions")
           .HasDiscriminator<string>("QuestionType")
           .HasValue<ParagraphQuestion>("Paragraph")
           .HasValue<YesNoQuestion>("YesNo")
           .HasValue<DropdownQuestion>("Dropdown")
           .HasValue<MultipleChoiceQuestion>("MultipleChoice")
           .HasValue<DateQuestion>("Date")
           .HasValue<NumberQuestion>("Number");

            modelBuilder.Entity<ApplicationModel>().ToContainer("Applications").HasPartitionKey(x => x.Id);
        }
    }

}

