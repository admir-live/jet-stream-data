using JetStreamData.FlightsService.Domain.Entities;
using JetStreamData.FlightsService.Domain.Enums;

namespace JetStreamData.FlightsService.Infrastructure.Seed;

public static class Seed
{
    public static IEnumerable<FlightInformation> Get => new[]
    {
        new FlightInformation(
            new Guid("ed418aed-8efc-4496-bd08-c6483674411d"),
            new Flight("KL562", FlightStatus.Cancelled),
            new Airport("Tokyo", "Asia/Tokyo", "HND", "RJTT"),
            new Airport("New York", "America/New_York", "JFK", "KJFK"),
            new Airline("Delta Air Lines", "DL", "DAL"),
            new Schedule(new DateTime(2022, 1, 10, 0, 56, 0), new DateTime(2022, 1, 10, 9, 56, 0), new DateTime(2022, 1, 10, 0, 56, 0)),
            new Schedule(new DateTime(2022, 1, 10, 9, 56, 0), new DateTime(2022, 1, 10, 9, 56, 0), new DateTime(2022, 1, 10, 9, 56, 0))
        ),
        new FlightInformation(
            new Guid("8b8221be-ba14-4f86-bc8e-0436d477d477"),
            new Flight("QF695", FlightStatus.Delayed),
            new Airport("San Francisco", "America/Los_Angeles", "SFO", "KSFO"),
            new Airport("Heathrow", "Europe/London", "LHR", "EGLL"),
            new Airline("Singapore Airlines", "SQ", "SIA"),
            new Schedule(new DateTime(2022, 12, 24, 5, 58, 0), new DateTime(2022, 12, 24, 13, 58, 0), new DateTime(2022, 12, 24, 5, 58, 0)),
            new Schedule(new DateTime(2022, 12, 24, 13, 58, 0), new DateTime(2022, 12, 24, 13, 58, 0), new DateTime(2022, 12, 24, 13, 58, 0))
        ),
        new FlightInformation(
            new Guid("581e4526-1eb3-4917-8dee-6ba10faae15f"),
            new Flight("EK159", FlightStatus.InFlight),
            new Airport("Tokyo", "Asia/Tokyo", "HND", "RJTT"),
            new Airport("Singapore", "Asia/Singapore", "SIN", "WSSS"),
            new Airline("British Airways", "BA", "BAW"),
            new Schedule(new DateTime(2022, 8, 28, 8, 18, 0), new DateTime(2022, 8, 28, 17, 18, 0), new DateTime(2022, 8, 28, 8, 18, 0)),
            new Schedule(new DateTime(2022, 8, 28, 17, 18, 0), new DateTime(2022, 8, 28, 17, 18, 0), new DateTime(2022, 8, 28, 17, 18, 0))
        ),
        new FlightInformation(
            new Guid("26ef68c4-2eb9-4347-81fc-258e97b6da29"),
            new Flight("UA935", FlightStatus.Scheduled),
            new Airport("Chicago", "America/Chicago", "ORD", "KORD"),
            new Airport("Frankfurt", "Europe/Berlin", "FRA", "EDDF"),
            new Airline("United Airlines", "UA", "UAL"),
            new Schedule(new DateTime(2023, 03, 15, 13, 10, 0), new DateTime(2023, 03, 16, 0, 25, 0), new DateTime(2023, 03, 15, 13, 10, 0)),
            new Schedule(new DateTime(2023, 03, 16, 0, 25, 0), new DateTime(2023, 03, 16, 0, 25, 0), new DateTime(2023, 03, 16, 0, 25, 0))
        ),
        new FlightInformation(
            new Guid("835e9679-a8ac-4021-93c2-a694d139018f"),
            new Flight("AC112", FlightStatus.Landed),
            new Airport("Toronto", "America/Toronto", "YYZ", "CYYZ"),
            new Airport("Sydney", "Australia/Sydney", "SYD", "YSSY"),
            new Airline("Air Canada", "AC", "ACA"),
            new Schedule(new DateTime(2023, 05, 28, 22, 30, 0), new DateTime(2023, 05, 29, 17, 55, 0), new DateTime(2023, 05, 28, 22, 30, 0)),
            new Schedule(new DateTime(2023, 05, 29, 17, 55, 0), new DateTime(2023, 05, 29, 17, 55, 0), new DateTime(2023, 05, 29, 17, 55, 0))
        ),
        new FlightInformation(
            new Guid("66358648-d101-42e9-8371-561589179ac8"),
            new Flight("BA283", FlightStatus.InFlight),
            new Airport("London", "Europe/London", "LGW", "EGKK"),
            new Airport("Los Angeles", "America/Los_Angeles", "LAX", "KLAX"),
            new Airline("British Airways", "BA", "BAW"),
            new Schedule(new DateTime(2023, 04, 21, 10, 20, 0), new DateTime(2023, 04, 21, 23, 10, 0), new DateTime(2023, 04, 21, 10, 20, 0)),
            new Schedule(new DateTime(2023, 04, 21, 23, 10, 0), new DateTime(2023, 04, 21, 23, 10, 0), new DateTime(2023, 04, 21, 23, 10, 0))
        ),
        new FlightInformation(
            new Guid("20e8e5d6-29ec-49fa-8f68-6062c4211630"),
            new Flight("LH400", FlightStatus.Delayed),
            new Airport("Frankfurt", "Europe/Berlin", "FRA", "EDDF"),
            new Airport("New York", "America/New_York", "JFK", "KJFK"),
            new Airline("Lufthansa", "LH", "DLH"),
            new Schedule(new DateTime(2023, 09, 03, 16, 15, 0), new DateTime(2023, 09, 03, 19, 30, 0), new DateTime(2023, 09, 03, 17, 00, 0)), //Delayed
            new Schedule(new DateTime(2023, 09, 03, 19, 30, 0), new DateTime(2023, 09, 03, 19, 30, 0), new DateTime(2023, 09, 03, 19, 30, 0))
        ),
        new FlightInformation(
            new Guid("98120a99-0b87-417c-ae86-a2a8a052e627"),
            new Flight("NZ147", FlightStatus.Scheduled),
            new Airport("Auckland", "Pacific/Auckland", "AKL", "NZAA"),
            new Airport("Dubai", "Asia/Dubai", "DXB", "OMDB"),
            new Airline("Emirates", "EK", "UAE"),
            new Schedule(new DateTime(2023, 07, 12, 17, 50, 0), new DateTime(2023, 07, 13, 10, 25, 0), new DateTime(2023, 07, 12, 17, 50, 0)),
            new Schedule(new DateTime(2023, 07, 13, 10, 25, 0), new DateTime(2023, 07, 13, 10, 25, 0), new DateTime(2023, 07, 13, 10, 25, 0))
        ),
        new FlightInformation(
            new Guid("d1d48b28-656f-41b8-92eb-8c6e210431ba"),
            new Flight("JL38", FlightStatus.Landed),
            new Airport("Tokyo", "Asia/Tokyo", "NRT", "RJAA"),
            new Airport("London", "Europe/London", "LHR", "EGLL"),
            new Airline("Japan Airlines", "JL", "JAL"),
            new Schedule(new DateTime(2023, 02, 20, 11, 40, 0), new DateTime(2023, 02, 20, 17, 25, 0), new DateTime(2023, 02, 20, 11, 40, 0)),
            new Schedule(new DateTime(2023, 02, 20, 17, 25, 0), new DateTime(2023, 02, 20, 17, 25, 0), new DateTime(2023, 02, 20, 17, 25, 0))
        ),
        new FlightInformation(
            new Guid("51298d30-a2f8-41d1-a0a6-c1593410a5e0"),
            new Flight("AA106", FlightStatus.InFlight),
            new Airport("Miami", "America/New_York", "MIA", "KMIA"),
            new Airport("Paris", "Europe/Paris", "CDG", "LFPG"),
            new Airline("American Airlines", "AA", "AAL"),
            new Schedule(new DateTime(2023, 06, 08, 20, 15, 0), new DateTime(2023, 06, 09, 9, 35, 0), new DateTime(2023, 06, 08, 20, 15, 0)),
            new Schedule(new DateTime(2023, 06, 09, 9, 35, 0), new DateTime(2023, 06, 09, 9, 35, 0), new DateTime(2023, 06, 09, 9, 35, 0))
        ),
        new FlightInformation(
            new Guid("1858ef3a-1065-41f7-9502-a1a5023c13e9"),
            new Flight("AF1380", FlightStatus.Cancelled),
            new Airport("Paris", "Europe/Paris", "CDG", "LFPG"),
            new Airport("Mexico City", "America/Mexico_City", "MEX", "MMMX"),
            new Airline("Air France", "AF", "AFR"),
            new Schedule(new DateTime(2023, 05, 16, 15, 45, 0), new DateTime(2023, 05, 16, 23, 10, 0), new DateTime(2023, 05, 16, 15, 45, 0)),
            new Schedule(new DateTime(2023, 05, 16, 23, 10, 0), new DateTime(2023, 05, 16, 23, 10, 0), new DateTime(2023, 05, 16, 23, 10, 0))
        ),
        new FlightInformation(
            new Guid("e8f43c30-3f21-4560-91f4-8988ef118291"),
            new Flight("CX251", FlightStatus.Scheduled),
            new Airport("Hong Kong", "Asia/Hong_Kong", "HKG", "VHHH"),
            new Airport("New York", "America/New_York", "JFK", "KJFK"),
            new Airline("Cathay Pacific", "CX", "CPA"),
            new Schedule(new DateTime(2023, 11, 01, 1, 25, 0), new DateTime(2023, 11, 01, 18, 35, 0), new DateTime(2023, 11, 01, 1, 25, 0)),
            new Schedule(new DateTime(2023, 11, 01, 18, 35, 0), new DateTime(2023, 11, 01, 18, 35, 0), new DateTime(2023, 11, 01, 18, 35, 0))
        ),
        new FlightInformation(
            new Guid("a55a6175-c4ee-4357-8601-37f613c6e8a4"),
            new Flight("SA232", FlightStatus.InFlight),
            new Airport("Johannesburg", "Africa/Johannesburg", "JNB", "FAOR"),
            new Airport("New York", "America/New_York", "JFK", "KJFK"),
            new Airline("South African Airways", "SA", "SAA"),
            new Schedule(new DateTime(2023, 10, 26, 20, 55, 0), new DateTime(2023, 10, 27, 9, 20, 0), new DateTime(2023, 10, 26, 20, 55, 0)),
            new Schedule(new DateTime(2023, 10, 27, 9, 20, 0), new DateTime(2023, 10, 27, 9, 20, 0), new DateTime(2023, 10, 27, 9, 20, 0))
        ),
        new FlightInformation(
            new Guid("a02339f8-1102-430f-b0d7-353192a17209"),
            new Flight("EY451", FlightStatus.Scheduled),
            new Airport("Abu Dhabi", "Asia/Dubai", "AUH", "OMAA"),
            new Airport("New York", "America/New_York", "JFK", "KJFK"),
            new Airline("Etihad Airways", "EY", "ETD"),
            new Schedule(new DateTime(2023, 04, 05, 2, 10, 0), new DateTime(2023, 04, 05, 16, 55, 0), new DateTime(2023, 04, 05, 2, 10, 0)),
            new Schedule(new DateTime(2023, 04, 05, 16, 55, 0), new DateTime(2023, 04, 05, 16, 55, 0), new DateTime(2023, 04, 05, 16, 55, 0))
        ),
        new FlightInformation(
            new Guid("e0578220-780c-40e3-a121-60b832127832"),
            new Flight("TK76", FlightStatus.Landed),
            new Airport("Istanbul", "Europe/Istanbul", "IST", "LTFM"),
            new Airport("Sao Paulo", "America/Sao_Paulo", "GRU", "SBGR"),
            new Airline("Turkish Airlines", "TK", "THY"),
            new Schedule(new DateTime(2023, 03, 30, 22, 45, 0), new DateTime(2023, 03, 31, 10, 40, 0), new DateTime(2023, 03, 30, 22, 45, 0)),
            new Schedule(new DateTime(2023, 03, 31, 10, 40, 0), new DateTime(2023, 03, 31, 10, 40, 0), new DateTime(2023, 03, 31, 10, 40, 0))
        ),
        new FlightInformation(
            new Guid("a010a89b-da2c-4fe4-a7a3-50089118a54e"),
            new Flight("AZ611", FlightStatus.Delayed),
            new Airport("Rome", "Europe/Rome", "FCO", "LIRF"),
            new Airport("New York", "America/New_York", "JFK", "KJFK"),
            new Airline("Alitalia", "AZ", "AZA"),
            new Schedule(new DateTime(2023, 12, 15, 11, 00, 0), new DateTime(2023, 12, 15, 16, 40, 0), new DateTime(2023, 12, 15, 12, 30, 0)), // Delayed
            new Schedule(new DateTime(2023, 12, 15, 16, 40, 0), new DateTime(2023, 12, 15, 16, 40, 0), new DateTime(2023, 12, 15, 16, 40, 0))
        ),
        new FlightInformation(
            new Guid("45e0e6e0-6195-4b52-91e5-72ea524571c5"),
            new Flight("VS46", FlightStatus.InFlight),
            new Airport("London", "Europe/London", "LHR", "EGLL"),
            new Airport("Delhi", "Asia/Kolkata", "DEL", "VIDP"),
            new Airline("Virgin Atlantic", "VS", "VIR"),
            new Schedule(new DateTime(2023, 01, 18, 21, 40, 0), new DateTime(2023, 01, 19, 11, 35, 0), new DateTime(2023, 01, 18, 21, 40, 0)),
            new Schedule(new DateTime(2023, 01, 19, 11, 35, 0), new DateTime(2023, 01, 19, 11, 35, 0), new DateTime(2023, 01, 19, 11, 35, 0))
        ),
        new FlightInformation(
            new Guid("770e5d19-d817-4444-ae25-60d23bf602df"),
            new Flight("MH140", FlightStatus.Scheduled),
            new Airport("Kuala Lumpur", "Asia/Kuala_Lumpur", "KUL", "WMKK"),
            new Airport("London", "Europe/London", "LHR", "EGLL"),
            new Airline("Malaysia Airlines", "MH", "MAS"),
            new Schedule(new DateTime(2023, 07, 01, 1, 15, 0), new DateTime(2023, 07, 01, 9, 05, 0), new DateTime(2023, 07, 01, 1, 15, 0)),
            new Schedule(new DateTime(2023, 07, 01, 9, 05, 0), new DateTime(2023, 07, 01, 9, 05, 0), new DateTime(2023, 07, 01, 9, 05, 0))
        ),
        new FlightInformation(
            new Guid("18b0e856-f816-4d20-9b29-1b47e8991e52"),
            new Flight("PR301", FlightStatus.Landed),
            new Airport("Manila", "Asia/Manila", "MNL", "RPLL"),
            new Airport("San Francisco", "America/Los_Angeles", "SFO", "KSFO"),
            new Airline("Philippine Airlines", "PR", "PAL"),
            new Schedule(new DateTime(2023, 11, 17, 23, 50, 0), new DateTime(2023, 11, 18, 15, 40, 0), new DateTime(2023, 11, 17, 23, 50, 0)),
            new Schedule(new DateTime(2023, 11, 18, 15, 40, 0), new DateTime(2023, 11, 18, 15, 40, 0), new DateTime(2023, 11, 18, 15, 40, 0)))
    };
}
