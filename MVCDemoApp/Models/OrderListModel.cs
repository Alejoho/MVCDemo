using DataLibrary.Models;

namespace MVCDemoApp.Models;

public class OrderListModel
{
    public List<FoodModel> Foods { get; set; } = new();
    public List<OrderModel> Orders { get; set; } = new();
}