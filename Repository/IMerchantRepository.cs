using MerchantWebApplication.Models;
using MerchantWebApplication.Models.Response;

namespace MerchantWebApplication.Repository
{
    public interface IMerchantRepository
    {
        public MerchantResponse GetMerchants(int page, string? merchantCode);
        public Merchant GetMerchant(int id);
        public void CreateMerchant(Merchant merchant);
        public bool UpdateMerchant(int id, Merchant merchant);
        bool DeleteMerchant(int id);
        public bool CreateStoreForMerchant(int id,Store store);
    }
}
