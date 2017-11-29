using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace IdentityServerHost
{
    public class Config
    {
        private const string CLIENT_SECRET = "67A20C10A94248DBA64B4F1EB00CFD6A";
        private const string API = "api1";

        public static IEnumerable<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "spa",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = {
                    "http://localhost:5000/callback.html",
                    "http://localhost:5000/popup.html",
                    "http://localhost:5000/silent.html"
                },
                PostLogoutRedirectUris = { "http://localhost:5000/index.html" },
                AllowedScopes = { "openid", "profile", "email", "api1" },
                AllowedCorsOrigins = { "http://localhost:5000" }
            },
            new Client
            {
                ClientId = "postman-api",
                ClientSecrets = new List<Secret> {new Secret(CLIENT_SECRET.Sha256()) },
                AllowedGrantTypes = 
                {
                    GrantType.ResourceOwnerPassword,
                    GrantType.Hybrid,
                    "my_custom_grant_type"
                },
                RequireConsent = false,
                EnableLocalLogin = true,
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,

                RedirectUris = { "https://www.getpostman.com/oauth2/callback" },
                PostLogoutRedirectUris = {"https://www.getpostman.com" },
                AllowedCorsOrigins={ "https://www.getpostman.com" },
                AllowedScopes = { "openid", "profile", "email", API },
            },
        };

        public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

        public static IEnumerable<ApiResource> Apis = new List<ApiResource>
        {
            new ApiResource("api1", "My API 1")
        };
    }
}
