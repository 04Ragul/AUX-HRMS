using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HRMS.Application.Interfaces.Services;
using HRMS.Domain.Contract;
using HRMS.Domain.Entities.Identity;
using HRMS.Domain.Entities.Chat;
using HRMS.Domain.Entities.Features;
using HRMS.Domain.Entities.Features.Employees;
using HRMS.Domain.Entities.Features.LMS;
using HRMS.Domain.Entities.Features.Masters;
using HRMS.Domain.Entities.Features.Organisations;
using HRMS.Domain.Entities.Features.Recruitment;
namespace HRMS.Infrastructure.Contexts
{
    public class ApplicationDbContext : AuditableContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public DbSet<ChatHistory<ApplicationUser>> ChatHistories { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<IAuditableEntity>? entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = _currentUserService.UserName;
                        entry.Entity.IPAddress = _currentUserService.IpAddress;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
                        entry.Entity.LastModifiedBy = _currentUserService.UserName;
                        entry.Entity.IPAddress = _currentUserService.IpAddress;
                        break;
                }
            }
            return _currentUserService.UserId == 0
                ? await base.SaveChangesAsync(cancellationToken)
                : await base.SaveChangesAsync(_currentUserService.UserName, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableProperty? property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableProperty? property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.Name is "LastModifiedBy" or "CreatedBy"))
            {
                property.SetColumnType("nvarchar(128)");
            }

            base.OnModelCreating(builder);
            _ = builder.Entity<ChatHistory<ApplicationUser>>(entity =>
            {
                _ = entity.ToTable("ChatHistory");

                _ = entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.ChatHistoryFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                _ = entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.ChatHistoryToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            _ = builder.Entity<ApplicationUser>(entity =>
            {
                _ = entity.ToTable(name: "Users", "Identity");
                _ = entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            _ = builder.Entity<ApplicationRole>(entity =>
            {
                _ = entity.ToTable(name: "Roles", "Identity");
            });
            _ = builder.Entity<IdentityUserRole<int>>(entity =>
            {
                _ = entity.ToTable("UserRoles", "Identity");
            });

            _ = builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                _ = entity.ToTable("UserClaims", "Identity");
            });

            _ = builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                _ = entity.ToTable("UserLogins", "Identity");
            });

            _ = builder.Entity<ApplicationRoleClaim>(entity =>
            {
                _ = entity.ToTable(name: "RoleClaims", "Identity");

                _ = entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            _ = builder.Entity<IdentityUserToken<int>>(entity => _ = entity.ToTable("UserTokens", "Identity"));
            _ = builder.Entity<Organisation>(entity =>
            {
                _ = entity.ToTable(name: "Organisation", "Tenant");
            });
            _ = builder.Entity<Location>(entity =>
            {
                _ = entity.ToTable(name: "Location", "Tenant");
            });
            _ = builder.Entity<Branch>(entity =>
            {
                _ = entity.ToTable(name: "Branch", "Tenant");
            });
            _ = builder.Entity<Department>(entity =>
            {
                _ = entity.ToTable(name: "Department", "Master");
            });
            _ = builder.Entity<Designation>(entity =>
            {
                _ = entity.ToTable(name: "Designation", "Master");
            });
            _ = builder.Entity<DocumentType>(entity =>
            {
                _ = entity.ToTable(name: "DocumentType", "Master");
            });
            _ = builder.Entity<Region>(entity =>
            {
                _ = entity.ToTable(name: "Region", "Master");
            });
            _ = builder.Entity<Holiday>(entity =>
            {
                _ = entity.ToTable(name: "Holiday", "LMS");
            });
            _ = builder.Entity<Employee>(entity =>
            {
                _ = entity.ToTable(name: "Employee", "EmployeeManagement");
            });
            _ = builder.Entity<EmployeeBranchMapping>(entity =>
            {
                _ = entity.ToTable(name: "EmployeeBranchMapping", "EmployeeManagement");
            });
            _ = builder.Entity<EmployeeDocuments>(entity =>
            {
                _ = entity.ToTable(name: "EmployeeDocuments", "EmployeeManagement");
            });
            _ = builder.Entity<EmployeeHirachyMapping>(entity =>
            {
                _ = entity.ToTable(name: "EmployeeHirachyMapping", "EmployeeManagement");
            });

            ///Recruit Process
            _ = builder.Entity<JobCategory>(entity =>
            {
                _ = entity.ToTable(name: "JobCategory", "Recruitment");
            });
            _ = builder.Entity<JobLocation>(entity =>
            {
                _ = entity.ToTable(name: "JobLocation", "Recruitment");
            });

        }
    }
}
