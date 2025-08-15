using BookingService.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Contracts.Domain;
using StackExchange.Redis;

namespace BookingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly BookingDbContext _db;
    private readonly IPublishEndpoint _publish;
    private readonly IDatabase _redis;

    public BookingController(BookingDbContext db, IPublishEndpoint publish, IConnectionMultiplexer redis)
    {
        _db = db;
        _publish = publish;
        _redis = redis.GetDatabase();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            CustomerName = dto.CustomerName,
            BookingDate = dto.BookingDate,
            Price = dto.Price
        };

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        // cache a simple serialized value (optional)
        await _redis.StringSetAsync($"booking:{booking.Id}", $"{booking.CustomerName}|{booking.BookingDate:o}");

        // publish event
        await _publish.Publish(new BookingCreated(booking.Id, booking.CustomerName, booking.BookingDate));

        return CreatedAtAction(nameof(Get), new { id = booking.Id }, booking);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        // try cache
        var cached = await _redis.StringGetAsync($"booking:{id}");
        if (cached.HasValue)
        {
            var parts = cached.ToString().Split('|');
            return Ok(new { Id = id, CustomerName = parts[0], BookingDate = DateTime.Parse(parts[1]) });
        }

        var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null) return NotFound();

        return Ok(booking);
    }
}

public record CreateBookingDto(string CustomerName, DateTime BookingDate, decimal Price);
