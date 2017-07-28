namespace TasksApp.Migrations
{
    using System.Data.Entity.Migrations;
    using Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<TasksContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TasksApp.Models.TasksContext";
        }

        protected override void Seed(TasksContext context)
        {
            var tags = new List<Tag>
            {
                new Tag { Name = "Homework" },
                new Tag { Name = "Programming" },
                new Tag { Name = "Events" },
                new Tag { Name = "Rest" },
                new Tag { Name = "Family" }
            };

            context.Tags.AddRange(tags);
            context.SaveChanges();
        }
    }
}
