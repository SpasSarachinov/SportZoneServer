using SportZoneServer.Common.Requests.Product;
using SportZoneServer.Common.Responses.Product;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
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
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
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

        return new ProductResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
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
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Price = request.Price,
            Quantity = request.Quantity,
            CategoryId = request.CategoryId
        };

        product = await productRepository.AddAsync(product);

        return new ProductResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
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

        existingProduct.Name = request.Name;
        existingProduct.Description = request.Description;
        existingProduct.ImageUrl = request.ImageUrl;
        existingProduct.Price = request.Price;
        existingProduct.Quantity = request.Quantity;
        existingProduct.CategoryId = request.CategoryId;

        Product updatedProduct = await productRepository.UpdateAsync(existingProduct);

        return new ProductResponse()
        {
            Id = updatedProduct.Id,
            Name = updatedProduct.Name,
            Description = updatedProduct.Description,
            ImageUrl = updatedProduct.ImageUrl,
            Price = updatedProduct.Price,
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
}
