using System.ComponentModel.DataAnnotations;
using DataLibrary.Models;

namespace MVCDemoApp.Models;

public class OrderDisplayModel
{
    public OrderModel Order { get; set; }
    public FoodModel Food { get; set; }
    public int Id { get; set; }

    [Required]
    [MaxLength(20, ErrorMessage = "The order by should be less than 20 characters")]
    [MinLength(3, ErrorMessage = "The order by should have at least 3 characters")]
    public string NewName { get; set; }
}