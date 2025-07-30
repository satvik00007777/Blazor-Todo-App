using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoWebApp.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateOnly ReleaseDate { get; set; }

        public string Genre { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
