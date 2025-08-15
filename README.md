# Cloud Booking Platform (starter)

Run local infra:
docker compose up -d

Run BookingService:
cd BookingService
dotnet run

Run PaymentService:
cd PaymentService
dotnet run

Run NotificationService:
cd NotificationService
dotnet run

Create a booking (example):
POST http://localhost:5000/api/booking
{ "customerName":"John", "bookingDate":"2025-09-01T10:00:00Z", "price":100.0 }
