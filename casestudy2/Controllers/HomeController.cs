using casestudy2.Models.Product;
using casestudy2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace casestudy2.Controllers
{
    // [Route("[controller]")]
    [Authorize]
    public class HomeController : Controller
    {
        IProductRepositories _productRepositories;
        ICartRespositories _cartRespositories;
        IOrderRespositories _orderRespositories;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IProductRepositories productRepositories, UserManager<IdentityUser> userManager, ICartRespositories cartRespositories, IOrderRespositories orderRespositories)
        {
            _productRepositories = productRepositories;
            _userManager = userManager;
            _cartRespositories = cartRespositories;
            _orderRespositories = orderRespositories;
        }



        // [HttpGet("/")]
        [AllowAnonymous]
        public IActionResult HomePage()
        {
            var categories = _productRepositories.GetCategories();
            var subcategories = _productRepositories.GetSubCategories();
            ViewBag.subcategories = subcategories;
            ViewBag.categories = categories;
            return View();
        }
        //[HttpGet("/getAllProduct")]

        [HttpGet("Home/displaypage/{categoryId:int?}/{subcategoryId:int?}")]

        [AllowAnonymous]
        public IActionResult DisplayPage(int? categoryId, int? subcategoryId)
        {

            var categories = _productRepositories.GetCategories();
            var subcategories = _productRepositories.GetSubCategories();
            ViewBag.subcategories = subcategories;
            ViewBag.categories = categories;

            if (categoryId.HasValue && subcategoryId.HasValue)
            {
                ViewBag.allproduct = _productRepositories.ProductFilter(categoryId.Value, subcategoryId.Value);
            }
            else if (categoryId.HasValue)
            {
                ViewBag.allproduct = _productRepositories.ProductFilter(categoryId.Value, -1);
            }
            else if (subcategoryId.HasValue)
            {
                ViewBag.allproduct = _productRepositories.ProductFilter(-1, subcategoryId.Value);
            }
            else
            {
                ViewBag.allproduct = _productRepositories.DisplayAllProduct();
            }

            return View();
        }


        public IActionResult AddProduct()
        {
            return View();
        }

        //[HttpPost("Home/addproduct/{product:int}")]
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productRepositories.AddProduct(product);
            return RedirectToAction("DisplayPage");
        }
        [HttpGet("Home/modifyProduct/{productId:int}")]
        public IActionResult ModifyProduct(int productId)
        {
            Product product = _productRepositories.GetProductbyId(productId);
            if (product == null)
            {
                return Content($"product not found{productId}");
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult ModifyProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                string response = _productRepositories.ModifyProduct(product);
                return RedirectToAction("DisplayPage");
            }

            return RedirectToAction("DisplayPage");
        }
        [HttpGet("Home/deleteproduct/{productId:int}")]
        public IActionResult DeleteProduct(int productId)
        {
            Product product = _productRepositories.GetProductbyId(productId);
            if (product == null)
            {
                return Content("product not found");
            }
            else
            {
                _productRepositories.DeleteProduct(product);
            }
            return RedirectToAction("DisplayPage");
        }
        [HttpGet("/Home/Addtocart/{productId:int}")]
        public IActionResult AddtoCart(int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            var product = _productRepositories.GetProductbyId(productId);
            if (product == null)
            {
                return Content("Product not found");
            }
            var cart = new Cart
            {
                ProductId = productId,
                Quantity = 1,
                UserId = userId
            };
            _cartRespositories.AddtoCart(cart);
            return RedirectToAction("DisplayPage");
        }
        public IActionResult DisplayCart()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            var cartItems = _cartRespositories.GetAllCartitem(userId);
            var grandTotal = cartItems.Sum(e => e.Quantity * e.Product?.ProductPrice);
            ViewBag.grandTotal = grandTotal;
            return View(cartItems);
        }
        [HttpGet("/Home/deletecartitem/{cartItemId:int}")]
        public IActionResult DeleteCartItem(int cartItemId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            _cartRespositories.DeleteCartItem(cartItemId);
            return RedirectToAction("DisplayCart");
        }
        [HttpPost]
        public IActionResult UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            _cartRespositories.UpdateCartItemQuantity(cartItemId, quantity);
            return RedirectToAction("DisplayCart");
        }

        public IActionResult Checkout()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var cartItems = _cartRespositories.GetAllCartitem(userId);
            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("DisplayCart");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderItems = cartItems.Select(e => new OrderItem
                {
                    ProductId = e.ProductId,
                    Quantity = e.Quantity,
                    Price = e.Product.ProductPrice,


                }).ToList(),
                TotalAmount = cartItems.Sum(e => e.Quantity * e.Product.ProductPrice)

            };

            _orderRespositories.CreateOrder(order);

            _cartRespositories.ClearCart();

            return RedirectToAction("OrderConfirmation", new { order.OrderId });
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            var order = _orderRespositories.GetOrderById(orderId);
            return View(order);
        }


    }

}
