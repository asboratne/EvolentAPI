using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactManagement.App_Start
{
    //API Auth provider
    public class APIAuthProvider: OAuthAuthorizationServerProvider
    {
        //This method does not require any "grant_type" and will not do any username,password or client Id validations.
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        //This method requires "grant_type" as "password" to be called and do the auth validations.
        //"http://localhost:port/token" will give call to this method.
        //Once the claims are validated, a token is generated "access_token", we will use this token as "bearer" for further requests to api.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == "evolent" && context.Password == "001319a4-24d1-4212-86be-389245a3add9")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "evolent"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                context.Rejected();
            }
        }

    }
}