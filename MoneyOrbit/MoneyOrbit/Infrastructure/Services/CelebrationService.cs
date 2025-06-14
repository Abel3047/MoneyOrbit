using MoneyOrbit.Application.DTOs.MotivationDtos;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Infrastructure.Services
{
    public class CelebrationService : ICelebrationService
    {
        private readonly ITrophyRepository<Trophy> _trophyRepository;

        public CelebrationService(ITrophyRepository<Trophy> trophyRepository)
        {
            _trophyRepository = trophyRepository;
        }

        public Task<ResultObject> GetMotivationalStatement(GetMotivationStatementDto mdto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultObject> GetTrophy(GetTrophyDto tdto)
        {
            var response = await _trophyRepository.GetInstanceOfType<Trophy>(tdto.ID);
            return new ResultObject() { Result= response };
        }

        public Task<ResultObject> SetMotivationalStatement(CreateMotivationalStatementDto mdto)
        {
            throw new NotImplementedException();
        }

        public Task<ResultObject> SetTrophy(CreateTrophyDto tdto)
        {
            throw new NotImplementedException();
        }
    }
}