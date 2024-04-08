// Copyright (c) Admir Mujkic for Journey Mentor Cyprus LTD. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using JetStreamData.Kernel.Domain.Enums;

namespace JetStreamData.FlightsService.Domain.Enums;

public sealed class FlightStatus(
    int id,
    string name) : Enumeration(id, name)
{
    public static readonly FlightStatus Unknown = new(0, nameof(Unknown));
    public static readonly FlightStatus Scheduled = new(1, nameof(Scheduled));
    public static readonly FlightStatus Landed = new(2, nameof(Landed));
    public static readonly FlightStatus Departed = new(3, nameof(Departed));
    public static readonly FlightStatus InFlight = new(4, nameof(InFlight));
    public static readonly FlightStatus Delayed = new(5, nameof(Delayed));
    public static readonly FlightStatus Arrived = new(6, nameof(Arrived));
    public static readonly FlightStatus Cancelled = new(7, nameof(Cancelled));

    private FlightStatus() : this(Unknown.Id, Unknown.Name)
    {
    }

    public static FlightStatus Parse(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        value = value.Trim().ToLowerInvariant();
        return value switch
        {
            "unknown" => Unknown,
            "scheduled" => Scheduled,
            "landed" => Landed,
            "departed" => Departed,
            "inflight" => InFlight,
            "delayed" => Delayed,
            "arrived" => Arrived,
            "cancelled" => Cancelled,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static FlightStatus Parse(int value)
    {
        return value switch
        {
            0 => Unknown,
            1 => Scheduled,
            2 => Landed,
            3 => Departed,
            4 => InFlight,
            5 => Delayed,
            6 => Arrived,
            7 => Cancelled,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static implicit operator FlightStatus(int value)
    {
        return Parse(value);
    }

    public static implicit operator FlightStatus(string value)
    {
        return Parse(value);
    }

    public static implicit operator int(FlightStatus value)
    {
        return value.Id;
    }

    public static implicit operator string(FlightStatus value)
    {
        return value.ToString();
    }
}
