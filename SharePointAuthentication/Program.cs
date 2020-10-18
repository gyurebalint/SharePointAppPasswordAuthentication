using Microsoft.SharePoint.Client;
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
