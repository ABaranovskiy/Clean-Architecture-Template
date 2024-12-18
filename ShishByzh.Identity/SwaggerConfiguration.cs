using Duende.IdentityServer.Models;

namespace ShishByzh.Identity
{
    public static class SwaggerConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new("api1", "Identity API v1")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
        [
            new("api1", "Identity API v1")
            {
                Scopes = { "api1" }
            }
        ];
        
        public static IEnumerable<Client> Clients =>
        [
            new()
            {
                ClientId = "angular-client",
                ClientName = "Angular Client",
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = { "openid", "profile", "api1" },
                RequirePkce = true,
                AllowAccessTokensViaBrowser = true,
                ClientSecrets = { new Secret("secret-angular-client".Sha256()) }
            }
        ];
    }
}
