using System.ComponentModel;

namespace Models.Enums
{
    public enum BookingStatus
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Confirmed")]
        Confirmed = 1,
        [Description("Canceled")]
        Canceled = 2,
    }
}
