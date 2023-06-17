namespace Fragenevaluierung.Service
{
    public class CheckFrage : ICheckFrage
    {
        public bool isValid(String text)
        {
            return !(string.IsNullOrEmpty(text) || !text.Contains('?'));
        }
    }
}
