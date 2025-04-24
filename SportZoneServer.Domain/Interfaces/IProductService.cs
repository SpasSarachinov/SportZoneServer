using SportZoneServer.Common.Requests.Product;
using SportZoneServer.Common.Responses.Product;
using SportZoneServer.Core.Pages;

namespace SportZoneServer.Domain.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>?> GetAsync();
    Task<ProductResponse?> GetByIdAsync(Guid id);
    Task<ProductResponse?> UpdateAsync(UpdateProductRequest request);
    Task<ProductResponse?> CreateAsync(CreateProductRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<Paginated<ProductsResponse>> SearchProductsAsync(SearchProductsRequest request);

}
