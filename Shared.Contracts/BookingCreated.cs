namespace Shared.Contracts
{
    public record BookingCreated(Guid BookingId, string CustomerName, DateTime BookingDate);
   
}
