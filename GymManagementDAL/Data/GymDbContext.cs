using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Data
{
    internal class GymDbContext : DbContext
    {
        public GymDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=GymManagementGroup03;Trusted_Connection=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Trainer>()
                        .Property(M => M.CreatedAt)
                        .HasColumnName("JoinDate")
                        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Trainer>()
                        .Property(T => T.CreatedAt)
                        .HasColumnName("HireDate")
                        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<HealthRecord>()
                        .Property(T => T.UpdatedAt)
                        .HasColumnName("LastUpdate")
                        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Session>(E =>
            {
                E.ToTable(S => { S.HasCheckConstraint("CK_Session_EndDate", "[EndDate] > [StartDate]"); });
            });

            // Relationships

            // 1-1 Relationship Between Member And HealthRecord
            modelBuilder.Entity<HealthRecord>().ToTable("Members")
                                               .HasKey(x => x.Id);
            modelBuilder.Entity<HealthRecord>().Ignore(x => x.CreatedAt);
            modelBuilder.Entity<HealthRecord>().Ignore(x => x.UpdatedAt);


            modelBuilder.Entity<Member>()   
                        .HasOne<HealthRecord>(M => M.HealthRecord)
                        .WithOne(H => H.Member)
                        .HasForeignKey<HealthRecord>(H => H.Id);


            // 1-M Relationship Between Category And Session
            modelBuilder.Entity<Category>()
                        .HasMany(C => C.Sessions)
                        .WithOne(S => S.Category)
                        .HasForeignKey(S => S.CategoryId)
                        .OnDelete(DeleteBehavior.Restrict);

            // 1-M Relationship Between Trainer And Session
            modelBuilder.Entity<Trainer>()
                        .HasMany(T => T.Sessions)
                        .WithOne(S => S.Trainer)
                        .HasForeignKey(S => S.TrainerId)
                        .OnDelete(DeleteBehavior.Restrict);

            // M-M Relationship Between Member And Plan
            // 1-M Relationship Between Member And Membership
            modelBuilder.Entity<Member>()
                        .HasMany(M => M.Memberships)
                        .WithOne(MS => MS.Member)
                        .HasForeignKey(MS => MS.MemberId)
                        .OnDelete(DeleteBehavior.Restrict);

            // 1-M Relationship Between Plan And Membership
            modelBuilder.Entity<Plan>()
                       .HasMany(P => P.Memberships)
                       .WithOne(MS => MS.Plan)
                       .HasForeignKey(MS => MS.PlanId)
                       .OnDelete(DeleteBehavior.Restrict);
            // Configurations Of Membership
            modelBuilder.Entity<Membership>()
                        .Property(T => T.CreatedAt)
                        .HasColumnName("StartDate")
                        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Membership>().Ignore(M => M.Id);
            modelBuilder.Entity<Membership>().HasKey(M => new { M.MemberId, M.PlanId });

            // M-M Relationship Between Member And Session
            // 1-M Relationship Between Member And MemberSession

            modelBuilder.Entity<Member>()
                        .HasMany(M => M.MemberSessions)
                        .WithOne(MS => MS.Member)
                        .HasForeignKey(MS => MS.MemberId)
                        .OnDelete(DeleteBehavior.Restrict);

            // 1-M Relationship Between Session And MemberSession
            modelBuilder.Entity<Session>()
                     .HasMany(S => S.MemberSessions)
                     .WithOne(MS => MS.Session)
                     .HasForeignKey(MS => MS.SessionId)
                     .OnDelete(DeleteBehavior.Restrict);

            // Configurations Of MemberSession
            modelBuilder.Entity<MemberSession>().Ignore(M => M.Id);
            modelBuilder.Entity<MemberSession>().HasKey(M => new { M.MemberId, M.SessionId });

            modelBuilder.Entity<MemberSession>()
                      .Property(T => T.CreatedAt)
                      .HasColumnName("BookingDate")
                      .HasDefaultValueSql("GETDATE()");

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }


    }
}

// IMemberRepository
// ITrainerRepository
// MemberRepository  TrainerRepository
