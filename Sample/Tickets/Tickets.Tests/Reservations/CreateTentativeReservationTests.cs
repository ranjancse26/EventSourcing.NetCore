using System;
using FluentAssertions;
using Tickets.Reservations;
using Tickets.Reservations.Events;
using Tickets.Tests.Extensions;
using Tickets.Tests.Extensions.Reservations;
using Tickets.Tests.Stubs.Ids;
using Tickets.Tests.Stubs.Reservations;
using Xunit;

namespace Tickets.Tests.Reservations
{
    public class CreateTentativeReservationTests
    {
        [Fact]
        public void ForValidParams_ShouldCreateReservationWithTentativeStatus()
        {
            // Given
            var idGenerator = new FakeAggregateIdGenerator<Reservation>();
            var numberGenerator = new FakeReservationNumberGenerator();
            var seatId = Guid.NewGuid();

            // When
            var reservation = Reservation.CreateTentative(
                idGenerator,
                numberGenerator,
                seatId
            );

            // Then
            idGenerator.LastGeneratedId.Should().NotBeNull();
            numberGenerator.LastGeneratedNumber.Should().NotBeNull();

            reservation
                .IsTentativeReservationWith(
                    idGenerator.LastGeneratedId.Value,
                    numberGenerator.LastGeneratedNumber,
                    seatId
                )
                .HasTentativeReservationCreatedEventWith(
                    idGenerator.LastGeneratedId.Value,
                    numberGenerator.LastGeneratedNumber,
                    seatId
                );
        }
    }
}
