namespace AntwortService.Model
{
    public class Antwort
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int FrageId { get; set; }
        public String Frage { get; set; }
        public String FragebogenTyp { get; set; }

    }
}
