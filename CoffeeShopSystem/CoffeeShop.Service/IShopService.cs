using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

public interface IShopService
{
    bool IsUniqueName(string input, int shopID);
    bool ValidShopModel(Shop s);
    bool CreateNew(Shop s);

    bool Update(Shop s);

    bool SoftDelete(Shop s);

    bool Recover(Shop s);

    IEnumerable<Shop> GetAll();

    IEnumerable<Shop> GetAllNonDelete();

    Shop GetShopByID(int id);

    IEnumerable<Shop> SearchByNameOrDescription(string Filter, int curPage = 1, int pageSize = 5, string sort = "ID", string type = "asc" , string option = "Managing");


    //sort service or common service
    string ChangeSortType(string type, bool condition);

    IEnumerable<Shop> GetAllNonDeleteWithPaginationAndSort(string sort, string type, int curPage, int pageSize);


    IEnumerable<Shop> GetAllDeleted();
    IEnumerable<Shop> GetAllDeletedWithPaginationAndSort(string sort, string type, int curPage, int pageSize);
    
}