using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TasksApp.ViewModels
{
    public class CreateTaskViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public IEnumerable<TagViewModel> SelectedTags { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ExpirationDate { get; set; }
    }
}