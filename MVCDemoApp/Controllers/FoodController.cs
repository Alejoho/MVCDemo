using DataLibrary.Data;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemoApp.Controllers;

public class FoodController : Controller
{
    private readonly IFoodData _foodData;

    public FoodController(IFoodData foodData)
    {
        _foodData = foodData;
    }

    // GET
    public async Task<IActionResult> List()
    {
        var foodList = await _foodData.GetAllFood();
        return View(foodList);
    }
}