using AntwortService.Model;
using AuswertungService.Model;

namespace AuswertungService.Services
{
    public class AuswertungManagementService : IAuswertungManagementService
    {
        private readonly AuswertungContext _context;

        public AuswertungManagementService(AuswertungContext context)
        {
            this._context = context;
        }

        public void SpeichereAuswertung(Auswertung auswertung)
        {
            try
            {
                _context.Auswertungen.Add(auswertung);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Auswertung Speicher Error.", e);
            }
        }

        public void SpeichereAuswertung(String auswertung)
        {
            Auswertung auswertungVar = new Auswertung()
            {
                Id = 0,
                FragebogenTyp = "string",
                FrageId = 0,
                Frage = "string",
                Antworten = auswertung
            };
            _context.Auswertungen.Add(auswertungVar);
            _context.SaveChanges();
        }

        public List<Auswertung> HoleAlleAuswertungen()
        {
            return _context.Auswertungen.ToList();
        }

        public List<String> HoleAlleAntworten()
        {
            List<String> antworten = new List<String>();

            foreach (var auswertung in _context.Auswertungen.ToList())
            {
                foreach (var antwort in auswertung.Antworten.Split("##$$##"))
                {
                    antworten.Add(antwort);
                }
            }

            return antworten;
        }

        public Auswertung HoleAuswertung(int id)
        {

            return _context.Auswertungen.Find(id);

        }

        public void LoescheAuswertung(int id)
        {
            var auswertung = _context.Auswertungen.Find(id);
            _context.Auswertungen.Remove(auswertung);
            _context.SaveChanges();
        }

        public void SpeichereAntwort(Antwort antwort)
        {
            Auswertung existingAuswertung = _context.Auswertungen.ToList().Find(x=> x.FrageId == antwort.FrageId && x.FragebogenTyp == antwort.FragebogenTyp);
            if(existingAuswertung != null)
            {
                existingAuswertung.Antworten += "##$$##" + antwort.Text;
                _context.Auswertungen.Update(existingAuswertung);
            }
            else
            {
                Auswertung auswertungVar = new Auswertung()
                {
                    FragebogenTyp = antwort.FragebogenTyp,
                    FrageId = antwort.FrageId,
                    Frage = antwort.Frage,
                    Antworten = antwort.Text
                };
                _context.Auswertungen.Add(auswertungVar);
            }
            _context.SaveChanges();
        }
    }
}
