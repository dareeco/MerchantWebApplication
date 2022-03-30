using MerchantWebApplication.Database;
using MerchantWebApplication.Models;
using MerchantWebApplication.Models.Response;

namespace MerchantWebApplication.Repository
{
    public class StoreRepositoryImpl : IStoreRepository
    {
        private readonly StoreDbContext _storeDbContext;

        public StoreRepositoryImpl(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
         
        public void CreateStore(Store store)
        {
            _storeDbContext.Stores.Add(store);
            _storeDbContext.SaveChanges();
        }

        public bool DeleteStore(int id)
        {
            var storeFromDatabase= _storeDbContext.Stores.FirstOrDefault(s => s.Id == id);
            if (storeFromDatabase == null)
            {
                return false;
            }
            _storeDbContext.Stores.Remove(storeFromDatabase);
            _storeDbContext.SaveChanges();
            return true;
        }

        public Store GetStore(int id)
        {
            var store=_storeDbContext.Stores.FirstOrDefault(s => s.Id == id);
            return store;
        }

        public StoreResponse GetStores(int page, string? storeCode)
        {
            var defaultPageSize = 10f;
            var stores = _storeDbContext.Stores.ToList();

            var pageCount = Math.Ceiling(stores.Count / defaultPageSize);

            if (!string.IsNullOrEmpty(storeCode) && stores.Count > 0)
            {
                stores = stores.Where(x => x.storeCode == storeCode).ToList();    //proveri id da ne treba mesto storecode
                pageCount = Math.Ceiling(stores.Count / defaultPageSize);
            }
            var StoresPaged = stores.Skip((page - 1) * (int)defaultPageSize).Take((int)defaultPageSize).ToList();
            StoreResponse storeResponse = new StoreResponse
            {
                Stores = StoresPaged,
                CurrentPage = page,
                Pages = (int)pageCount   
            };
            return storeResponse;
        }

        public bool UpdateStore(int id, Store store)
        {
            var storeFromDatabase = _storeDbContext.Stores.Where(x => x.Id == id).FirstOrDefault();
            if(storeFromDatabase == null)
            {
                return false;
            }
            storeFromDatabase.storeCode = store.storeCode;
            storeFromDatabase.phone = store.phone;
            storeFromDatabase.name = store.name;
            storeFromDatabase.address = store.address;
            storeFromDatabase.description = store.description;
            storeFromDatabase.city = store.city;
            storeFromDatabase.email=store.email;
            return true;
        }
    }
}
