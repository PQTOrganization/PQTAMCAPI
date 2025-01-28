using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Providers.AuthHandlers.Scheme;
using PQAMCClasses;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Providers.AuthHandlers.Scheme
{
    public class PQAMCAuthSchemeOptions : AuthenticationSchemeOptions
    { }
}

namespace PQAMCAPI.Providers.AuthHandlers
{
    public class PQAMCAuthHandler : AuthenticationHandler<PQAMCAuthSchemeOptions>
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenDBService _tokenDBService;

        public PQAMCAuthHandler(
                    IOptionsMonitor<PQAMCAuthSchemeOptions> options,
                    IConfiguration configuration,
                    ILoggerFactory logger,
                    UrlEncoder encoder,
                    ISystemClock clock,
                    ITokenDBService tokenDBService)
                    : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
            _tokenDBService = tokenDBService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            TokenModel model;

            // validation comes in here
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                return AuthenticateResult.Fail("Header Not Found.");
            }

            var header = Request.Headers[HeaderNames.Authorization].ToString();
            var tokenMatch = Regex.Match(header, $"Bearer (?<token>.*)");

            if (tokenMatch.Success)
            {
                // the token is captured in this group
                // as declared in the Regex
                var token = tokenMatch.Groups["token"].Value;
                var expired = await _tokenDBService.IsExpireToken(token);
                if (expired)
                {
                    return AuthenticateResult.Fail("INVALID TOKEN");
                }

                try
                {
                        var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token);

                    // deserialize the JSON string obtained from the byte array
                    model = new TokenModel()
                    {
                        UserId = int.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                        Email = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.GivenName).Value,
                        SecurityKeys = jwtSecurityToken.Claims.First(claim => claim.Type == Globals.SessionKeys.SecurityKeys).Value
                    };
                    int AcctCatID = 0;

                    int.TryParse(jwtSecurityToken.Claims.First(claim => claim.Type == "AccountCategoryID").Value, out AcctCatID);
                    model.AccountCategoryID = AcctCatID;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Exception Occured while Deserializing: " + ex);
                    return AuthenticateResult.Fail("TokenParseException");
                }

                // success branch
                // generate authTicket
                // authenticate the request
                if (model != null)
                {
                    // create claims array from the model
                    var claims = new[] {
                        new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim("AccountCategoryID", model.AccountCategoryID.ToString()),
                        new Claim(Globals.SessionKeys.SecurityKeys, model.SecurityKeys)
                    };

                    // generate claimsIdentity on the name of the class
                    var claimsIdentity = new ClaimsIdentity(claims,
                                nameof(PQAMCAuthHandler));

                    // generate AuthenticationTicket from the Identity
                    // and current authentication scheme
                    var ticket = new AuthenticationTicket(
                        new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);

                    // pass on the ticket to the middleware
                    return AuthenticateResult.Success(ticket);
                }
            }

            // failure branch
            // return failure
            // with an optional message
            return AuthenticateResult.Fail("Model is Empty");
        }
    }
}
