using System.ComponentModel;
using System.Security.Claims;

namespace Extensions.Base;

public static class IdentityExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user) => GetUserId<Guid>(user);
    //extension method  - this in front. is added to the class definition
    public static TKeyType GetUserId<TKeyType>(this ClaimsPrincipal user)
    {
        var idClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (idClaim == null)
        {
            throw new NullReferenceException("Claim not found!");
        }


        var res = (TKeyType?) TypeDescriptor.
            GetConverter(typeof(TKeyType)).
            ConvertFromInvariantString(idClaim.Value)!;
        return res;

    }
}