namespace GBC_Travel_Group_32.Models {
    public class FilterViewModel {
        
        public string? SearchTerm { get; set; }
        public bool Available { get; set; }
        public bool Unavailable { get; set; }
        public bool FlightDate { get; set; }
        public bool Location { get; set; }
        public bool Destination { get; set; }
        public bool Model { get; set; }
        public bool Manufacturer { get; set; }
        public bool MinPrice { get; set; }
        public bool MaxPrice { get; set; }
    }
}