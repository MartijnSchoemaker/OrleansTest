using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans;
using OrleansTest.Grains;
using OrleansTest.Models;

namespace OrleansTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> logger;
        private readonly IGrainFactory grainFactory;

        public BasketController(ILogger<BasketController> logger, IGrainFactory grainFactory)
        {
            this.logger = logger;
            this.grainFactory = grainFactory;
        }

        [HttpGet("{customerId}")]
        public async Task<Basket> Get(Guid customerId)
        {
            var basketGrain = grainFactory.GetGrain<IBasketGrain>(customerId);
            var basket = await basketGrain.GetBasket();
           
            return basket;
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> AddItem(Guid customerId, [FromBody]int itemId)
        {
            var basketGrain = grainFactory.GetGrain<IBasketGrain>(customerId);
            await basketGrain.AddItem(itemId);

            return new OkResult();
        }

        [HttpDelete("{customerId}/{itemId}")]
        public async Task<IActionResult> RemoveItem(Guid customerId, int itemId)
        {
            var basketGrain = grainFactory.GetGrain<IBasketGrain>(customerId);
            await basketGrain.RemoveItem(itemId);

            return new OkResult();
        }
    }
}
