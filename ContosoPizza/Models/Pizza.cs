using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models;

public class Pizza
{
    public int Id { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Name is too long.")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid characters in the name.")]
    public string? Name { get; set; }
    [Required]
    [EnumDataType(typeof(PizzaSize), ErrorMessage = "Invalid pizza size.")]
    public PizzaSize Size { get; set; }
    [Required]
    public bool IsGlutenFree { get; set; }

    [Range(0.01, 9999.99)]
    public decimal Price { get; set; }
}

public enum PizzaSize { Small, Medium, Large }