using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YouCode.Models.Entity
{
    [Table("Blogs")]
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BlogId { get; set;}
        public string? BlogImage { get; set;}
        public string BlogName { get; set;}
        public string BlogDescription { get; set;}

    }
}
    

