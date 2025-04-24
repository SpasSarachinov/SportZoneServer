using SportZoneServer.Common.Requests.Product;
using SportZoneServer.Common.Responses.Product;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Core.Pages;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Data.PaginationAndFiltering;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.Domain.Services;

public class ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository) : IProductService
{
    public async Task<IEnumerable<ProductResponse>?> GetAsync()
    {
        IEnumerable<Product> products = await productRepository.GetAllAsync();

        return products.Select(product => new ProductResponse()
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            RegularPrice = product.RegularPrice,
            Quantity = product.Quantity,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name
        });
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        Product? product = await productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new AppException("Product not found.").SetStatusCode(404);
        }

        return new()
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            RegularPrice = product.RegularPrice,
            Quantity = product.Quantity,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name
        };
    }

    public async Task<ProductResponse?> CreateAsync(CreateProductRequest request)
    {
        Category? category = await categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            throw new AppException("Invalid category.").SetStatusCode(400);
        }

        Product product = new()
        {
            Title = request.Title,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            RegularPrice = request.RegularPrice,
            Quantity = request.Quantity,
            CategoryId = request.CategoryId
        };

        product = await productRepository.AddAsync(product);

        return new()
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            RegularPrice = product.RegularPrice,
            Quantity = product.Quantity,
            CategoryId = product.CategoryId,
            CategoryName = category.Name
        };
    }

    public async Task<ProductResponse?> UpdateAsync(UpdateProductRequest request)
    {
        Product? existingProduct = await productRepository.GetByIdAsync(request.Id);
        if (existingProduct == null)
        {
            throw new AppException("Product not found.").SetStatusCode(404);
        }

        Category? category = await categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            throw new AppException("Invalid category.").SetStatusCode(400);
        }

        existingProduct.Title = request.Title;
        existingProduct.Description = request.Description;
        existingProduct.ImageUrl = request.ImageUrl;
        existingProduct.RegularPrice = request.RegularPrice;
        existingProduct.Quantity = request.Quantity;
        existingProduct.CategoryId = request.CategoryId;

        Product updatedProduct = await productRepository.UpdateAsync(existingProduct);

        return new()
        {
            Id = updatedProduct.Id,
            Title = updatedProduct.Title,
            Description = updatedProduct.Description,
            ImageUrl = updatedProduct.ImageUrl,
            RegularPrice = updatedProduct.RegularPrice,
            Quantity = updatedProduct.Quantity,
            CategoryId = updatedProduct.CategoryId,
            CategoryName = category.Name
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Product? product = await productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new AppException("Product not found.").SetStatusCode(404);
        }

        return await productRepository.DeleteAsync(id);
    }

    public async Task<Paginated<ProductsResponse>> SearchProductsAsync(SearchProductsRequest request)
    {
        if (request == null)
        {
            request = new();
        }
        
        Filter<Product> filter = new()
        {
            Includes = 
            [
                x => x.Category!
            ],
            Predicate = request.GetPredicate(),
            PageNumber = request.PageNumber ?? 1,
            PageSize = request.PageSize ?? 10,
            SortBy = request.SortBy ?? "RegularPrice",
            SortDescending = request.SortDescending ?? false,
        };

        Paginated<Product> result = await productRepository.SearchAsync(filter);

        List<ProductsResponse> responses = new();

        foreach (Product product in result.Items!)
        {
            ProductsResponse response = new()
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                RegularPrice = product.RegularPrice,
                DiscountPercantage = product.DiscountPercantage,
                DiscountedPrice = product.DiscountedPrice,
                Quantity = product.Quantity,
                Rating = product.Rating,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name // Assuming Category has Name
            };

            responses.Add(response);
        }

        Paginated<ProductsResponse> paginated = new()
        {
            Items = responses,
            TotalCount = result.TotalCount
        };

        return paginated;
    }
    
}
