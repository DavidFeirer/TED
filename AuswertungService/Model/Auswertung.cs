namespace AuswertungService.Model
{
    public class Auswertung
    {
        public int Id { get; set; }
        public String FragebogenTyp { get; set; }
        public long FrageId { get; set; }
        public String Frage { get; set; }
        
        public String Antworten { get; set; }
    }
}

// 1 - Nachhaltigkeit - 15 - Wie nachhaltig ist das Produkt? - { "Sehr", "Gar net", "xD" }
// 2 - Nachhaltigkeit - 16 - Was ist nachhaltigkeit? - { "Keine Ahnung", "Mir egal" }
// 3 - Nachhaltigkeit - 17 - Lebst du nachhaltig? - { "Ja", "Nein" } 

// Nachhaltigkeit - 3 Fragen
// Nachhaltigkeit - Frage 15 - 3 Antworten
// Nachhaltigkeit - 7 Antworten