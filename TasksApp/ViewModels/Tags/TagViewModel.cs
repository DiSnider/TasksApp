using System.ComponentModel.DataAnnotations;

namespace TasksApp.ViewModels
{
    public class TagViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}