
namespace CarsRUsServer.Models.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int WheelId { get; set; }
        public int TechnologyId { get; set; }
        public int PaintId { get; set; }
        public int InteriorId { get; set; }
        public WheelsDTO Wheel { get; set; }
    }
}
