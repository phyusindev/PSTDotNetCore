using PSTDotNetCore.ConsoleAppRestClientExample;

Console.WriteLine("Hello, World!");

//Console App - Client (frontend)
//ASP.NET Core Web API - Server (backend)


RestClientExample restClientExample = new RestClientExample ();
await restClientExample.RunAsync();

Console.ReadLine();
