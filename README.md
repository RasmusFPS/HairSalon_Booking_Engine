# HairSalon Booking Engine

A RESTful ASP.NET Core Web API for managing bookings at a hair salon. The system handles customers, stylists, treatments, schedules, and bookings — including availability checking, conflict detection, and status management.

---

## Tech Stack

- **ASP.NET Core** – Web API framework
- **Entity Framework Core** – ORM with SQL Server
- **FluentValidation** – Request validation
- **MSTest + EF In-Memory** – Unit testing

---

## Domain Overview

| Entity | Description |
|---|---|
| `Customer` | A person who makes bookings |
| `Stylist` | A salon employee who performs treatments |
| `Treatment` | A service offered (e.g. haircut, balayage) with a price and duration |
| `Schedule` | A stylist's working hours for a given day of the week, including lunch break |
| `Booking` | Connects a customer and stylist to one or more treatments at a specific time |

Booking statuses: `Pending`, `Confirmed`, `Completed`, `Cancelled`

---

## Project Structure

```
HairSalon_Booking_Engine/
├── Controllers/
│   ├── BookingController.cs
│   └── CustomerController.cs
├── Services/
│   ├── BookingService.cs
│   └── CustomerService.cs
├── Models/
│   ├── DTOs/           # Request and response records
│   └── ...             # Entity models
├── Mappings/           # Extension methods for entity → DTO mapping
├── Validation/         # FluentValidation validators
├── SeedData.cs         # Initial database seed data
└── Program.cs

HairSalon_Booking_Engine.Tests/
├── ServiceTests/
│   ├── BookingServiceTest.cs
│   └── CustomerServiceTest.cs
└── TestData/
    ├── DbContextFactory.cs     # In-memory DB setup
    └── TestDataBuilder.cs      # Test fixture helpers
```

---

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (local or remote)

### Setup

1. Clone the repository.

2. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "Default": "Server=...;Database=HairSalonDB;..."
   }
   ```

3. Apply migrations and seed the database:
   ```bash
   dotnet ef database update
   ```

4. Run the API:
   ```bash
   dotnet run
   ```

Swagger UI is available at `https://localhost:{port}/swagger` when running in Development mode.

---

## API Endpoints

### Bookings — `/api/booking`

| Method | Route | Description |
|---|---|---|
| GET | `/api/booking` | Get all bookings |
| GET | `/api/booking/{id}` | Get booking by ID |
| GET | `/api/booking/search` | Filter bookings by date, stylist, customer, status, sort |
| GET | `/api/booking/week?weekStart=` | Get all bookings for a given week |
| GET | `/api/booking/month?year=&month=` | Get all bookings for a given month |
| GET | `/api/booking/available-times?date=&stylistId=` | Get available hourly time slots for a stylist |
| POST | `/api/booking/create` | Create a new booking |
| PUT | `/api/booking/{id}/update` | Update an existing booking |
| DELETE | `/api/booking/{id}/delete` | Delete a booking |
| PATCH | `/api/booking/{id}/cancel` | Cancel a booking |
| PATCH | `/api/booking/{id}/reschedule` | Reschedule a booking (optionally change stylist) |
| PATCH | `/api/booking/{id}/change-status` | Manually change booking status |

### Customers — `/api/customer`

| Method | Route | Description |
|---|---|---|
| GET | `/api/customer` | Get all customers |
| GET | `/api/customer/{id}` | Get customer by ID |
| GET | `/api/customer/{id}/booking-history` | Get a customer's full booking history |
| POST | `/api/customer/create` | Create a new customer |
| PUT | `/api/customer/{id}/update` | Update a customer |
| DELETE | `/api/customer/{id}/delete` | Delete a customer |

---

## Booking Logic

When a booking is created or updated, the service:

1. Verifies all requested treatments exist.
2. Calculates the end time by summing treatment durations.
3. Checks that the stylist has a schedule covering the full time slot (excluding lunch).
4. Checks that the stylist has no conflicting active bookings during that period.

Business rules enforced in the service layer:

- Completed or cancelled bookings cannot be updated.
- A completed booking cannot be cancelled.
- A rescheduled booking must be set to a future time.
- A booking must always have at least one treatment.

---

## Validation

All create and update requests are validated with FluentValidation before reaching the service layer.

**Booking rules:** start time must be in the future; stylist, customer, and at least one treatment ID are required.

**Customer rules:** first/last name 2–50 characters, letters only; phone in E.164 format; email up to 254 characters.

---

## Seed Data

The database seeds with:

- **3 stylists:** Sofia Andersson, Marcus Lindqvist, Isabelle Karlsson
- **5 customers:** Emma, Lucas, Maja, Oliver, Astrid
- **9 treatments:** ranging from a Men's Cut (30 min, 350 kr) to a Keratin Treatment (150 min, 1800 kr)
- **Schedules:** Mon–Fri coverage with at least one stylist per day, 09:00–17:00 with a 12:00 lunch break
- **30 sample bookings** across June 1–15, 2026

---

## Running Tests

Tests use MSTest with an EF Core in-memory database — no SQL Server required.

```bash
dotnet test
```

Each test method creates its own isolated in-memory database (named after the test method) to prevent state leakage between tests. The `TestDataBuilder` helper class provides factory methods for all entity and DTO types.

---

Here is the English translation of your Test Strategy document. You can drop this straight into your documentation or `README.md`!

***

# Test Strategy

## Purpose

The purpose of the testing is to ensure that the system functions correctly, handles errors safely.

Testing focuses on:

* Functionality
* Validations
* API responses
* Error handling

---

## Test Areas

* Controllers
* Services
* Validations

---

## Types of Tests

### Unit Tests

Unit tests are used to test individual components in isolation.

The tests verify, among other things, that:

* Methods return the correct data
* Errors are handled properly
* Validations work according to requirements

### Controller Tests

Controller tests are used to ensure that API endpoints:

* Return the correct HTTP status codes
* Handle requests correctly
* Return the correct response data

Examples of tested status codes:

* `200 OK`
* `201 Created`
* `400 Bad Request`
* `404 Not Found`

### Integration Tests (Postman)

Integration tests are used to ensure that the entire API communicates correctly with the database in a real-world flow.

* Verifies complete user flows (Create customer ➔ Book appointment ➔ Cancel appointment)
* Database communication tests

---

## Test Methodology

### Happy Path

Tests where we simulate a user doing everything correctly. 
*Examples from the project:* The customer books a haircut in the future, an available stylist is fetched correctly, or a customer chooses to cancel their appointment (which successfully updates the booking status to `Cancelled`).

### Sad Path

Tests where we act as a "careless" or malicious user to ensure that the system functions securely and catches these errors. 
*Examples from the project:* Attempting to book a time that has already passed, requesting a customer with ID 9999, submitting a booking that completely lacks Treatments, or attempting to delete a resource that has already been removed.

---

## Mocking and Test Tools

The following tools and frameworks are used in testing:

* **MSTest** (Testing framework)
* **Moq** (Mocking framework)
* **EF Core In-Memory** (Test database)
* **Postman** (Integration tests)

Moq is used for mocking dependencies (such as `IBookingService`) to be able to test the system's Controllers in isolation. Entity Framework In-Memory Database and our custom `TestDataBuilder` are used to safely test database calls in the Service layer without affecting a real database. Performance tests have been omitted at this stage as they are classified as an extra assignment, allowing us to place full focus on the quality of the core functionality.
