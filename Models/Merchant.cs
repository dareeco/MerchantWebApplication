namespace MerchantWebApplication.Models
{
    public class Merchant
    {
        public int Id { get; set; }
        public string merchantCode { get; set; }
        public string merchantName { get; set; }    
        public string fullName { get; set; }  
        //Nagore za prikazhvanje vo tabela, nadolu dodatni podatoci
        public string telephone { get; set; } //string oti ne e broj sho se koristi vo presmetki nema brojna vrednost
        public string address { get; set; }
        public string city { get; set; }  
        public string email { get; set; }
        public string website { get; set; }
        public string accountNumber { get; set; } //od ista prichina ko telefonot string

        //tuka kje treba lista od Prodavnici i da gi vrzish one to many
        //eden merchant pokje prodavnici, ko wp so shopping carts
        //1-N, 1 Merchant, N Stores
       public List<Store> StoresList{ get; set; }
      




    }
}
