using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CoffeeShop.UnitTest.ServiceTest
{
    [TestClass]
    public class GroupTableServiceTest
    {
        private Mock<IGroupTableRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IGroupTableService _groupService;
        private List<GroupTable> listGroupTable;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IGroupTableRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _groupService = new GroupTableService(_mockRepository.Object, _mockUnitOfWork.Object);
            listGroupTable = new List<GroupTable>()
            {
                new GroupTable() {ID = 1, Name= "Test 1", IsDelete = false },
                new GroupTable() {ID = 2, Name= "Test 2", IsDelete = false },
                new GroupTable() {ID = 3, Name= "Test 3", IsDelete = true },
                new GroupTable() {ID = 4, Name= "Test 4", IsDelete = false }
            };
        }

        [TestMethod]
        public void GroupTable_Service_GetAll()
        {
            //setup method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(listGroupTable);

            //call method
            var rs = _groupService.GetAll() as List<GroupTable>;

            //compare
            Assert.IsNotNull(rs);
            Assert.AreEqual(4, rs.Count);
        }

        [TestMethod]
        public void GroupTable_Service_Create()
        {
            GroupTable groupTable = new GroupTable();
            groupTable.Name = "test-name";
            groupTable.IsDelete = true;
            _mockRepository.Setup(m => m.Add(groupTable)).Returns((GroupTable g) =>
            {
                g.ID = 1;
                return g;
            });
            var rs = _groupService.Add(groupTable);
            Assert.IsNotNull(rs);
            Assert.AreEqual(1, rs.ID);
        }
    }
}