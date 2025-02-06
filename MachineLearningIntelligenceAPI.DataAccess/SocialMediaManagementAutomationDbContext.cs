using MachineLearningIntelligenceAPI.DomainModels;
using MachineLearningIntelligenceAPI.DataAccess.Daos;
using MachineLearningIntelligenceAPI.Common.StringConstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MachineLearningIntelligenceAPI.DataAccess
{
    public class SocialMediaManagementAutomationDbContext : DbContext
    {
        public DbSet<AccountAutomation> AccountAutomation { get; set; }
        public DbSet<AccountAutomationType> AccountAutomationType { get; set; }
        public DbSet<UserAccountPermission> UserAccountPermission { get; set; }

        private RequestSessionInformation RequestSessionInformation { get; set; }
        public SocialMediaManagementAutomationDbContext(RequestSessionInformation requestSessionInformation, DbContextOptions<SocialMediaManagementAutomationDbContext> context) : base(context)
        {
            RequestSessionInformation = requestSessionInformation;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO MAKE SURE THIS IS SETUP RIGHT BUILD OUT CONSTRAINTS AND EVERYTHING, everything from the db script
            // https://learn.microsoft.com/en-us/ef/core/modeling/table-splitting
            modelBuilder
                .Entity<UserAccountPermission>(
                ob =>
                {
                    ob.ToTable(DaoUserAccountPermissionStrings.Table);
                    ob.Property(uap => uap.Id).HasColumnName(DaoUserAccountPermissionStrings.Id)
                        .HasColumnType("varchar(36)")
                        .HasConversion(
                                v => v,  // convert to string when sending to database
                                v => new Guid(v.ToString()));   // convert when retrieving from database
                    ob.Property(uap => uap.PermissionName).HasColumnName(DaoUserAccountPermissionStrings.PermissionName);
                    ob.Property(uap => uap.PermissionTypeId).HasColumnName(DaoUserAccountPermissionStrings.PermissionType);
                    ob.Property(uap => uap.UserAccountId).HasColumnName(DaoUserAccountPermissionStrings.UserAccountId)
                        .HasColumnType("varchar(36)");
                    // conversion example from https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations
                    ob.Property(uap => uap.Created).HasColumnName(DaoEntityBaseConstants.Created);
                    ob.Property(uap => uap.CreatedBy).HasColumnName(DaoEntityBaseConstants.CreatedBy);
                    ob.Property(uap => uap.Modified).HasColumnName(DaoEntityBaseConstants.Modified);
                    ob.Property(uap => uap.ModifiedBy).HasColumnName(DaoEntityBaseConstants.ModifiedBy);
                })
                .Entity<AccountAutomation>(
                ob =>
                {
                    ob.ToTable(DaoAccountAutomationStrings.Table);
                    ob.Property(aa => aa.Id).HasColumnName(DaoAccountAutomationStrings.Id)
                        .HasColumnType("varchar(36)")
                        .HasConversion(
                                v => v,  // convert to string when sending to database
                                v => new Guid(v.ToString()));   // convert when retrieving from database
                    ob.Property(aa => aa.UserAccountId).HasColumnName(DaoAccountAutomationStrings.UserAccountId);
                    ob.Property(aa => aa.UsernameOrEmailEncrypted).HasColumnName(DaoAccountAutomationStrings.UsernameOrEmailEncrypted);
                    ob.Property(aa => aa.UsernameOrEmailPrivateKey).HasColumnName(DaoAccountAutomationStrings.UsernameOrEmailPrivateKey);
                    ob.Property(aa => aa.UsernameOrEmailInitializationVector).HasColumnName(DaoAccountAutomationStrings.UsernameOrEmailInitializationVector);
                    ob.Property(aa => aa.PasswordEncrypted).HasColumnName(DaoAccountAutomationStrings.PasswordEncrypted);
                    ob.Property(aa => aa.PasswordPrivateKey).HasColumnName(DaoAccountAutomationStrings.PasswordPrivateKey);
                    ob.Property(aa => aa.PasswordInitializationVector).HasColumnName(DaoAccountAutomationStrings.PasswordInitializationVector);
                    ob.Property(aa => aa.ClientId).HasColumnName(DaoAccountAutomationStrings.ClientId);
                    ob.Property(aa => aa.AccessToken).HasColumnName(DaoAccountAutomationStrings.AccessToken);
                    ob.Property(aa => aa.RefreshToken).HasColumnName(DaoAccountAutomationStrings.RefreshToken);
                    ob.Property(aa => aa.AutomationType).HasColumnName(DaoAccountAutomationStrings.AutomationType);
                    ob.Property(aa => aa.AutomationStatus).HasColumnName(DaoAccountAutomationStrings.AutomationStatus);
                    ob.Property(aa => aa.DisplayName).HasColumnName(DaoAccountAutomationStrings.DisplayName);
                    ob.Property(aa => aa.Culture).HasColumnName(DaoAccountAutomationStrings.Culture);
                    ob.Property(aa => aa.Created).HasColumnName(DaoEntityBaseConstants.Created);
                    ob.Property(aa => aa.CreatedBy).HasColumnName(DaoEntityBaseConstants.CreatedBy);
                    ob.Property(aa => aa.Modified).HasColumnName(DaoEntityBaseConstants.Modified);
                    ob.Property(aa => aa.ModifiedBy).HasColumnName(DaoEntityBaseConstants.ModifiedBy);
                })
                .Entity<AccountAutomationType>(
                ob =>
                {
                    ob.ToTable(DaoAccountAutomationTypeStrings.Table);
                    ob.Property(aat => aat.Id).HasColumnName(DaoAccountAutomationTypeStrings.Id);
                    ob.Property(aat => aat.DisplayName).HasColumnName(DaoAccountAutomationTypeStrings.DisplayName);
                    ob.Property(aat => aat.AutomationDescription).HasColumnName(DaoAccountAutomationTypeStrings.AutomationDescription);
                    ob.Property(aat => aat.Created).HasColumnName(DaoEntityBaseConstants.Created);
                    ob.Property(aat => aat.CreatedBy).HasColumnName(DaoEntityBaseConstants.CreatedBy);
                    ob.Property(aat => aat.Modified).HasColumnName(DaoEntityBaseConstants.Modified);
                    ob.Property(aat => aat.ModifiedBy).HasColumnName(DaoEntityBaseConstants.ModifiedBy);
                });
        }

        public override void Dispose()
        {
            // overriden for debugging purposes to see when dispose is being called
            base.Dispose();
        }

        //add range, add, asyncs... make it handle id-less entities by doing a try catch on the entities id. if there is not an id property, then just call base.
        public override EntityEntry Add(object entity)
        {
            SetDomainModelBaseProperties();
            return base.Add(entity);
        }
        // DO ID
        public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
        {
            SetDomainModelBaseProperties();
            return base.AddAsync(entity, cancellationToken);
        }

        // DO ID
        public override void AddRange(IEnumerable<object> entities)
        {
            SetDomainModelBaseProperties();
            base.AddRange(entities);
        }

        // DO ID
        public override Task AddRangeAsync(params object[] entities)
        {
            SetDomainModelBaseProperties();
            return base.AddRangeAsync(entities);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetDomainModelBaseProperties();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            SetDomainModelBaseProperties();
            return base.SaveChanges();
        }

        // these don't need to be called, savechanges is better
        public override EntityEntry Update(object entity)
        {
            throw new NotImplementedException();
        }

        // these don't need to be called, savechanges is better
        public override void UpdateRange(params object[] entities)
        {
            throw new NotImplementedException();
        }

        // TODO TESTS
        internal void SetDomainModelBaseProperties()
        {
            var callerId = RequestSessionInformation.RequestUserId;

            if (callerId == null)
            {
                throw new Exception(InternalServerErrorString.MissingRequestUserIdError);
            }

            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                if (E.Metadata.FindProperty("Id") != null)
                {
                    E.Property("Id").CurrentValue = Guid.NewGuid();
                }
                if (E.Metadata.FindProperty("Created") != null)
                {
                    E.Property("Created").CurrentValue = DateTime.UtcNow;
                }
                if (E.Metadata.FindProperty("CreatedBy") != null)
                {
                    E.Property("CreatedBy").CurrentValue = RequestSessionInformation.RequestUserId.ToString();
                }
                if (E.Metadata.FindProperty("Modified") != null)
                {
                    E.Property("Modified").CurrentValue = DateTime.UtcNow;
                }
                if (E.Metadata.FindProperty("ModifiedBy") != null)
                {
                    E.Property("ModifiedBy").CurrentValue = RequestSessionInformation.RequestUserId.ToString();
                }
            });

            var ModifiedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            ModifiedEntities.ForEach(E =>
            {
                if (E.Metadata.FindProperty("Modified") != null)
                {
                    E.Property("Modified").CurrentValue = DateTime.UtcNow;
                }
                if (E.Metadata.FindProperty("ModifiedBy") != null)
                {
                    E.Property("ModifiedBy").CurrentValue = RequestSessionInformation.RequestUserId.ToString();
                }
            });
        }
    }
}
