using System.ComponentModel.DataAnnotations.Schema;

namespace MerchantWebApplication.Models
{
    public class Store
    {
        public int Id { get; set; } 
        public string storeCode { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string email { get; set; }

        //povrzi so merchant
        //By convention, cascade delete will be set to Cascade
        //for required relationships and ClientSetNull for optional relationships.
        [ForeignKey("merchantId")]  //definicija
        public int merchantId { get; set; }   //foreign key
        //public Merchant Merchant { get; set; } 
    }
}
