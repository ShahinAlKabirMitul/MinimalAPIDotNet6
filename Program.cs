using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionStrings=builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddSingleton<ItemRepository>();
builder.Services.AddDbContext<ApiDbContext>( options=> options.UseSqlite(connectionStrings));


var app=builder.Build();
app.MapGet("/items",([FromServices] ItemRepository item)=>
{
    return item.GetAll();
});

app.MapPost("/items",([FromServices] ItemRepository items,Item item)=>{
    if (items.GetById(item.id) != null)
    {
        return Results.NotFound();
    }
    items.Add(item);
    return Results.Created($"/items/{item.id}",item);
});

app.MapGet("items/{id}",([FromServices] ItemRepository items,int id) =>{
    var item = items.GetById(id);
    return item == null ? Results.NotFound() : Results.Ok(item);
});

app.MapPut("items/{id}",([FromServices] ItemRepository items ,int id,Item item)=> {
if (items.GetById(id) != null)
    {
        return Results.NotFound();
    }
    items.Update(item);
   return Results.Ok();

});

app.MapDelete("items/{id}",([FromServices] ItemRepository items ,int id)=> {
if (items.GetById(id) != null)
    {
        return Results.NotFound();
    }
    items.Delete(id);
   return Results.Ok();

});

app.MapPut("items/{id}",([FromServices] ItemRepository items,int id,Item item) =>{
    if (items.GetById(id)==null)
    {
        return Results.NotFound();
    }
    items.Update(item);
    return Results.Ok(item);
});

app.MapGet("/",()=> "Hello from minimal API");
app.Run();

record Item (int id,string title,bool isComplete);
class ItemRepository{
    private Dictionary<int,Item> items = new Dictionary<int, Item>();
    public ItemRepository(){
        var item1 = new Item(1,"Go to GYM",false);
        var item2 = new Item(2,"Drink Water",true);
        var item3 = new Item(3,"Watch TV",false);
        items.Add(item1.id,item1);
        items.Add(item2.id,item2);
        items.Add(item3.id,item3);
    }
    public IEnumerable<Item> GetAll()=> items.Values;
    public Item GetById(int id){
        if (items.ContainsKey(id))
        {
            return items[id];
        }
        return null;
    }
    public void Add(Item item) => items.Add(item.id,item);
    public void Update(Item item)=> items[item.id] = item;
    public void Delete (int id)=> items.Remove(id);
}

class ApiDbContext : DbContext{
    public DbSet<Item> Items { get; set; }
    public ApiDbContext(DbContextOptions<ApiDbContext> options):base (options)
    {
        
    }
}
// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateTime.Now.AddDays(index),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.Run();

// record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }