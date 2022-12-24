using Reports.Application.Dto.Models;
using Reports.Core.Models;

namespace Reports.Application.Mapping;

public static class ModelsMapping
{
    public static AuthenticatorDto AsDto(this Authenticator authenticator)
    {
        return new AuthenticatorDto(authenticator.Login, authenticator.Password);
    }
}