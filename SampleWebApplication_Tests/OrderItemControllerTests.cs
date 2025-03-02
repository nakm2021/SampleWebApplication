using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebApplication_DataAccess.Data;
using SampleWebApplication_DataAccess.Repository;
using SampleWebApplication_DataAccess.Repository.IRepository;
using SampleWebApplication_Models;
using SampleWebApplication_Web.Controllers;

namespace SampleWebApplication_Tests
{
    /// <summary>
    /// OrderItemControllerのテスト
    /// </summary>
    public class OrderItemControllerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrderItemController _controller;

        public OrderItemControllerTests()
        {
            // テスト用の仮想のデータベースコンテキストを作成
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Nakm_OrderItemControllerTests")
                .Options;
            _context = new ApplicationDbContext(options);
            _unitOfWork = new UnitOfWork(_context);
            _controller = new OrderItemController(_unitOfWork);
            dummyData();
        }

        /// <summary>
        /// ダミーデータを作成
        /// </summary>
        private void dummyData()
        {
            //テストを実行するたびに Ordersを初期化
            //テーブル内の既存のデータをすべて削除してから、新しいダミーデータを挿入する
            _context.OrderItems.RemoveRange(_context.OrderItems);
            //_context.ChangeTracker.Clear();
            for (int i = 1; i <= 1000; i++)
            {
                _context.OrderItems.Add(new OrderItem
                {
                    OrderId = i,
                    ProductId = i,
                    Quantity = 1,
                    Price = i * 777
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
            var order = new OrderItem
            {
                OrderId = 100,
                ProductId = 100,
                Quantity = 1,
                Price = 100 * 777
            };

            // 既存のエンティティがある場合、削除する
            var existingOrder = _context.OrderItems.SingleOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder != null)
            {
                _context.OrderItems.Remove(existingOrder);
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
            var orderItem = new OrderItem
            {
                OrderId = id,
                ProductId = 5,
                Quantity = 1,
                Price = 5 * 777
            };
            var result = _controller.Edit(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrderItem>(viewResult.ViewData.Model);
            Assert.Equal(orderItem, model);
        }
    }
}
