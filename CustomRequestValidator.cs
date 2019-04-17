//-----------------------------------------------------------------------------
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//
//-----------------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Util;

using Microsoft.IdentityModel.Protocols.WSFederation;

namespace AHTD.Security.Web
{
    /// <summary>
    /// This CustomRequestValidator validates the wresult parameter of the
    /// WS-Federation passive protocol by checking for a SignInResponse message
    /// in the form post. The SignInResponse message contents are verified later by
    /// the WSFederationPassiveAuthenticationModule or the WIF signin controls.
    /// </summary>
    /// <remarks>
    /// Add the following line to system.web in your config file to use this class:
    /// <code>
    /// &lt;httpRuntime requestValidationType=&quot;AHTD.Security.Web.CustomRequestValidator&quot;/&gt;
    /// </code>
    /// </remarks>
    public class CustomRequestValidator : RequestValidator
    {
        /// <summary>
        /// Validates a string that contains HTTP request data.
        /// </summary>
        /// <param name="context">The context of the current request.</param>
        /// <param name="value">The HTTP request data to validate.</param>
        /// <param name="requestValidationSource">An enumeration that represents the source of request data that is being validated. The following are possible values for the enumeration:QueryStringForm CookiesFilesRawUrlPathPathInfoHeaders</param>
        /// <param name="collectionKey">The key in the request collection of the item to validate. This parameter is optional. This parameter is used if the data to validate is obtained from a collection. If the data to validate is not from a collection, <paramref name="collectionKey"/> can be null.</param>
        /// <param name="validationFailureIndex">When this method returns, indicates the zero-based starting point of the problematic or invalid text in the request collection. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the string to be validated is valid; otherwise, false.
        /// </returns>
        protected override bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
        {
            validationFailureIndex = 0;

            if (requestValidationSource == RequestValidationSource.Form && String.Equals(collectionKey, WSFederationConstants.Parameters.Result, StringComparison.Ordinal))
            {
                //SignInResponseMessage message = WSFederationMessage.CreateFromFormPost(context.Request) as SignInResponseMessage;

                //if (message != null)
                //{
                return true;
                //}
            }

            return base.IsValidRequestString(context, value, requestValidationSource, collectionKey, out validationFailureIndex);
        }
    }
}