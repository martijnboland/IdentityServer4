﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Core.Models;
using System.Threading.Tasks;

namespace IdentityServer4.Core.Services.Default
{
    /// <summary>
    /// Base class implementation of IUserService with empty method implementations.
    /// </summary>
    public class UserServiceBase : IUserService
    {
        /// <summary>
        /// This method gets called before the login page is shown. This allows you to determine if the user should be authenticated by some out of band mechanism (e.g. client certificates or trusted headers).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task PreAuthenticateAsync(PreAuthenticationContext context)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// This method gets called for local authentication (whenever the user uses the username and password dialog).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// This method gets called when the user uses an external identity provider to authenticate.
        /// The user's identity from the external provider is passed via the `externalUser` parameter which contains the
        /// provider identifier, the provider's identifier for the user, and the claims from the provider for the external user.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// This method is called prior to the user being issued a login cookie for IdentityServer.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task PostAuthenticateAsync(PostAuthenticationContext context)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// This method gets called when the user signs out.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task SignOutAsync(SignOutContext context)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}