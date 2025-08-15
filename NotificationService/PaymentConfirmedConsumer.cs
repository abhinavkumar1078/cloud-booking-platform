using MassTransit;
using Shared.Contracts;

namespace NotificationService
{
    public class PaymentConfirmedConsumer : IConsumer<PaymentConfirmed>
    {
        public Task Consume(ConsumeContext<PaymentConfirmed> context)
        {
            var bookingId = context.Message.BookingId;
            var amount = context.Message.Amount;
            Console.WriteLine($"[NotificationService] Payment confirmed for {bookingId}, amt: {amount}. Sending notification (simulated).");
            // Here you would call Mailjet/SMTP etc. For now just log.
            return Task.CompletedTask;
        }
    }
}
