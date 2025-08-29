using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCDemoApp.Models;

public class OrderCreateModel
{
    public OrderModel Order { get; set; }
    public FoodModel Food { get; set; }
    public List<SelectListItem> Foods { get; set; } = new();
}