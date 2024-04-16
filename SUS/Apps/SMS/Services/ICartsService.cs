
using SMS.Data;
using SMS.ViewModels.Carts;

namespace SMS.Services
{
    public interface ICartsService
    {
        void AddProductToCart(string cartId, string productId);

        bool DoesProductExist(string productId);

        string GetUserCartId(string userId);

        ICollection<CartProductViewModel> GetUserCart(string userId);

        void BuyProductsInCart(string userId);

        void RemoveProductFromCart(string userId, string productId);
    }
}
