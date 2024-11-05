using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core.Tokens.Configs;

public static class SecretKeyHelper
{
    public static SecurityKey GetSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
}
