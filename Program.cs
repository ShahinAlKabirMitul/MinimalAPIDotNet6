var app = WebApplication.CreateBuilder(args).Build();
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
    public Item GetById(int id)=> items[id];
    public void Add(Item item) => items.Add(item.id,item);
    public void Update(Item item)=> items[item.id] = item;
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