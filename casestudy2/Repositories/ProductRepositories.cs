using casestudy2.Data;
using casestudy2.Models.Product;
using Microsoft.AspNetCore.Identity;

namespace casestudy2.Repositories
{
    public class ProductRepositories : IProductRepositories
    {

        //dependency injection
        private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ProductRepositories(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public List<Product> DisplayAllProduct()
        {
            List<Product> data = _context.Product.ToList();
            return data;
        }
        public List<Category> GetCategories()
        {
            List<Category> data = _context.Category.ToList();
            return data;
        }
        public List<SubCategory> GetSubCategories()
        {
            List<SubCategory> data = _context.SubCategory.ToList();
            return data;
        }
        public List<Product> ProductFilter(int categoryId, int subcategoryId)
        {

            if (categoryId != -1 && subcategoryId != -1)
            {

                return _context.Product.Where(e => e.SubCategory.CategoryId == categoryId && e.SubCategory.SubCategoryId == subcategoryId).ToList();
            }
            else if (categoryId != -1)
            {
                return _context.Product.Where(e => e.SubCategory.CategoryId == categoryId).ToList();
            }
            else
            {
                return _context.Product.Where(e => e.SubCategory.SubCategoryId == subcategoryId).ToList();

            }

        }
        public void AddProduct(Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
        }
        public string ModifyProduct(Product product)
        {
            _context.Product.Update(product);
            _context.SaveChanges();
            return "Modified success";
        }
        public void DeleteProduct(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }
        public Product GetProductbyId(int productId)
        {
            return _context.Product.Find(productId);
        }

    }

}
