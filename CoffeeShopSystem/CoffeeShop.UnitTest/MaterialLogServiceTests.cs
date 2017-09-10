using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System.Linq;


namespace CoffeeShop.Service.Tests
{
    [TestClass()]
    public class MaterialLogServiceTests
    {
        //private IMATERIALLOGSERVICE SV;
        //private LIST<MATERIALLOG> LIST;
        

        //[TestInitialize]
        //public void Initialize()
        //{
        //    //var fac = new DbFactory();

        //    //sv = new MaterialLogService( new MaterialLogRepository(fac), new UnitOfWork(fac));
        //    _mockRepository = new Mock<IMaterialLogRepository>();
        //    _mockUnitOfWork = new Mock<IUnitOfWork>();
        //    _materialLogservice = new MaterialLogService(_mockRepository.Object, _mockUnitOfWork.Object);
        //   _listMaterialLog = new List<MaterialLog>()
        //    {
        //        new MaterialLog() {ID = 69,Description = "dang cap nhat",Type = true },
        //    };

        //}

        //[TestMethod]
        //public void GetAllTest()
        //{
        //    var result = sv.GetAll();
        //    Assert.AreEqual(3, result.Count());
           
        //}
    }
}