﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer4.Core.Validation
{
    /// <summary>
    /// Models the result of custom grant validation.
    /// </summary>
    public class CustomGrantValidationResult : ValidationResult
    {
        /// <summary>
        /// Gets or sets the principal which represents the result of the authentication.
        /// </summary>
        /// <value>
        /// The principal.
        /// </value>
        public ClaimsPrincipal Principal { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomGrantValidationResult"/> class with an error message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public CustomGrantValidationResult(string errorMessage)
        {
            Error = errorMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomGrantValidationResult"/> class with no error and no user.
        /// </summary>
        public CustomGrantValidationResult()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomGrantValidationResult"/> class.
        /// </summary>
        /// <param name="subject">The subject claim used to uniquely identifier the user.</param>
        /// <param name="authenticationMethod">The authentication method which describes the custom grant type.</param>
        /// <param name="claims">Additional claims that will be maintained in the principal.</param>
        /// <param name="identityProvider">The identity provider.</param>
        public CustomGrantValidationResult(
            string subject, 
            string authenticationMethod,
            IEnumerable<Claim> claims = null,
            string identityProvider = Constants.BuiltInIdentityProvider)
        {
            var resultClaims = new List<Claim>
            {
                new Claim(Constants.ClaimTypes.Subject, subject),
                new Claim(Constants.ClaimTypes.AuthenticationMethod, authenticationMethod),
                new Claim(Constants.ClaimTypes.IdentityProvider, identityProvider),
                new Claim(Constants.ClaimTypes.AuthenticationTime, DateTimeOffsetHelper.UtcNow.ToEpochTime().ToString(), ClaimValueTypes.Integer)
            };

            if (claims != null && claims.Any())
            {
                resultClaims.AddRange(claims.Where(x => !Constants.OidcProtocolClaimTypes.Contains(x.Type)));
            }

            var id = new ClaimsIdentity(authenticationMethod);
            id.AddClaims(resultClaims.Distinct(new ClaimComparer()));

            Principal = new ClaimsPrincipal(id);

            IsError = false;
        }
    }
}