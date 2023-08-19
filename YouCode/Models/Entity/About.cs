namespace YouCode.Models.Entity
{
    public class About
    {
        [System.ComponentModel.DataAnnotations.Key]

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Url { get; set; }
    }
}
