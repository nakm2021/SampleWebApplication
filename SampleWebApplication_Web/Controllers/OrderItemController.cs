using Microsoft.AspNetCore.Mvc;
using SampleWebApplication_DataAccess.Repository.IRepository;
using SampleWebApplication_Models;

namespace SampleWebApplication_Web.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.OrderItem.Add(orderItem);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            OrderItem? obj = _unitOfWork.OrderItem.Get(x => x.OrderId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.OrderItem.Update(orderItem);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            OrderItem? obj = _unitOfWork.OrderItem.Get(x => x.OrderId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            OrderItem? obj = _unitOfWork.OrderItem.Get(x => x.OrderId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.OrderItem.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            var obj = _unitOfWork.OrderItem.GetAll();
            return View(obj);
        }
    }
}
