using SportZoneServer.Common.Requests.Category;
using SportZoneServer.Common.Responses.Category;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.Domain.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<CategoryResponse>?> GetAsync()
    {
        IEnumerable<Category> categories = await categoryRepository.GetAllAsync();

        return categories.Select(category => new CategoryResponse()
        {
            Id = category.Id,
            Name = category.Name,
            ImageURI = category.ImageURI,
        });
    }

    public async Task<CategoryResponse?> GetByIdAsync(Guid id)
    {
        Category? category = await categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new AppException("Category not found.").SetStatusCode(404);
        }

        return new()
        {
            Id = category.Id,
            Name = category.Name,
            ImageURI = category.ImageURI,
        };
    }

    public async Task<CategoryResponse?> CreateAsync(CreateCategoryRequest request)
    {
        Category category = new()
        {
            Name = request.Name,
            ImageURI = request.ImageURI,
        };

        category = (await categoryRepository.AddAsync(category))!;

        return new()
        {
            Id = category.Id,
            Name = category.Name,
            ImageURI = category.ImageURI,
        };
    }

    public async Task<CategoryResponse?> UpdateAsync(UpdateCategoryRequest request)
    {
        Category? existingCategory = await categoryRepository.GetByIdAsync(request.Id);
        if (existingCategory == null)
        {
            throw new AppException("Category not found.").SetStatusCode(404);
        }

        existingCategory.Name = request.Name;
        existingCategory.ImageURI = request.ImageURI;

        Category updatedCategory = (await categoryRepository.UpdateAsync(existingCategory))!;

        return new()
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name,
            ImageURI = updatedCategory.ImageURI,
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Category? category = await categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new AppException("Category not found.").SetStatusCode(404);
        }

        return await categoryRepository.DeleteAsync(id);
    }
}
