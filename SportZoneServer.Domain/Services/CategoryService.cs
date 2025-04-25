using SportZoneServer.Common.Requests.Category;
using SportZoneServer.Common.Responses.Category;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.Domain.Services;

public class CategoryService(ICategoryRepository categoryRepository, IImageRepository imageRepository) : ICategoryService
{
    public async Task<IEnumerable<CategoryResponse>?> GetAsync()
    {
        IEnumerable<Category> categories = await categoryRepository.GetAllAsync();

        CategoryResponse[] response = await Task.WhenAll(categories.Select(async category => new CategoryResponse()
        {
            Id = category.Id,
            Name = category.Name,
            ImageURI = (await imageRepository.GetByIdAsync(category.ImageId)).Uri,
        }));
                
        return response;
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
            ImageURI = (await imageRepository.GetByIdAsync(category.ImageId)).Uri,
        };
    }

    public async Task<CategoryResponse?> CreateAsync(CreateCategoryRequest request)
    {
        Image newImage = new Image()
        {
            Uri = request.ImageURI,
        };
        
        await imageRepository.AddAsync(newImage);
        
        Category category = new()
        {
            Name = request.Name,
            ImageId = newImage.Id,
        };

        category = (await categoryRepository.AddAsync(category))!;

        return new()
        {
            Id = category.Id,
            Name = category.Name,
            ImageURI = newImage.Uri,
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
        
        string imageUri = (await imageRepository.GetByIdAsync(existingCategory.ImageId)).Uri;
        
        if (request.ImageURI != null)
        {
            await imageRepository.DeleteAsync(existingCategory.ImageId);

            Image newImage = new()
            {
                Uri = request.ImageURI,
            };
            
            existingCategory.ImageId = newImage.Id;
            
            imageUri = request.ImageURI;
        }
        Category updatedCategory = (await categoryRepository.UpdateAsync(existingCategory))!;

        return new()
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name,
            ImageURI = imageUri,
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Category? category = await categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new AppException("Category not found.").SetStatusCode(404);
        }

        await imageRepository.DeleteAsync(category.ImageId);
        return await categoryRepository.DeleteAsync(id);
    }
}
