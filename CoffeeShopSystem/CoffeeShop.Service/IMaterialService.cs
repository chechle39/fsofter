using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoffeeShop.Service
{
    public interface IMaterialService : IService<Material>
    {
        Material GetSingleByID(int id);
        IEnumerable<Material> GetSearchStatusPaging(string keyword, int status, int index, int size, out int total);
    }
}