using System.ComponentModel.DataAnnotations;

namespace YouCode.Models.Entity
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? ProjectName { get; set; }
        public List<Code>? Codes { get; set; }

    }
}
