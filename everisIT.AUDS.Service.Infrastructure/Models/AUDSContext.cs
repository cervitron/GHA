using System;
using everisIT.Fen2.Utils.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace everisIT.AUDS.Service.Infrastructure.Models
{
    public partial class AUDSContext : DbContexWithAudit
    {
        

        public virtual DbSet<AudsAppTag> AudsAppTag { get; set; }
        public virtual DbSet<AudsApplication> AudsApplication { get; set; }
        public virtual DbSet<AudsAudit> AudsAudit { get; set; }
        public virtual DbSet<AudsAuditHco> AudsAuditHco { get; set; }
        public virtual DbSet<AudsDocument> AudsDocument { get; set; }
        public virtual DbSet<AudsGroup> AudsGroup { get; set; }
        public virtual DbSet<AudsRisk> AudsRisk { get; set; }
        public virtual DbSet<AudsState> AudsState { get; set; }
        public virtual DbSet<AudsStateType> AudsStateType { get; set; }
        public virtual DbSet<AudsTag> AudsTag { get; set; }
        public virtual DbSet<AudsType> AudsType { get; set; }
        public virtual DbSet<AudsVulnerability> AudsVulnerability { get; set; }
        public virtual DbSet<AudsVulnerabilityHco> AudsVulnerabilityHco { get; set; }
        public virtual DbSet<AudsAuditResponsible> AudsAuditResponsible { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AudsAppTag>(entity =>
            {
                entity.HasKey(e => new { e.ApplicationId, e.TagId });

                entity.ToTable("auds_app_tag", "AUDS");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AudsAppTag)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_app_tag_auds_application");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.AudsAppTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_app_tag_auds_tag");
            });

            modelBuilder.Entity<AudsApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationId);

                entity.ToTable("auds_application", "AUDS");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.ApplicationDescription)
                    .HasColumnName("application_description")
                    .HasMaxLength(500);

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasColumnName("application_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AudsApplication)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_application_auds_group");
            });

            modelBuilder.Entity<AudsAudit>(entity =>
            {
                entity.HasKey(e => e.AuditId);

                entity.ToTable("auds_audit", "AUDS");

                entity.Property(e => e.AuditId).HasColumnName("audit_id");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.AuditDateEnd)
                    .HasColumnName("audit_date_end")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.AuditDateStart)
                    .HasColumnName("audit_date_start")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.AuditDescription)
                    .HasColumnName("audit_description")
                    .HasMaxLength(500);

                entity.Property(e => e.AuditIsNotificationSent).HasColumnName("audit_isNotificationSent");

                entity.Property(e => e.AuditIsNotificationEndSent).HasColumnName("audit_isNotificationEndSent");

                entity.Property(e => e.AuditResolutor).HasColumnName("audit_resolutor");

                entity.Property(e => e.AuditResponsible).HasColumnName("audit_responsible");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AudsAudit)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_audit_auds_application");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.AudsAudit)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_audit_auds_type");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.AudsAudit)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_audit_auds_state");
            });

            modelBuilder.Entity<AudsAuditHco>(entity =>
            {
                entity.HasKey(e => e.AuditHcoId);

                entity.ToTable("auds_audit_hco", "AUDS");

                entity.Property(e => e.AuditHcoId).HasColumnName("audit_hco_id");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.AuditDateEnd)
                    .HasColumnName("audit_date_end")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.AuditDateStart)
                    .HasColumnName("audit_date_start")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.AuditDescription)
                    .HasColumnName("audit_description")
                    .HasMaxLength(500);

                entity.Property(e => e.AuditId).HasColumnName("audit_id");

                entity.Property(e => e.AuditIsNotificationSent).HasColumnName("audit_isNotificationSent");

                entity.Property(e => e.AuditResolutor).HasColumnName("audit_resolutor");

                entity.Property(e => e.AuditResponsible).HasColumnName("audit_responsible");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AudsAuditHco)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_audit_hco_auds_application");

                entity.HasOne(d => d.Audit)
                    .WithMany(p => p.AudsAuditHco)
                    .HasForeignKey(d => d.AuditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_audit_hco_auds_audit");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.AudsAuditHco)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_audit_hco_auds_type");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.AudsAuditHco)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_audit_hco_auds_state");
            });

            modelBuilder.Entity<AudsDocument>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.ToTable("auds_document", "AUDS");

                entity.Property(e => e.DocumentId).HasColumnName("document_id");

                entity.Property(e => e.AuditId).HasColumnName("audit_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DocumentDateUpload)
                    .HasColumnName("document_date_upload")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DocumentDescription)
                    .IsRequired()
                    .HasColumnName("document_description")
                    .HasMaxLength(1000);

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasColumnName("document_name")
                    .HasMaxLength(50);

                entity.Property(e => e.DocumentUserUpload).HasColumnName("document_user_upload");

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");

                entity.HasOne(d => d.Audit)
                    .WithMany(p => p.AudsDocument)
                    .HasForeignKey(d => d.AuditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_document_auds_audit");
            });

            modelBuilder.Entity<AudsGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("auds_group", "AUDS");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("group_name")
                    .HasMaxLength(50);

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");
            });

            modelBuilder.Entity<AudsRisk>(entity =>
            {
                entity.HasKey(e => e.RiskId);

                entity.ToTable("auds_risk", "AUDS");

                entity.Property(e => e.RiskId).HasColumnName("risk_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.HowManyDaysUntilNotification).HasColumnName("how_many_days_until_notification");

                entity.Property(e => e.RiskName)
                    .IsRequired()
                    .HasColumnName("risk_name")
                    .HasMaxLength(50);

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");
            });

            modelBuilder.Entity<AudsState>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.ToTable("auds_state", "AUDS");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasColumnName("state_name")
                    .HasMaxLength(50);

                entity.Property(e => e.StateType).HasColumnName("state_type");

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");

                entity.HasOne(d => d.StateTypeNavigation)
                    .WithMany(p => p.AudsState)
                    .HasForeignKey(d => d.StateType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_state_auds_state_type");
            });

            modelBuilder.Entity<AudsStateType>(entity =>
            {
                entity.HasKey(e => e.StateTypeId);

                entity.ToTable("auds_state_type", "AUDS");

                entity.Property(e => e.StateTypeId).HasColumnName("state_type_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.StateTypeName)
                    .IsRequired()
                    .HasColumnName("state_type_name")
                    .HasMaxLength(50);

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");
            });

            modelBuilder.Entity<AudsTag>(entity =>
            {
                entity.HasKey(e => e.TagId);

                entity.ToTable("auds_tag", "AUDS");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasColumnName("tag_name")
                    .HasMaxLength(50);

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");
            });

            modelBuilder.Entity<AudsType>(entity =>
            {
                entity.HasKey(e => e.IdType);

                entity.ToTable("auds_type", "AUDS");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.NameType)
                    .IsRequired()
                    .HasColumnName("name_type")
                    .HasMaxLength(50);

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");
            });

            modelBuilder.Entity<AudsVulnerability>(entity =>
            {
                entity.HasKey(e => e.VulnerabilityId);

                entity.ToTable("auds_vulnerability", "AUDS");

                entity.Property(e => e.VulnerabilityId).HasColumnName("vulnerability_id");

                entity.Property(e => e.AuditId).HasColumnName("audit_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.RiskId).HasColumnName("risk_id");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.Property(e => e.StateIdResolution).HasColumnName("state_id_resolution");

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");

                entity.Property(e => e.VulnerabilityActionPlans)
                    .HasColumnName("vulnerability_action_plans")
                    .HasMaxLength(1000);

                entity.Property(e => e.VulnerabilityDateCommitment)
                    .HasColumnName("vulnerability_date_commitment")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.VulnerabilityDateIdentification)
                    .HasColumnName("vulnerability_date_identification")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.VulnerabilityDateMitigation)
                    .HasColumnName("vulnerability_date_mitigation")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.VulnerabilityDateResolution)
                    .HasColumnName("vulnerability_date_resolution")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.VulnerabilityDescription)
                    .IsRequired()
                    .HasColumnName("vulnerability_description")
                    .HasMaxLength(1000);

                entity.Property(e => e.VulnerabilityIdControl)
                    .IsRequired()
                    .HasColumnName("vulnerability_id_control")
                    .HasMaxLength(50);

                entity.Property(e => e.VulnerabilityResponsible).HasColumnName("vulnerability_responsible");

                entity.HasOne(d => d.Audit)
                    .WithMany(p => p.AudsVulnerability)
                    .HasForeignKey(d => d.AuditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_vulnerability_auds_audit");

                entity.HasOne(d => d.Risk)
                    .WithMany(p => p.AudsVulnerability)
                    .HasForeignKey(d => d.RiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_vulnerability_auds_risk");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.AudsVulnerabilityState)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_vulnerability_auds_state");

                entity.HasOne(d => d.StateIdResolutionNavigation)
                    .WithMany(p => p.AudsVulnerabilityStateIdResolutionNavigation)
                    .HasForeignKey(d => d.StateIdResolution)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_vulnerability_auds_state_Resolution");
            });

            modelBuilder.Entity<AudsVulnerabilityHco>(entity =>
            {
                entity.HasKey(e => e.VulnerabilityHcoId);

                entity.ToTable("auds_vulnerability_hco", "AUDS");

                entity.Property(e => e.VulnerabilityHcoId).HasColumnName("vulnerability_hco_id");

                entity.Property(e => e.AuditId).HasColumnName("audit_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.RiskId).HasColumnName("risk_id");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.Property(e => e.StateIdResolution).HasColumnName("state_id_resolution");

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");

                entity.Property(e => e.VulnerabilityActionPlans)
                    .HasColumnName("vulnerability_action_plans")
                    .HasMaxLength(1000);

                entity.Property(e => e.VulnerabilityDateCommitment)
                    .HasColumnName("vulnerability_date_commitment")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.VulnerabilityDateIdentification)
                    .HasColumnName("vulnerability_date_identification")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.VulnerabilityDateMitigation)
                    .HasColumnName("vulnerability_date_mitigation")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.VulnerabilityDateResolution)
                    .HasColumnName("vulnerability_date_resolution")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.VulnerabilityDescription)
                    .IsRequired()
                    .HasColumnName("vulnerability_description")
                    .HasMaxLength(1000);

                entity.Property(e => e.VulnerabilityId).HasColumnName("vulnerability_id");

                entity.Property(e => e.VulnerabilityIdControl)
                    .IsRequired()
                    .HasColumnName("vulnerability_id_control")
                    .HasMaxLength(50);

                entity.Property(e => e.VulnerabilityRemarks)
                    .IsRequired()
                    .HasColumnName("vulnerability_remarks")
                    .HasMaxLength(500);

                entity.Property(e => e.VulnerabilityResponsible).HasColumnName("vulnerability_responsible");

                entity.HasOne(d => d.Audit)
                    .WithMany(p => p.AudsVulnerabilityHco)
                    .HasForeignKey(d => d.AuditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_vulnerability_hco_auds_audit");

                entity.HasOne(d => d.Risk)
                    .WithMany(p => p.AudsVulnerabilityHco)
                    .HasForeignKey(d => d.RiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_vulnerability_hco_auds_risk");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.AudsVulnerabilityHcoState)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_vulnerability_hco_auds_state");

                entity.HasOne(d => d.StateIdResolutionNavigation)
                    .WithMany(p => p.AudsVulnerabilityHcoStateIdResolutionNavigation)
                    .HasForeignKey(d => d.StateIdResolution)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_vulnerability_hco_auds_state_Resolution");

                entity.HasOne(d => d.Vulnerability)
                    .WithMany(p => p.AudsVulnerabilityHco)
                    .HasForeignKey(d => d.VulnerabilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_vulnerability_hco_auds_vulnerability");
            });

            modelBuilder.Entity<AudsAuditResponsible>(entity =>
            {
                entity.ToTable("auds_audit_responsible", "AUDS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuditId).HasColumnName("audit_id");

                entity.Property(e => e.CodeStatus).HasColumnName("code_status");

                entity.Property(e => e.DateLastUpdateRegister)
                    .HasColumnName("date_last_update_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.DateNewRegister)
                    .HasColumnName("date_new_register")
                    .HasColumnType("datetime2(3)");

                entity.Property(e => e.IdEmployee).HasColumnName("id_employee");

                entity.Property(e => e.UserLastUpdateRegister).HasColumnName("user_last_update_register");

                entity.Property(e => e.UserNewRegister).HasColumnName("user_new_register");

                entity.HasOne(d => d.Audit)
                    .WithMany(p => p.AudsAuditResponsible)
                    .HasForeignKey(d => d.AuditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_auds_audit_responsible_auds_audit");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
