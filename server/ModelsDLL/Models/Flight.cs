using FlightSimulator.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlightSimulator.Models
{
    public class Flight
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public FlightType FType { get; set; }
        public int? StopId { get; set; }
        [ForeignKey("StopId")]
        [JsonIgnore]
        public virtual Stop? Stop { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
