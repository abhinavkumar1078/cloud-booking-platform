using MassTransit;
using Shared.Contracts;

public class BookingCreatedConsumer : IConsumer<BookingCreated>
{
    public async Task Consume(ConsumeContext<BookingCreated> context)
    {
        // simulate payment processing
        var bookingId = context.Message.BookingId;
        Console.WriteLine($"[PaymentService] Received BookingCreated for {bookingId}. Processing payment...");

        // simulate async work
        await Task.Delay(500);

        var amount = 123.45m; // in real app compute price
        await context.Publish(new PaymentConfirmed(bookingId, amount, DateTime.UtcNow));

        Console.WriteLine($"[PaymentService] PaymentConfirmed published for {bookingId}");
    }
}
