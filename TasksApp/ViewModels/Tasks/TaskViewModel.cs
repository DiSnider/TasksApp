using System.Collections.Generic;

namespace TasksApp.ViewModels
{
    public class TaskViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsDone { get; set; }

        public IEnumerable<string> RelatedTags { get; set; }

        public string CreationDate { get; set; }

        public bool IsExpired { get; set; }
    }
}