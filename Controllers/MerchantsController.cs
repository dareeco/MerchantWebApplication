using MerchantWebApplication.Models;
using MerchantWebApplication.Models.Response;
using MerchantWebApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MerchantWebApplication.Controllers
{
    [Route("api/merchants")]
    [ApiController]
    public class MerchantsController: ControllerBase
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantsController(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        [HttpGet]
        public ActionResult<MerchantResponse> GetMerchants([FromQuery] int? page, string? merchantCode)
        {
            if(!page.HasValue || page == 0)
            {
                page= 1;
            }

            return _merchantRepository.GetMerchants(page.Value, merchantCode);
        }

        [HttpPost]
        public ActionResult CreateMerchant([FromBody] Merchant merchant)
        {
            _merchantRepository.CreateMerchant(merchant); //go kreira so metodot v repo
            return Ok();
        }
        [HttpPost("{id}/addStore")]
        public ActionResult CreateStoreForMerchant([FromRoute] int id, [FromBody] Store store)
        {
            _merchantRepository.CreateStoreForMerchant(id, store); 
            return Ok();      
        }

        [HttpGet("{id}")]
        public ActionResult GetMerchantById([FromRoute] int id)
        {
            var merchant = _merchantRepository.GetMerchant(id);
            if (merchant == null)
            {
                return NotFound(); //ne postoi merchant so to id
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMerchant([FromRoute] int id, [FromBody] Merchant merchant)
        {
            var result=_merchantRepository.UpdateMerchant(id, merchant);
            
            if(!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMerchant([FromRoute] int id)
        {
            var result=_merchantRepository.DeleteMerchant(id);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
