using DatabaseAccess;
using Models;
using Repositories.Concrete;
using Repositories.Interfaces;

string connString = @"Server=(localdb)\MSSQLLocalDB;Database=Architecture-Testing;Integrated Security=true;";

// Initialize connection manager.
bool init = await ConnectionManager.Instance.InitAsync(connString);
Console.WriteLine($"ConnectionManager Init: {init}");

// Initialize executioner
IExecutioner executioner = new Executioner(ConnectionManager.Instance);

// Initialize repo
IPersonRepository repo = new PersonRepository(executioner);

Person me = await repo.GetByIDAsync(1);
List<Person> people = new(await repo.GetAllWithChildren());

Console.Read();