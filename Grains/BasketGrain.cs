using System.Threading.Tasks;
using Orleans;
using OrleansTest.Models;

namespace OrleansTest.Grains
{
    public class BasketGrain : Grain<Basket>, IBasketGrain
    {
        public override Task OnActivateAsync()
        {
            State.CustomerId = this.GetPrimaryKey();
            return base.OnActivateAsync();
        }
        public async Task AddItem(int id)
        {
            State.Items.Add(id);
            await WriteStateAsync();
        }

        public Task<Basket> GetBasket()
        {
            State.Host = System.Net.Dns.GetHostName();
            return Task.FromResult(State);
        }

        public async Task RemoveItem(int id)
        {
            State.Items.Remove(id);
            await WriteStateAsync();
        }
    }
}