using UserRegister.Business.Interfaces.Repositories;
using UserRegister.Business.Interfaces.Services;

namespace UserRegister.Application.Services
{
    public class ExempleService : IExempleService
    {
        //private readonly IExempleRepository _exempleRepository;

        // public ExempleService(
        //     IExempleRepository exempleRepository)
        // {
        //     _exempleRepository = exempleRepository;
        // }
        
        public ExempleService()
        {
        }

        public async Task<string> GetString()
        {
            return "Exemple Service Success";
        }
    }
}
