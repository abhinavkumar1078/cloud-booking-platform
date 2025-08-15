namespace Shared.Contracts
{
    public record PaymentConfirmed(Guid BookingId, decimal Amount, DateTime PaymentDate);
}
