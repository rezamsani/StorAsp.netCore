using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Carts;
using Store.Domain.Entities.Users;

namespace Store.Application.Services.CartServices
{
    public class CartServices : ICartServices
    {
        private readonly IDataBaseContext _context;
        public CartServices(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Add(long CartItemId)
        {
            var cartItem = _context.CartItems.Find(CartItemId);
            cartItem.Count++;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = ""
            };
        }

        public ResultDto AddToCart(long ProductId, Guid BrowserId)
        {
            var cart = _context.Carts.Where(p => p.BrowserId == BrowserId && p.Finished == false)
            .FirstOrDefault();
            if (cart == null)
            {
                Cart newCart = new Cart()
                {
                    Finished = false,
                    BrowserId = BrowserId,
                };
                _context.Carts.Add(newCart);
                _context.SaveChanges();
                cart = newCart;
            }
            var product = _context.Products.Find(ProductId);
            var cartItem = _context.CartItems.Where(p => p.ProductId == ProductId && p.CartId == cart.Id)
                .FirstOrDefault();
            if(cartItem != null)
            {
                cartItem.Count++;
            }
            else
            {
                CartItem newCartItem = new CartItem()
                {
                    Count = 1,
                    Price=product.Price,
                    Cart = cart,
                    Product = product
                };
                _context.CartItems.Add(newCartItem);
            }
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"محصول {product.Name} با موفقیت به سبد خرید اضافه شد."
            };
        }

        public ResultDto<CartDto> GetMyCart(Guid BrowseId,long? UserId)
        {
            var cart = _context.Carts.Where(p => p.BrowserId == BrowseId && p.Finished == false)
                .Include(p=>p.CartItems).ThenInclude(p=> p.Product)
                .ThenInclude(p=> p.ProductImage)
                .OrderByDescending(p => p.Id).FirstOrDefault();
            if(cart == null)
            {
                return new ResultDto<CartDto>()
                {
                    Data = new CartDto
                    {
                        CartItems = new List<CartItemDto>(),
                    }
                };
            }
            if(UserId != null)
            {
                var user = _context.Users.Find(UserId);
                cart.User = user;
                _context.SaveChanges();
            }
            return new ResultDto<CartDto>()
            {
                Data = new CartDto()
                {
                    CartId=cart.Id,
                    ProductCount = cart.CartItems.Count(),
                    SumAmount = cart.CartItems.Sum(p => p.Count * p.Price),
                    CartItems=cart.CartItems.Select(p=> new CartItemDto
                    {
                        Count=p.Count,
                        Price=p.Price,
                        Product=p.Product.Name,
                        Id=p.Id,
                        Images=p.Product?.ProductImage?.FirstOrDefault()?.Src?? ""
                    }).ToList(),
                },
                IsSuccess=true,
                Message=""
            };
        }

        public ResultDto LowOff(long CartItemId)
        {
            var cartItem = _context.CartItems.Find(CartItemId);
            if(cartItem.Count <= 1)
            {
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = ""
                };
            }
            cartItem.Count--;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = ""
            };
        }

        public ResultDto RemoveFromCart(long ProductId, Guid BrowserId)
        {
            var cartItem = _context.CartItems.Where(p => p.Cart.BrowserId == BrowserId).FirstOrDefault();
            if(cartItem != null)
            {
                cartItem.IsRemoved = true;
                cartItem.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "محصول از سبد خرید حذف شد."
                };
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد."
                };
            }
           
        }
    }
}
