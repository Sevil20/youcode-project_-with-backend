namespace YouCode.Models.Entity
{
    public class Language
    {
        private string? languageName;
        private List<Code>? codes;

        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string LanguageName { get => languageName; set => languageName = value; }
        public List<Code> Codes { get => codes; set => codes = value; }
    }
}
