using FindMyLocation.Service.Contract;
using System;

namespace FindMyLocation.Service.Implementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}