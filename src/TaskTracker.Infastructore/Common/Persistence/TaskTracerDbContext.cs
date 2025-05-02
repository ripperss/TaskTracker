using TaskTracker.Infastructore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain.Users;
using TaskTracker.Domain.TaskComments;
using TaskTracker.Domain.TaskParticipants;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Managers;
using TaskTracker.Domain.TasksUser;
using TaskTracker.Application.Common.Interfaces;

namespace TaskTracker.Infastructore.Common.Persistence;

public class TaskTrackerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IUnitOfWork
{
    public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskComment> TaskComments { get; set; }
    public DbSet<TaskParticipant> TaskParticipants { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Tasks>  Tasks { get; set; }

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(TaskTrackerDbContext).Assembly);

        // Настройка Identity
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("UsersApplication");
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable("Roles");
        });

        builder.Entity<IdentityUserRole<Guid>>(entity =>
        {
            entity.ToTable("UserRoles");
        });

        builder.Entity<IdentityUserClaim<Guid>>(entity =>
        {
            entity.ToTable("UserClaims");
        });

        builder.Entity<IdentityUserLogin<Guid>>(entity =>
        {
            entity.ToTable("UserLogins");
        });

        builder.Entity<IdentityRoleClaim<Guid>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });

        builder.Entity<IdentityUserToken<Guid>>(entity =>
        {
            entity.ToTable("UserTokens");
        });
    }
}
