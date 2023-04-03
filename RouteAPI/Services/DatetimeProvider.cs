using RouteAPI.Interfaces;

namespace RouteAPI.Services
{
    public class DatetimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
