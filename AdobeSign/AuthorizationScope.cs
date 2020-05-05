﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdobeSignatureV6
{
    /// <summary>
    /// Authorization Scope to get the authorization code
    /// https://secure.in1.echosign.com/public/static/oauthDoc.jsp#scopes
    /// </summary>
    public enum AuthorizationScope
    {
        agreement_read,
        agreement_retention,
        agreement_send,
        agreement_vault,
        agreement_write,
        library_read,
        library_write,
        user_login,
        user_read,
        user_write,
        widget_read,
        widget_write,
        workflow_read,
        workflow_write
    }
}
