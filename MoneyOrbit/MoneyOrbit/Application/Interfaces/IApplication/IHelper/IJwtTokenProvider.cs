using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IHelper
{
    public interface IJwtTokenProvider
    {
        string CreateToken(User user);
    }
}