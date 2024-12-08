using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MigrationDemo.Models;
namespace MigrationDemo.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<SalesPipeline> SalesPipelines { get; set; }
        public DbSet<CommunicationHistory> CommunicationHistories { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Oppurtunity> Opportunities { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationPreference> NotificationPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Lead>()
                .HasKey(l => l.LeadId);

            modelBuilder.Entity<Lead>()
                .HasOne<User>() 
                .WithMany()
                .HasForeignKey(l => l.AssignedTo)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SalesPipeline>()
                .HasKey(sp => sp.PipelineId);

            modelBuilder.Entity<SalesPipeline>()
                .HasOne<Lead>() 
                .WithMany()
                .HasForeignKey(sp => sp.LeadId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<CommunicationHistory>()
                .HasKey(ch => ch.InteractionId);

            modelBuilder.Entity<CommunicationHistory>()
                .HasOne<Customer>() 
                .WithMany()
                .HasForeignKey(ch => ch.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tasks>()
                .HasKey(t => t.TaskId);

            modelBuilder.Entity<Tasks>()
                .HasOne<Customer>() 
                .WithMany()
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tasks>()
                .HasOne<User>() 
                .WithMany()
                .HasForeignKey(t => t.AssignedTo)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Oppurtunity>()
            .HasKey(o => o.OpportunityId);

            //modelBuilder.Entity<Oppurtunity>()
            //    .HasOne<Customer>() 
            //    .WithMany()
            //    .HasForeignKey(o => o.CustomerId)
            //.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Oppurtunity>()
                .HasOne<User>() 
                .WithMany()
                .HasForeignKey(o => o.AccountManagerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupportTicket>()
                .HasKey(st => st.TicketId);

            modelBuilder.Entity<SupportTicket>()
                .HasOne<Customer>() 
                .WithMany()
                .HasForeignKey(st => st.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupportTicket>()
                .HasOne<User>() 
                .WithMany()
                .HasForeignKey(st => st.AssignedTo)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Campaign>()
                .HasKey(c => c.CampaignId);

            modelBuilder.Entity<Campaign>()
                .HasOne<User>() 
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Report>()
                .HasKey(r => r.ReportId);

            modelBuilder.Entity<Notification>()
                .HasKey(n => n.NotificationId);

            modelBuilder.Entity<Notification>()
                .HasOne<User>() 
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NotificationPreference>()
                .HasKey(np => np.PreferenceId);

            modelBuilder.Entity<NotificationPreference>()
                .HasOne<User>() 
                .WithMany()
                .HasForeignKey(np => np.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
