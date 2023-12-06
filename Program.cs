using CarsRUsServer.Models;
using CarsRUsServer.Models.DTOs;


 List<PaintColor> paintColors = new List<PaintColor>
        {
            new PaintColor { Id = 1, Price = 500, Color = "Silver" },
            new PaintColor { Id = 2, Price = 600, Color = "Midnight Blue" },
            new PaintColor { Id = 3, Price = 700, Color = "Firebrick Red" },
            new PaintColor { Id = 4, Price = 800, Color = "Spring Green" }
        };

        List<Interior> interiors = new List<Interior>
        {
            new Interior { Id = 1, Price = 1000, Material = "Beige Fabric" },
            new Interior { Id = 2, Price = 1200, Material = "Charcoal Fabric" },
            new Interior { Id = 3, Price = 1500, Material = "White Leather" },
            new Interior { Id = 4, Price = 1800, Material = "Black Leather" }
        };

        List<Technology> technologies = new List<Technology>
        {
            new Technology { Id = 1, Price = 2000, Package = "Basic Package" },
            new Technology { Id = 2, Price = 2500, Package = "Navigation Package" },
            new Technology { Id = 3, Price = 3000, Package = "Visibility Package" },
            new Technology { Id = 4, Price = 3500, Package = "Ultra Package" }
        };

        List<Wheels> wheels = new List<Wheels>
        {
            new Wheels { Id = 1, Price = 800, Style = "17-inch Pair Radial" },
            new Wheels { Id = 2, Price = 900, Style = "17-inch Pair Radial Black" },
            new Wheels { Id = 3, Price = 1000, Style = "18-inch Pair Spoke Silver" },
            new Wheels { Id = 4, Price = 1100, Style = "18-inch Pair Spoke Black" }
        };

        List<Order> orders = new List<Order>
{
    new Order
    {
        Id = 1,
        Timestamp = new DateTime(2009, 11, 11, 13, 30, 0, DateTimeKind.Local),
        WheelId = 2,
        TechnologyId = 3,
        PaintId = 1,
        InteriorId = 4
    },
    new Order
    {
        Id = 2,
        Timestamp = new DateTime(2015, 6, 24, 10, 15, 0, DateTimeKind.Local),
        WheelId = 1,
        TechnologyId = 1,
        PaintId = 3,
        InteriorId = 2
    },
    new Order
    {
        Id = 3,
        Timestamp = new DateTime(2020, 3, 17, 16, 45, 0, DateTimeKind.Local),
        WheelId = 3,
        TechnologyId = 4,
        PaintId = 2,
        InteriorId = 1
    }
};



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/wheels", () =>
{
    return wheels.Select(w => new WheelsDTO
    {
        Id = w.Id,
        Price = w.Price,
        Style = w.Style
    });
});

app.MapGet("/technologies", () =>
{
    return technologies.Select(t => new TechnologyDTO
    {
        Id = t.Id,
        Price = t.Price,
        Package = t.Package
    });
});

app.MapGet("/interiors", () =>
{
    return interiors.Select(i => new InteriorDTO
    {
        Id = i.Id,
        Price = i.Price,
        Material = i.Material
    });
});


app.MapGet("/paintcolors", () =>
{
    return paintColors.Select(p => new PaintColorDTO
    {
        Id = p.Id,
        Price = p.Price,
        Color = p.Color
    });
});


app.MapGet("/orders", () =>
{
    return orders.Select(o => new OrderDTO
    {
        Id = o.Id,
        Timestamp = o.Timestamp,
        WheelId = o.WheelId,
        Wheel = wheels.FirstOrDefault(w => w.Id == o.WheelId) != null
            ? new WheelsDTO
            {
                Id = o.WheelId,
                Price = wheels.First(w => w.Id == o.WheelId).Price,
                Style = wheels.First(w => w.Id == o.WheelId).Style
            }
            : null,
        TechnologyId = o.TechnologyId,
        PaintId = o.PaintId,
        InteriorId = o.InteriorId
    });
});

app.MapPost("/orders", (Order order) =>
{
    // creates a new id (SQL will do this for us like JSON Server did!)
    order.Id = orders.Max(o => o.Id) + 1;
    orders.Add(order);

    // Created returns a 201 status code with a link in the headers to where the new resource can be accessed
    return Results.Created($"/orders/{order.Id}", new OrderDTO
    {
        Id = order.Id,
        Timestamp = DateTime.Now, // This will be the current time
        WheelId = order.WheelId,
        TechnologyId = order.TechnologyId,
        PaintId = order.PaintId,
        InteriorId = order.InteriorId
    });
});




app.Run();

