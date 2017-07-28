using System.Collections.Generic;

namespace TasksApp.ViewModels
{
    public class TaskDetailsViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsDone { get; set; }

        public IEnumerable<TagViewModel> RelatedTags { get; set; }

        public string CreationDate { get; set; }

        public string LastModificationDate { get; set; }

        public string ExpirationDate { get; set; }

        public bool IsExpired { get; set; }
    }
}