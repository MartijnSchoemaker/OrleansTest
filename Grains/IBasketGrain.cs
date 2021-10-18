using System.Threading.Tasks;
using Orleans;
using OrleansTest.Models;

namespace OrleansTest.Grains
{
    public interface IBasketGrain: IGrainWithGuidKey
    {
        Task<Basket> GetBasket();
        Task AddItem(int id);
        Task RemoveItem(int id);
    }
}