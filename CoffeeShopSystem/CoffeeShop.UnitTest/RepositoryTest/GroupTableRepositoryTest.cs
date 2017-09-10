using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CoffeeShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class GroupTableRepositoryTest
    {
        IDbFactory dbFactory;
        IGroupTableRepository objRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize() //test phuong thuc khoi tao. de khoi tao cac khoi tuong test
        {
            dbFactory = new DbFactory();
            objRepository = new GroupTableRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void GroupTable_Repository_Create()
        {
            GroupTable groupTable = new GroupTable();
            groupTable.Name = "test-name";
            groupTable.Description = "test-description";
            groupTable.IsDelete = false;
            var rs = objRepository.Add(groupTable);
            unitOfWork.Commit();
            Assert.IsNotNull(rs);
            Assert.Equals(1, rs.ID);
        }

        [TestMethod]
        public void GroupTable_Repository_GetAll()
        {
            var list = objRepository.GetAll().ToList();
            Assert.AreEqual(7, list.Count);
        }
    }
}
