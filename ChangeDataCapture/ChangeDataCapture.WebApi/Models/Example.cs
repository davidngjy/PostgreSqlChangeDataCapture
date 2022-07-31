using System.ComponentModel.DataAnnotations.Schema;

namespace ChangeDataCapture.WebApi.Models;

[Table("example")]
public class Example
{
    [Column("example_id")]
    public int Id { get; init; }

    [Column("name")]
    public string Name { get; init; }

    [Column("description")]
    public string Description { get; init; }
}
