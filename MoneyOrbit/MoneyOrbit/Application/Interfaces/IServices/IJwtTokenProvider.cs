using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IJwtTokenProvider
    {
        string Create(User user);
    }
}