using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointAuthentication
{

    public static class SharePointConnection
    {
        public static ClientContext GetAppPasswordAuthenticatedContext(this ClientContext clientContext, string userName, string password)
        {
            MsOnlineClaimsHelper claims = new MsOnlineClaimsHelper(clientContext.Url, userName, password);
            ClientContext Context = new ClientContext(clientContext.Url);
            Context.ExecutingWebRequest += claims.clientContext_ExecutingWebRequest;

            return Context;
        }
    }
}
