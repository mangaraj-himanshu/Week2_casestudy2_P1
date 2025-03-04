using casestudy2.Models.Product;

namespace casestudy2.Repositories
{
    public interface IProductRepositories
    {
        public List<Product> DisplayAllProduct();
        public List<Category> GetCategories();
        public List<SubCategory> GetSubCategories();
        public List<Product> ProductFilter(int categoryId, int subcategoryID);
        public void AddProduct(Product product);
        public string ModifyProduct(Product product);
        public void DeleteProduct(Product product);
        public Product GetProductbyId(int productId);


    }
}
