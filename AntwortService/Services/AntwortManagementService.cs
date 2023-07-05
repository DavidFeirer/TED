using AntwortService.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AntwortService.Services
{
    public class AntwortManagementService
    {
        private readonly AntwortContext _context;
        private readonly UpdateQueue _updateQueue;

        public AntwortManagementService(AntwortContext context, UpdateQueue updateQueue)
        {
            this._context = context;
            this._updateQueue = updateQueue;
        }

        public void SpeichereAntwort(Antwort antwort)
        {
            try
            {
                _context.Antworten.Add(antwort);
                _updateQueue.PostToQueue(antwort);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Antwort Speicher Error.", e);
            }
        }

        public List<Antwort> HoleAlleAntworten()
        {
            return _context.Antworten.ToList();
        }

        public Antwort HoleAntwort(int id)
        {

            return _context.Antworten.Find(id);

        }

        public void LoescheAntwort(int id)
        {
            var antwort = _context.Antworten.Find(id);
            _context.Antworten.Remove(antwort);
            _context.SaveChanges();
        }
    }


}
