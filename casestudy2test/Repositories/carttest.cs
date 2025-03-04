using casestudy2.Data;
using casestudy2.Models.Product;
using casestudy2.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
namespace casestudy2test.Repositories
{
    public class CartTest
    {
        CartRespositories cartRespositories;
        Mock<ApplicationDbContext> context;
        UserManager<IdentityUser> _userManager;
        public CartTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MainDatabase").Options;
            context = new Mock<ApplicationDbContext>(options);
            cartRespositories = new CartRespositories(context, _userManager);
        }
        [Fact]
        public void addcart()
        {
            var caritem = new Cart { UserId = "1", ProductId = 1, Quantity = 1 };
            cartRespositories.AddtoCart(caritem);
            context.Verify(m => m.Cart.Add(It.IsAny<Cart>()), Times.Once());
            context.Verify(m => m.SaveChanges(), Times.Once());

        }
    }

}
