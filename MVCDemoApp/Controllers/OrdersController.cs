using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDemoApp.Models;

namespace MVCDemoApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderData _orderData;
        private readonly IFoodData _foodData;

        public OrdersController(IOrderData orderData, IFoodData foodData)
        {
            _orderData = orderData;
            _foodData = foodData;
        }

        public async Task<ActionResult> Index()
        {
            OrderCreateModel orderCreate = new();
            var foods = await _foodData.GetAllFood();

            foods.ForEach(f =>
            {
                orderCreate.Foods.Add(new SelectListItem()
                {
                    Value = f.Id.ToString(),
                    Text = $"{f.Title} - {f.Price.ToString("C")}"
                });
            });

            return View("Create", orderCreate);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderModel order)
        {
            if (ModelState.IsValid is false)
            {
                return View();
            }

            int id = await _orderData.CreateOrderAsync(order);

            return RedirectToAction("Display", new { id });
        }

        public async Task<IActionResult> Display(int id)
        {
            OrderDisplayModel model = new();

            model.Order = await _orderData.GetOrderByIdAsync(id);
            var foods = await _foodData.GetAllFood();

            model.Food = foods.First(f => f.Id == model.Order.FoodId);
            model.Order.Total = model.Food.Price * model.Order.Quantity;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string newName)
        {
            if (ModelState.IsValid is false)
            {
                OrderDisplayModel model = new();
                model.Order = await _orderData.GetOrderByIdAsync(id);
                var foods = await _foodData.GetAllFood();

                model.Food = foods.First(f => f.Id == model.Order.FoodId);
                model.Order.Total = model.Food.Price * model.Order.Quantity;
                model.NewName = newName;

                return View("Display", model);
            }

            await _orderData.UpdateOrderName(id, newName);

            return RedirectToAction("Display", new { id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderData.GetOrderByIdAsync(id);

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string n)
        {
            await _orderData.DeleteOrder(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> List()
        {
            OrderListModel model = new();
            model.Foods = await _foodData.GetAllFood();
            model.Orders = await _orderData.GetAllOrders();

            model.Orders.ForEach(o =>
            {
                o.Total = o.Quantity * model.Foods.First(f => f.Id == o.FoodId).Price;
            });

            return View("List", model);
        }
    }
}