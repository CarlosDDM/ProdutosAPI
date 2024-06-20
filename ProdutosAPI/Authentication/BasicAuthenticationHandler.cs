using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ProdutosAPI.Authentication;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }


    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        string authorizationHeader = Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return AuthenticateResult.Fail("Unauthorized");

        }

        if (authorizationHeader.StartsWith("baisc ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Unathorized");
        }

        var token = authorizationHeader.Substring(6);
        var credencialTexto = Encoding.UTF8.GetString(Convert.FromBase64String(token));

        var credencial = credencialTexto.Split(":");

        if (credencial.Length != 2)
        {
            return AuthenticateResult.Fail("Unathorized");
            
        }

        var nomeUsuario =  credencial[0];
        var senhaUsuario = credencial[1];

        if (nomeUsuario != "admin" || senhaUsuario != "password")
        {
            return AuthenticateResult.Fail("Authentication failed");
        }

        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, nomeUsuario),
        };

        var identidade = new ClaimsIdentity(claims, "Basic");
        var claimPrincipal = new ClaimsPrincipal(identidade);

        return AuthenticateResult.Success(new AuthenticationTicket(claimPrincipal, Scheme.Name));


    }
}
