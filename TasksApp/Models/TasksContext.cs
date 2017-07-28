using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TasksApp.Models
{
    public class TasksContext : IdentityDbContext<AppUser>
    {
        public TasksContext() : base("TasksDb")
        {
            Database.SetInitializer(new TasksContextInitializer());
        }

        public static TasksContext Create()
        {
            return new TasksContext();
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasMany(task => task.RelatedTags)
                .WithMany(tag => tag.RelatedTasks)
                .Map(mc =>
                {
                    mc.ToTable("TasksTags");
                    mc.MapLeftKey("TaskId");
                    mc.MapRightKey("TagId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }

    public class TasksContextInitializer : CreateDatabaseIfNotExists<TasksContext>
    {
        protected override void Seed(TasksContext context)
        {
            //...

            base.Seed(context);
        }
    }
}