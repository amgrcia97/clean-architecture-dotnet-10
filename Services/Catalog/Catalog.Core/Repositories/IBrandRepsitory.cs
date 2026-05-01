using Catalog.Core.Entities;

namespace Catalog.Core.Repositories
{
    public interface IBrandRepsitory
    {
        Task<IEnumerable<ProductBrand>> GetAllBrands();
        Task<ProductBrand> GetBrandAsync(int id);
    }
}
