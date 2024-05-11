using Newtonsoft.Json;
using PSTDotNetCore.ConsoleAppHttpClientExample;

Console.WriteLine("Hello, World!");

//Console App - Client (frontend)
//ASP.NET Core Web API - Server (backend)


HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

Console.ReadLine();