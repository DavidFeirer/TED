namespace FrageService.Model
{
    public class Frage
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long FragebogenId { get; set; }
    }
}
