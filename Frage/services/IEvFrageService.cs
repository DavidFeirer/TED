using Consul;

namespace FrageService.Services
{
    public interface IEvFrageService
    {
        Task<bool> isValid(String text);
    }
}
