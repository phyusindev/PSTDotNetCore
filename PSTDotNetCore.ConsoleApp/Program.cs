using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PSTDotNetCore.ConsoleApp.AdoDotNetExamples;
using PSTDotNetCore.ConsoleApp.DapperExamples;
using PSTDotNetCore.ConsoleApp.EFCoreExamples;
using PSTDotNetCore.ConsoleApp.Services;
using PSTDotNetCore.RestApi.Db;
using System;
using System.Data;
using System.Data.SqlClient;

//Console.WriteLine("Hello, World!");

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("title", "author", "content");
//adoDotNetExample.Update(1001,"title1", "author1", "content1");
//adoDotNetExample.Delete(1001);
//adoDotNetExample.Edit(1001);

//DapperExample dapperExample = new DapperExample();  
//dapperExample.Run();

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Run();

var connectionString = ConnectionStrings.sqlConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

var serviceProvider = new ServiceCollection()
    .AddScoped(n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped(n => new DapperExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

//AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();

var adoDotNetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
adoDotNetExample.Read();

//var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
//dapperExample.Run();

Console.ReadLine();

