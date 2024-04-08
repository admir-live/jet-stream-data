// Copyright (c) Admir Mujkic for Journey Mentor Cyprus LTD.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using JetStreamData.FlightsService.Domain.Enums;
using JetStreamData.Kernel.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Entities;

public sealed class Flight(
    string number,
    FlightStatus status) : Entity<Guid>
{
    private Flight() : this("N/A", FlightStatus.Unknown)
    {
    }

    public string Number { get; private set; } = number ?? throw new ArgumentNullException(nameof(number));
    public FlightStatus Status { get; private set; } = status ?? throw new ArgumentNullException(nameof(status));
}
