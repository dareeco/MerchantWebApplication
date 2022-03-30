using MerchantWebApplication.Database;
using MerchantWebApplication.Models;
using MerchantWebApplication.Models.Response;

namespace MerchantWebApplication.Repository
{
    public class MerchantRepositoryImpl : IMerchantRepository
    {
        private readonly MerchantDbContext _merchantDbContext; //konvencija da se pishi so _
        private readonly StoreDbContext _storeDbContext;
        

        public MerchantRepositoryImpl(MerchantDbContext merchantDbContext, StoreDbContext storeDbContext)
        {
            _merchantDbContext = merchantDbContext; //da se definira za da mozhi da se raboti so nego
            _storeDbContext = storeDbContext;
        }

        public void CreateMerchant(Merchant merchant)
        {
            _merchantDbContext.Merchants.Add(merchant);
            _merchantDbContext.SaveChanges();   
        }

        public bool DeleteMerchant(int id)
        {
            var merchantFromDatabase= _merchantDbContext.Merchants.FirstOrDefault(x => x.Id == id);
            //Nov ponuden nachin od Visual Studio, treba da e isto. ako pagja meni so _merchantDbContext.Merchants.Where(x => x.Id ==id).FirstOrDefault()

            if (merchantFromDatabase == null)
            {
                return false;
            }
            _merchantDbContext.Merchants.Remove(merchantFromDatabase);
            _merchantDbContext.SaveChanges();
            return true;
        }

        public MerchantResponse GetMerchants(int page, string? merchantCode)
        {
            var defaultPageSize = 10f;
            var merchants = _merchantDbContext.Merchants.ToList();

            var pageCount = Math.Ceiling(merchants.Count / defaultPageSize);
            
            if(!string.IsNullOrEmpty(merchantCode) && merchants.Count > 0)
            {
                merchants=merchants.Where(x => x.merchantCode == merchantCode).ToList();    //dali e okej ovaj del merchantCode == merchantCode
                pageCount= Math.Ceiling(merchants.Count / defaultPageSize);
            }
            var MerchantsPaged = merchants.Skip((page - 1) * (int)defaultPageSize).Take((int)defaultPageSize).ToList();
            MerchantResponse merchantResponse = new MerchantResponse
            {
                Merchants = MerchantsPaged,
                CurrentPage = page,
                Pages = (int)pageCount   //atributi se ovie ne ; na krajo tuku samo zapirki
            };
            return merchantResponse;
    }

        public Merchant GetMerchant(int id)
        {
            var merchant=_merchantDbContext.Merchants.Where(x => x.Id == id).FirstOrDefault();//da go najdi toj so id
            return merchant;
        }

        public bool UpdateMerchant(int id, Merchant merchant)
        {
            var merchantFromDatabase= _merchantDbContext.Merchants.Where(x => x.Id==id).FirstOrDefault();   //go bara so to id

            if (merchantFromDatabase == null)
            {
                return false; //ako ne postoi merchant so to id
            }

            merchantFromDatabase.merchantCode = merchant.merchantCode;
            merchantFromDatabase.merchantName = merchant.merchantName;
            merchantFromDatabase.fullName = merchant.fullName;
            merchantFromDatabase.email = merchant.email;
            merchantFromDatabase.website = merchant.website;
            merchantFromDatabase.telephone = merchant.telephone;
            merchantFromDatabase.address = merchant.address;
            merchantFromDatabase.city = merchant.city;
            merchantFromDatabase.accountNumber = merchant.accountNumber;//gi updejtira atributite so novite atribute
           // merchantFromDatabase.StoresList = merchant.StoresList;  

            _merchantDbContext.SaveChanges();   
            return true;
        }

        public bool  CreateStoreForMerchant(int id, Store store)
        {

            var merchantFromDatabase = _merchantDbContext.Merchants.Where(x => x.Id == id).FirstOrDefault();
            if (merchantFromDatabase == null)
            {
                return false; //ako nema takov trgovec kaj sho sakame da dodajme prodavnica
            }

            //OVDE ISTO PROVER DA LI POSTOJI I TAJ STORE U BAZI(SLICNO KAO STO SI PROVERIO MERCHANT), AKO POSTOJI NECE MOCI DA DODA PONOVO, IMA DA PUKNE
            var storeFromDatabase= _storeDbContext.Stores.Where(x =>x.Id==id).FirstOrDefault();
            if(storeFromDatabase != null)
            {
                return false;
            }

            if(merchantFromDatabase.StoresList == null)
            {
                merchantFromDatabase.StoresList=new List<Store>();   
            }

            merchantFromDatabase.StoresList.Add(store);
            _merchantDbContext.SaveChanges();
            return true;
        }
    }
}
