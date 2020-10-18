using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointAuthentication
{
    class Program
    {
        static void Main(string[] args)
        {
            var cred = CredentialManager.GetCredential("DrawingRegistryAccess");
            using (ClientContext context = new ClientContext("Your SharePoint site's URL here").GetAppPasswordAuthenticatedContext("Your UserName", "Your Password")) 
            {
                
                context.Load(context.Web);

                context.ExecuteQuery();

                Console.WriteLine("Name of the web is: " + context.Web.Title);
                Console.ReadKey();
            }
        }
    }
}
