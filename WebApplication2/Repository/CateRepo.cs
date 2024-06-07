using WebApplication2.Models;
namespace WebApplication2.Repository
{
    public class CateRepo : ICateRepo
    {
        private readonly OganiContext _oganiContext;
        public CateRepo(OganiContext oganiContext)
        {
            _oganiContext = oganiContext;
        }
        public Category Add(Category category)
        {
            _oganiContext.Categories.Add(category);
            _oganiContext.SaveChanges();
            return category;
        }

        public Category Delete(int CateId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _oganiContext.Categories;
        }

        public Category GetCategory(int CateId)
        {
            return _oganiContext.Categories.Find(CateId);
        }

        public Category Update(Category category)
        {
            _oganiContext.Update(category);
            _oganiContext.SaveChanges();
            return category;    
        }
    }
}
