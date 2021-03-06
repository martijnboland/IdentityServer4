﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNet.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer4.Core;
using System.Collections.Generic;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services.InMemory;
using System.Security.Claims;
using System.Linq;

namespace IdentityServer4.Tests.Common
{
    public class AuthorizeEndpointTestBase
    {
        public const string LoginPage = "https://server/ui/login";
        public const string ConsentPage = "https://server/ui/consent";
        public const string ErrorPage = "https://server/ui/error";

        public const string DiscoveryEndpoint = "https://server/.well-known/openid-configuration";
        public const string AuthorizeEndpoint = "https://server/connect/authorize";
        public const string TokenEndpoint = "https://server/connect/token";

        protected readonly TestServer _server;
        protected readonly HttpClient _client;
        protected readonly Browser _browser;
        protected readonly MockAuthorizationPipeline _mockPipeline;

        protected readonly AuthorizeRequest _authorizeRequest = new AuthorizeRequest(AuthorizeEndpoint);

        public AuthorizeEndpointTestBase()
        {
            _mockPipeline = new MockAuthorizationPipeline(Clients, Scopes, Users);
            _server = TestServer.Create(null, _mockPipeline.Configure, _mockPipeline.ConfigureServices);
            _browser = new Browser(_server.CreateHandler());
            _client = new HttpClient(_browser);
        }

        protected List<Client> Clients = new List<Client>();
        protected List<Scope> Scopes = new List<Scope>();
        protected List<InMemoryUser> Users = new List<InMemoryUser>();

        public async Task LoginAsync(ClaimsPrincipal subject)
        {
            var old = _browser.AllowAutoRedirect;
            _browser.AllowAutoRedirect = false;

            _mockPipeline.Subject = subject;
            await _client.GetAsync(LoginPage);

            _browser.AllowAutoRedirect = old;
        }

        public async Task LoginAsync(string subject)
        {
            var user = Users.Single(x => x.Subject == subject);
            var name = user.Claims.Where(x => x.Type == "name").Select(x=>x.Value).FirstOrDefault() ?? user.Username;
            await LoginAsync(IdentityServerPrincipal.Create(subject, name));
        }

        public string CreateAuthorizeUrl(
            string clientId, 
            string responseType, 
            string scope = null, 
            string redirectUri = null, 
            string state = null, 
            string nonce = null, 
            string loginHint = null, 
            string acrValues = null, 
            string responseMode = null, 
            object extra = null)
        {
            var url = _authorizeRequest.CreateAuthorizeUrl(
                clientId: clientId,
                responseType: responseType,
                scope: scope,
                redirectUri: redirectUri,
                state: state,
                nonce: nonce,
                loginHint: loginHint,
                acrValues: acrValues,
                responseMode: responseMode, 
                extra: extra);
            return url;
        }

        public IdentityModel.Client.AuthorizeResponse ParseAuthorizationResponseUrl(string url)
        {
            return new IdentityModel.Client.AuthorizeResponse(url);
        }
    }
}
