using Dacha.Dal.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Dacha.Dal.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser> 
    {
        public static string ConnectionStringName => ConfigurationManager.AppSettings["ConnectionStringName"] ?? "DachaContext";

        static ApplicationContext()
        {          
           Database.SetInitializer<ApplicationContext>(new IdentityDbInit());
          //var db=  new ApplicationContext();//
           // db.Database.CreateIfNotExists();
        }
        public ApplicationContext() : base(ConnectionStringName)
        {
            
        }

        public ApplicationContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Companionship> Companionships { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberDoc> MemberDocs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
              modelBuilder.Configurations.Add(new CompanionshipConfiguration());
              modelBuilder.Configurations.Add(new MemberConfiguration());
              modelBuilder.Configurations.Add(new MemberDocConfiguration());
              base.OnModelCreating(modelBuilder);
        }
    }


    public class CompanionshipConfiguration : EntityTypeConfiguration<Companionship>
    {
        public CompanionshipConfiguration()
        {
            this.ToTable("CompanionShip", "dbo").HasKey<Guid>(k => k.Id);           
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.RowVersion).IsRowVersion();
            this.Property(c => c.Name).IsRequired().HasMaxLength(128);
            this.Property(c => c.Address).HasMaxLength(256);
            this.Property(c => c.Registration).HasMaxLength(256);
            this.Property(c => c.Chairman).HasMaxLength(256);
            this.Property(c => c.Membership).HasMaxLength(50);
            this.Property(c => c.Addition).HasMaxLength(256);           
        }
    }

    public class MemberConfiguration : EntityTypeConfiguration<Member>
    {
        public MemberConfiguration()
        {
            this.ToTable("Members", "dbo").HasKey(k => k.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.RowVersion).IsRowVersion();
            this.Property(c => c.Owner).IsRequired().HasMaxLength(128);
            this.Property(c => c.OwnerAddress).HasMaxLength(256);
            this.Property(c => c.PlotNumber).HasMaxLength(20);
            this.Property(c => c.PlotAddress).HasMaxLength(128);
            this.Property(c => c.PlotSquare).HasMaxLength(50);
            this.Property(c => c.Addition).HasMaxLength(256);            
            this.HasOptional<Companionship>(c => c.Companionship).WithMany(m => m.Members).HasForeignKey(x => x.CompanionshipId).WillCascadeOnDelete(false);      
        }
    }

    public class MemberDocConfiguration : EntityTypeConfiguration<MemberDoc>
    {
        public MemberDocConfiguration()
        {
            this.ToTable("MemberDocs", "dbo").HasKey(k => k.Id);//  .MemberDocId);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.RowVersion).IsRowVersion();
            this.Property(c => c.NameDoc).IsRequired().HasMaxLength(128);
            this.Property(c => c.Description).HasMaxLength(256);          
            this.HasOptional<Member>(c => c.Member).WithMany(m => m.MemberDocs).HasForeignKey(x => x.MemberId).WillCascadeOnDelete(false);
        }
    }
}




