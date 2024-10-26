using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AngularApp1.Server.DTO
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Ascii { get; set; } = null!;

        public decimal Lat { get; set; }

        public decimal Lng { get; set; }

        public int Population { get; set; }

        public required string CountryName { get; set; } = null!;

    }

}
