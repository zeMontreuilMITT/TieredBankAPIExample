using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TieredBankAPI.BLL;
using TieredBankAPI.DAL;
using TieredBankAPI.Data;
using TieredBankAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TieredBankAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TieredBankAPIContext") ?? throw new InvalidOperationException("Connection string 'TieredBankAPIContext' not found.")));

var app = builder.Build();

app.MapGet("/accounts", async (TieredBankAPIContext db) =>
{
    AccountRepository repo = new AccountRepository(db);
    return repo.GetAccounts();
});

app.MapGet("/accounts/{id}", (string id, TieredBankAPIContext db) => {
    AccountRepository repo = new AccountRepository(db);

    try
    {
        return repo.GetAccountByID(int.Parse(id));
    } catch(Exception e)
    {
        return null;
    }
});

app.MapGet("/accounts/{id}/balance", (string id, TieredBankAPIContext db) => {
    AccountBusinessLogic accountBll = new AccountBusinessLogic(
        new AccountRepository(db));

    try
    {
        decimal balance = accountBll.GetBalance(int.Parse(id));
        return Results.Ok(balance);
    }
    catch(Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/customers", async (TieredBankAPIContext db) =>
{
    CustomerRepository repo = new CustomerRepository(db);
    return repo.GetCustomers();
});

app.MapGet("/customers/{id}/total", async (TieredBankAPIContext db, string id) =>
{
    AccountBusinessLogic accountBusinessLogic = new AccountBusinessLogic(new AccountRepository(db));
    
    decimal sum = accountBusinessLogic.GetSumOfBalances(int.Parse(id));

    var response = JsonSerializer.Serialize(new
    {
        Sum = sum
    });

    return Results.Ok(response);
});

app.MapPost("/accounts/{id}/deposit/{amount}", (TieredBankAPIContext db,string id, string amount) =>
{
    AccountBusinessLogic accountBL = new AccountBusinessLogic(new AccountRepository(db));
    try
    {
        accountBL.Deposit(int.Parse(id), decimal.Parse(amount));
        return Results.Ok();
    }
    catch
    {
        return Results.Problem();
    }
});

app.Run();