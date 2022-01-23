using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyLocation.Domain.Entities
{
    public class FourSqaureVenues
    {
        [Key]
        public int locationId { get; set; }
        public string fsq_id { get; set; }
        //Catergory
        public int id { get; set; }
        public string name { get; set; }
        //Icon
        public string prefix { get; set; }
        public string suffix { get; set; }
        //------
        //public List<object> chains { get; set; }
        public int distance { get; set; }
        //Geo codes

        public double latitude { get; set; }
        public double longitude { get; set; }
        //-----------------------
        //Location
        public string address { get; set; }
        public string country { get; set; }
        public string cross_street { get; set; }
        public string locality { get; set; }
        //public List<string> neighborhood { get; set; }
        public string po_box { get; set; }
        public string post_town { get; set; }
        public string postcode { get; set; }
        public string region { get; set; }
        //----------------------
        public string locationName { get; set; }
        public string timezone { get; set; }
    }
}
