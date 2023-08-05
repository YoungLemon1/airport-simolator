using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FlightSimulator.Models.Enums
{
    public enum FlightType
    {
        [Display(Name = "Landing")]
        landing,
        [Display(Name = "Takeoff")]
        takeoff
    }
}
