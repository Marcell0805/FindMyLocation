using System.ComponentModel.DataAnnotations;

namespace FindMyLocation.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
