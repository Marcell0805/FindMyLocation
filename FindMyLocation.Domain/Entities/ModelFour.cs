using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyLocation.Domain.Entities
{
    [Keyless]
    public class Icon
    {
        public string prefix { get; set; }
        public string suffix { get; set; }
    }
    [Keyless]
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public Icon icon { get; set; }
    }
    [Keyless]
    public class Main
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
    [Keyless]
    public class Geocodes
    {
        public Main main { get; set; }
    }
    [Keyless]
    public class Location
    {
        public string address { get; set; }
        public string country { get; set; }
        public string cross_street { get; set; }
        public string locality { get; set; }
        //public List<string> neighborhood { get; set; }
        public string po_box { get; set; }
        public string post_town { get; set; }
        public string postcode { get; set; }
        public string region { get; set; }
    }

    public class RelatedPlaces
    {
    }
    [Keyless]
    public class Result
    {
        public string fsq_id { get; set; }
        public List<Category> categories { get; set; }
        //public List<object> chains { get; set; }
        public int distance { get; set; }
        public Geocodes geocodes { get; set; }
        public Location location { get; set; }
        public string name { get; set; }
        public RelatedPlaces related_places { get; set; }
        public string timezone { get; set; }
    }
    [Keyless]
    public class ModelFour
    {
        public int locationId { get; set; }
        public List<Result> results { get; set; }
    }

}
