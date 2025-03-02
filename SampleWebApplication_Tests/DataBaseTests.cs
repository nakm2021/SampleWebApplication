using Microsoft.EntityFrameworkCore;
using SampleWebApplication_DataAccess.Data;
using SampleWebApplication_DataAccess.Repository;
using SampleWebApplication_DataAccess.Repository.IRepository;
using SampleWebApplication_Models;
using static NuGet.Packaging.PackagingConstants;

namespace SampleWebApplication_Tests
{
    public class DataBaseTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public DataBaseTests()
        {
            // テスト用の仮想のデータベースコンテキストを作成
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Nakm_DataBaseTests")
                .Options;
            _context = new ApplicationDbContext(options);
            _unitOfWork = new UnitOfWork(_context);
        }

        private void Init()
        {
            if (_unitOfWork.Order.GetAll().Count() >= 1)
            {
                foreach (var order in _unitOfWork.Order.GetAll())
                {
                    _unitOfWork.Order.Remove(order);
                }
                _unitOfWork.Save();
            }
        }

        [Fact]
        public void AddOrderTest1()
        {
            Init();
            var expected = new Order
            {
                OrderId = 999,
                CustomerId = 999,
                OrderDate = new DateTime(2000, 1, 1, 1, 1, 1)
            };
            _unitOfWork.Order.Add(expected);
            _unitOfWork.Save();

            var actual = _unitOfWork.Order.Get(o => o.OrderId == 999);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddOrderTest2()
        {
            Init();
            var expected = new Order
            {
                OrderId = -1,
                CustomerId = -1,
                OrderDate = new DateTime(2000, 1, 1, 1, 1, 1)
            };
            _unitOfWork.Order.Add(expected);
            _unitOfWork.Save();

            var actual = _unitOfWork.Order.Get(o => o.OrderId == -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetOrderTest()
        {
            Init();
            // データを追加
            for (int i = 1; i <= 1000; i++)
            {
                var order = new Order
                {
                    OrderId = i,
                    CustomerId = i,
                    OrderDate = new DateTime(2025, 3, 1)
                };
                _unitOfWork.Order.Add(order);
            }
            _unitOfWork.Save();

            var actual = _unitOfWork.Order.Get(o => o.OrderId == 120);

            Assert.Equal(120, actual?.OrderId);
            Assert.Equal(120, actual?.CustomerId);
            Assert.Equal(new DateTime(2025, 3, 1), actual?.OrderDate);
        }

        [Fact]
        public void GetAllOrdersCountTest()
        {
            Init();
            // データを追加
            for (int i = 1; i <= 1000; i++)
            {
                var order = new Order
                {
                    OrderId = i,
                    CustomerId = i,
                    OrderDate = new DateTime(2025, 3, 1)
                };
                _unitOfWork.Order.Add(order);
            }
            _unitOfWork.Save();
            var actual = _unitOfWork.Order.GetAll();
            Assert.Equal(1000, actual.Count());
        }

        [Fact]
        public void GetAllOrdersCollectionTest()
        {
            Init();
            // データを追加
            List<Order> orders = new List<Order>();
            for (int i = 1; i <= 1000; i++)
            {
                var order = new Order
                {
                    OrderId = i,
                    CustomerId = i,
                    OrderDate = new DateTime(2025, 3, 1)
                };
                orders.Add(order);
                _unitOfWork.Order.Add(order);
            }
            _unitOfWork.Save();
            var actual = _unitOfWork.Order.GetAll();
            Assert.Equal(orders.OrderBy(x => x.OrderId), actual.OrderBy(x => x.OrderId));
        }

        [Fact]
        public void RemoveOrderTest()
        {
            Init();
            for (int i = 1; i <= 1000; i++)
            {
                var order = new Order
                {
                    OrderId = i,
                    CustomerId = i,
                    OrderDate = new DateTime(2025, 3, 1)
                };
                _unitOfWork.Order.Add(order);
            }
            _unitOfWork.Save();

            var deleteOrder = _unitOfWork.Order.Get(o => o.OrderId == 33);

            if (deleteOrder != null)
            {
                _unitOfWork.Order.Remove(deleteOrder);
                _unitOfWork.Save();
            }
            Assert.Null(_unitOfWork.Order.Get(o => o.OrderId == 33));
        }

        [Fact]
        public void UpdateOrderTest()
        {
            Init();
            for (int i = 1; i <= 1000; i++)
            {
                var order = new Order
                {
                    OrderId = i,
                    CustomerId = i,
                    OrderDate = new DateTime(2025, 3, 1)
                };
                _unitOfWork.Order.Add(order);
            }
            _unitOfWork.Save();

            var updateOrder = _unitOfWork.Order.Get(o => o.OrderId == 55);
            if (updateOrder != null)
            {
                updateOrder.CustomerId = 1313;
                updateOrder.OrderDate = new DateTime(2025, 3, 2);
                _unitOfWork.Order.Update(updateOrder);
                _unitOfWork.Save();
            }
            var expected = new Order
            {
                OrderId = 55,
                CustomerId = 1313,
                OrderDate = new DateTime(2025, 3, 2)
            };
            var actual = _unitOfWork.Order.Get(o => o.OrderId == 55);
            Assert.Equal(expected, actual);

        }
    }
}
