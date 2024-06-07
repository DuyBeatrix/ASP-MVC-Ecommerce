using WebApplication2.Models;
namespace WebApplication2.Repository
{
    public interface ICateRepo
    {
        Category Add(Category category);
        Category Update(Category category);
        Category Delete(int CateId);
        Category GetCategory(int CateId);
        IEnumerable<Category> GetAllCategories();
    }
}
