using System;
using System.Security.Claims;

public static class TokenHelper
{
    public static int RetornaIdUsuario(this ClaimsPrincipal claim)
    {
        return Convert.ToInt32(claim.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}