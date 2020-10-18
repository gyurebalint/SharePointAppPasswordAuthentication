## SharePoint authentication with a SharePoint App Password

### Background
I work as a mechanical engineer in a designer company in Budapest, we design with Autodesk Inventor, and I started working on little piece of program that would allow designers to interact with our sharepoint lists within our designer program. With this addin they wouldn't need to leave the program to read or write to our sharepoint lists.
The problem was that we used **Two-step authentication** when logging into SharePoint, plus all legacy-authentication is disable. Our company's COO takes security very seriously and for good reason therefore the tightened security.

### Problem statement
When creating the prototype it was clear that the multi-step authentication will require an exact solution to be implemented, if you are here i'm sure you already found this linke in [C-sharpcorner](https://www.c-sharpcorner.com/blogs/using-csom-to-connect-to-a-sharepoint-site-with-multi-factor-authentication-enabled).
After adding the package **SharePointPnPCoreOnline**, I instantiated the *AuthenticationManager* object and called its *GetWebLoginClientContext* which return a ClientContext just what I needed. There was a problem with this, since after three requests, the authentication window the method gave me got stuck. Not only did it got stuck, it got stuck for 5 minutes, exactly. I knew there was something wrong, and I had a gap in my knowledge.

I tried to give the username and password in a form of the SharePointOnlineCredentials class, but the error message 'Unauthorized', or that I cannot use the SharePointOnlineCredentials alltogether. Every article I read said I either have to turn the *legacyprotocols* enabled or register an app on SharePoint or on Azure.

Now, registering an app with SharePoint had its drawbacks, because if you do this, than any change made by your application will be created by your application. So when you take a look at one of your lists, add an item, the 'Created by' column will say your username, since you are the one who added that item. Now if a SharePoint registered application would have added that item, the 'Created by' column would show your applications name. This isn't good, every addition every change has to be tracked for future reference. (Who, what, when something happened).
The alternate way is to register an app on Azure, which really is a pain unless you know what you are doing, plus if you aren't the administrator, convincing your superiors who don't code to register an app and do this and that is not going to be easy.

### Solution
Finally I found a solution which uses SharePoint App Passwords, which I can create on my own, it is a valid authentication which I can use programmatically. It turned out that with the previously mentioned *GetWebLoginClientContext* method's requests didn't bring back the necessary cookies for authentication. I couldn' get around that, so I searched some more and found a guy that wrote the MsOnlineClaimsHelper class and another one, his name is [Wictor Wilen](https://www.wictorwilen.se/blog/how-to-do-active-authentication-to-office-365-and-sharepoint-online/) you can read his blogpost about the problem and solution
I only abstracted away the necessary classes and created an extension method for the ClientContext object.
This way I generated SharePoint App Passwords for all of my designers, created a generic credential in Windows's Credential Managers to save the password and username safely on their computers, and they could authenticate themselves, this way everyone manages their own user and the changes in SharePoint lists can be tracked.

### How to use it

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

When instantiating a ClientContex type (context) you can call the 'GetAppPasswordAuthenticated(string username, string sharepointAppPassword) extensions method, which will return a ClientContext object for you.

credit: Goes out to [Wictor Wilen](https://www.wictorwilen.se/blog/how-to-do-active-authentication-to-office-365-and-sharepoint-online/)
Check out his blog, and the article on which I founded this extension method.
