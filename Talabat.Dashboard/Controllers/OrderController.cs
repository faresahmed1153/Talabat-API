using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Core.Specifications.OrderSpecifications;

namespace Talabat.Dashboard.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var spec = new OrderSpecifications();
            var orders = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return View(orders);
        }
        public async Task<IActionResult> GetUserOrders(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return View("Index", orders);
        }
    }
}
