using MerchantWebApplication.Models;
using MerchantWebApplication.Models.Response;

namespace MerchantWebApplication.Repository
{
    public interface IStoreRepository
    {
        public StoreResponse GetStores(int page, string? storeCode);
        public Store GetStore(int id);
        public void CreateStore(Store store);
        public bool UpdateStore(int id, Store store);
        bool DeleteStore(int id);


    }
}
