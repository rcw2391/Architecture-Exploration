﻿// See https://aka.ms/new-console-template for more information
using DatabaseAccess;
using Models;
using Repositories.Concrete;

Console.WriteLine("Hello, World!");

string connString = @"Server=(localdb)\MSSQLLocalDB;Database=Architecture-Testing;Integrated Security=true;";

// Initialize connection manager.
bool init = await ConnectionManager.Instance.InitAsync(connString);
Console.WriteLine($"ConnectionManager Init: {init}");

// Initialize executioner
Executioner executioner = new(ConnectionManager.Instance);

// Initialize repo
PersonRepository repo = new(executioner);

Person me = await repo.GetByIDAsync(1);

Console.ReadKey();