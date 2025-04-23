using SportZoneServer.Common.Requests.Category;
using SportZoneServer.Common.Responses.Category;

namespace SportZoneServer.Domain.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>?> GetAsync();
    Task<CategoryResponse?> GetByIdAsync(Guid id);
    Task<CategoryResponse?> UpdateAsync(UpdateCategoryRequest request);
    Task<CategoryResponse?> CreateAsync(CreateCategoryRequest request);
    Task<bool> DeleteAsync(Guid id);
}
