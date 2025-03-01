using Microsoft.AspNetCore.Mvc;
using SampleWebApplication_DataAccess.Repository.IRepository;
using SampleWebApplication_Models;

namespace SampleWebApplication_Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Order.Add(order);
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
            Order? obj = _unitOfWork.Order.Get(x => x.OrderId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Order.Update(order);
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
            Order? obj = _unitOfWork.Order.Get(x => x.OrderId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Order? obj = _unitOfWork.Order.Get(x => x.OrderId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Order.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            var obj = _unitOfWork.Order.GetAll();
            return View(obj);
        }
    }
}
