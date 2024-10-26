using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataModel;

[Table("city")]
public partial class City
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("ascii")]
    [StringLength(50)]
    public string Ascii { get; set; } = null!;

    [Column("lat", TypeName = "decimal(18, 4)")]
    public decimal Lat { get; set; }

    [Column("lng", TypeName = "decimal(18, 4)")]
    public decimal Lng { get; set; }

    [Column("population")]
    public int Population { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }  // Foreign Key

    public virtual Country Country { get; set; } = null!;
}
