using AntwortService.Model;
using AuswertungService.Model;

namespace AuswertungService.Services
{
    public interface IAuswertungManagementService
    {
        List<string> HoleAlleAntworten();
        List<Auswertung> HoleAlleAuswertungen();
        Auswertung HoleAuswertung(int id);
        void LoescheAuswertung(int id);
        void SpeichereAuswertung(Auswertung auswertung);
        void SpeichereAuswertung(string auswertung);
        void SpeichereAntwort(Antwort antwort);
    }
}