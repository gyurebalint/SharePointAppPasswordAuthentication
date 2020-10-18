# SharePointAppPasswordAuthentication

This library provides an extension method for a ClientContext instance which arguments are your SharePoint username and your [SharePoint App Password](https://docs.microsoft.com/en-us/azure/active-directory/user-help/multi-factor-authentication-end-user-app-passwords).

## How to use it

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

When instantiating a ClientContex type (context) you can call the 'GetAppPasswordAuthenticated(string username, string password) extensions method, which will return a ClientContext object for you.

credit: Goes out to [Wictor Wilen](https://www.wictorwilen.se/blog/how-to-do-active-authentication-to-office-365-and-sharepoint-online/)
Check out his blog, and the article on which I founded this extension method.
