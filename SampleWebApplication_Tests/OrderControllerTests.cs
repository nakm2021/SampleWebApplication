using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SampleWebApplication_DataAccess.Data;
using SampleWebApplication_DataAccess.Repository;
using SampleWebApplication_DataAccess.Repository.IRepository;
using SampleWebApplication_Models;
using SampleWebApplication_Web.Controllers;

namespace SampleWebApplication_Tests
{
    /// <summary>
    /// OrderControllerのテスト
    /// </summary>
    public class OrderControllerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            // テスト用の仮想のデータベースコンテキストを作成
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Nakm")
                .Options;
            _context = new ApplicationDbContext(options);
            _unitOfWork = new UnitOfWork(_context);
            _controller = new OrderController(_unitOfWork);
            dummyData();
        }

        /// <summary>
        /// ダミーデータを作成
        /// </summary>
        private void dummyData()
        {
            //テストを実行するたびに Ordersを初期化
            //テーブル内の既存のデータをすべて削除してから、新しいダミーデータを挿入する
            _context.Orders.RemoveRange(_context.Orders);
            //_context.ChangeTracker.Clear();
            for (int i = 1; i <= 1000; i++)
            {
                _context.Orders.Add(new Order
                {
                    OrderId = i,
                    CustomerId = i,
                    OrderDate = new DateTime(2025, 3, 1)
                });
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Createメソッドのテスト
        /// </summary>
        [Fact]
        public void Create_ReturnsViewResult()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        /// <summary>
        /// Createメソッドのテスト
        /// </summary>
        [Fact]
        public void Create_WithValidOrder_ReturnsRedirectToActionResult()
        {
            var order = new Order
            {
                OrderId = 100,
                CustomerId = 100,
                OrderDate = new DateTime(2025, 3, 1)
            };

            // 既存のエンティティがある場合、削除する
            var existingOrder = _context.Orders.SingleOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder != null)
            {
                _context.Orders.Remove(existingOrder);
                _context.SaveChanges();
            }

            var result = _controller.Create(order);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_controller.Index), redirectToActionResult.ActionName);
        }

        /// <summary>
        /// Editメソッドのテスト
        /// </summary>
        [Fact]
        public void Edit_InvalidId_ReturnsNotFoundResult()
        {
            int? id = null;
            var result = _controller.Edit(id);
            Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Editメソッドのテスト
        /// </summary>
        [Fact]
        public void Edit_ValidId_ReturnsViewResultWithOrder()
        {

            // Arrange
            int id = 5;
            var order = new Order
            {
                OrderId = id,
                CustomerId = 5,
                OrderDate = new DateTime(2025, 3, 1)
            };

            var result = _controller.Edit(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Order>(viewResult.ViewData.Model);
            Assert.Equal(order, model);
        }
    }
}
