using PSTDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

//Console.WriteLine("Hello, World!");

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("title", "author", "content");
//adoDotNetExample.Update(1001,"title1", "author1", "content1");
//adoDotNetExample.Delete(1001);
//adoDotNetExample.Edit(1001);

DapperExample dapperExample = new DapperExample();  
dapperExample.Run();

Console.ReadLine();

