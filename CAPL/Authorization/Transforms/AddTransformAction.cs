﻿/*
Claims Authorization Policy Langugage SDK ver. 1.0
 
Copyright (c) Matt Long labskunk@gmail.com
 
All rights reserved.
 
MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the ""Software""), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace Capl.Authorization.Transforms
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Capl.Authorization;

    /// <summary>
    /// A transform that adds a new claim to a set of claims.
    /// </summary>
    public class AddTransformAction : TransformAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddTransformAction"/> class.
        /// </summary>
        public AddTransformAction()
        {
        }


        public static Uri TransformUri
        {
            get { return new Uri(AuthorizationConstants.TransformUris.Add); }
        }
        
        /// <summary>
        /// Gets the URI that identifies the add transform action.
        /// </summary>
        public override Uri Uri
        {
            get { return new Uri(AuthorizationConstants.TransformUris.Add); }
        }

        /// <summary>
        /// Executes the add transform action.
        /// </summary>
        /// <param name="claimSet">Set of claims to perform the action.</param>
        /// <param name="sourceClaim">The source claims, which is ignored for the add transform action.</param>
        /// <param name="targetClaim">The target claim to be added with this action.</param>
        /// <returns>A transformed set of claims.</returns>
        public override IEnumerable<Claim> Execute(IEnumerable<Claim> claims, IList<Claim> matchedClaims, LiteralClaim targetClaim)
        {
            if (claims == null)
            {
                throw new ArgumentNullException("claims");
            }

            if (targetClaim == null)
            {
                throw new ArgumentNullException("targetClaim");
            }

            if (matchedClaims != null)
            {
                throw new ArgumentException("The expected value of matchedClaims must be null.");
            }

            List<Claim> claimList = new List<Claim>(claims);

            Claim claim = new Claim(targetClaim.ClaimType, targetClaim.ClaimValue);
            claimList.Add(claim);

            return claimList.ToArray();  
            
        }
    }
}
