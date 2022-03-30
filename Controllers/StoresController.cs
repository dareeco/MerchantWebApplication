using MerchantWebApplication.Models;
using MerchantWebApplication.Models.Response;
using MerchantWebApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MerchantWebApplication.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoresController:ControllerBase
    {
       private readonly IStoreRepository _storeRepository;
       public StoresController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet]
        public ActionResult<StoreResponse> GetStores([FromQuery] int? page, string? storeCode)
        {
            if (!page.HasValue || page == 0)
            {
                page = 1;
            }
            return _storeRepository.GetStores(page.Value, storeCode);
        }
        
        [HttpPost]
        public ActionResult CreateStore([FromBody] Store store)
        {
            _storeRepository.CreateStore(store);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult GetStoreById([FromRoute] int id)
        {
            var store = _storeRepository.GetStore(id);
            if(store == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStore([FromRoute] int id, [FromBody] Store store)
        {
            var result=_storeRepository.UpdateStore(id, store);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();
        }
        

        [HttpDelete("{id}")]
        public ActionResult DeleteStore([FromRoute] int id)
        {
           var result= _storeRepository.DeleteStore(id);
            if(!result) 
            { 
                return NotFound();
            }
            return Ok();
        }


    }
}
