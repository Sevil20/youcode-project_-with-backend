namespace YouCode.Models.Entity
{
    public class Code
    {
        private DateTime date;

        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string? CodeText { get; set; }
        public int ProjectId { get; set; }
        public DateTime Date { get => date; set => date = value; }
        public virtual Language? Language { get; set; }
        public virtual Project? Project { get; set; }
    }
}
