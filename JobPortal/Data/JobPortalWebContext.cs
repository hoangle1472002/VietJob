using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JobPortal.Models
{
    public partial class JobPortalWebContext : DbContext
    {
        public JobPortalWebContext()
        {
        }

        public JobPortalWebContext(DbContextOptions<JobPortalWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<EducationDetail> EducationDetails { get; set; } = null!;
        public virtual DbSet<ExperienceDetail> ExperienceDetails { get; set; } = null!;
        public virtual DbSet<JobPost> JobPosts { get; set; } = null!;
        public virtual DbSet<JobPostActivity> JobPostActivities { get; set; } = null!;
        public virtual DbSet<JobType> JobTypes { get; set; } = null!;
        public virtual DbSet<SeekerProfile> SeekerProfiles { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
         //       optionsBuilder.UseSqlServer("Server=DESKTOP-6KQ4CLP\\SQLEXPRESS;Database=JobPortalWeb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompanyLogoUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("company_logo_url");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("company_name");

                entity.Property(e => e.CompanyWebsiteUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("company_website_url");

                entity.Property(e => e.EstablishmentDate)
                    .HasColumnType("date")
                    .HasColumnName("establishment_date");

                entity.Property(e => e.ProfileDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("profile_description");
            });

            modelBuilder.Entity<EducationDetail>(entity =>
            {
                entity.HasKey(e => new { e.UserAccountId, e.CertificateDegreeName, e.Major })
                    .HasName("education_detail_pk");

                entity.ToTable("EducationDetail");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.Property(e => e.CertificateDegreeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("certificate_degree_name");

                entity.Property(e => e.Major)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("major");

                entity.Property(e => e.InstituteUniversityName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Institute_university_name");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.EducationDetails)
                    .HasForeignKey(d => d.UserAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("educ_dtl_seeker_profile");
            });

            modelBuilder.Entity<ExperienceDetail>(entity =>
            {
                entity.HasKey(e => new { e.UserAccountId, e.StartDate, e.EndDate })
                    .HasName("experience_detail_pk");

                entity.ToTable("ExperienceDetail");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("company_name");

                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("job_title");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.ExperienceDetails)
                    .HasForeignKey(d => d.UserAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("exp_dtl_seeker_profile");
            });

            modelBuilder.Entity<JobPost>(entity =>
            {
                entity.ToTable("JobPost");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("created_date");

                entity.Property(e => e.JobDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("job_description");

                entity.Property(e => e.JobLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("job_location");

                entity.Property(e => e.JobTypeId).HasColumnName("job_type_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.JobPosts)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_post_company");

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.JobPosts)
                    .HasForeignKey(d => d.JobTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_post_job_type");
            });

            modelBuilder.Entity<JobPostActivity>(entity =>
            {
                entity.HasKey(e => new { e.UserAccountId, e.JobPostId })
                    .HasName("job_post_activity_pk");

                entity.ToTable("JobPostActivity");

                entity.Property(e => e.UserAccountId).HasColumnName("user_account_id");

                entity.Property(e => e.JobPostId).HasColumnName("job_post_id");

                entity.Property(e => e.ApplyDate)
                    .HasColumnType("date")
                    .HasColumnName("apply_date");

                entity.HasOne(d => d.JobPost)
                    .WithMany(p => p.JobPostActivities)
                    .HasForeignKey(d => d.JobPostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_post_activity_job_post");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.JobPostActivities)
                    .HasForeignKey(d => d.UserAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_post_act_user_register");
            });

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.ToTable("JobType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.JobType1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("job_type");
            });

            modelBuilder.Entity<SeekerProfile>(entity =>
            {
                entity.HasKey(e => e.UserAccountId)
                    .HasName("seeker_profile_pk");

                entity.ToTable("SeekerProfile");

                entity.Property(e => e.UserAccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_account_id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.HasOne(d => d.UserAccount)
                    .WithOne(p => p.SeekerProfile)
                    .HasForeignKey<SeekerProfile>(d => d.UserAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("seeker_profile_user_register");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("contact_number");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("gender")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("date")
                    .HasColumnName("registration_date");

                entity.Property(e => e.UserImageUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("user_image_url");

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_register_user_type");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.UserTypeName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("user_type_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
