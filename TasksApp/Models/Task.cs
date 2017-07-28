using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksApp.Models
{
    public class Task
    {
        public Task()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Index]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [Required]
        public virtual AppUser User { get; set; }

        public bool IsDone { get; set; }

        public virtual ICollection<Tag> RelatedTags { get; set; }

        [Index]
        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [NotMapped]
        public bool IsExpired => ExpirationDate.HasValue && DateTime.UtcNow > ExpirationDate.Value;
    }
}