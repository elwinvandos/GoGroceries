using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Core.Extensions;
using Elwin.GoGroceries.Domain.Models;
using Elwin.GoGroceries.Infrastructure.Mappers;
using Elwin.GoGroceries.Infrastructure.Repositories;

namespace Elwin.GoGroceries.Core.Managers
{
    public interface IManageCategories
    {
        Task<ICollection<CategoryDto>> GetAllCategoriesAsync();
        Task<ICollection<CategoryDto>> GetEmptyCategoriesAsync();
        Task<CategoryDto> GetCategoryAsync(Guid id);
        Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto);
        Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(Guid categoryId);
    }

    public class ManageCategories : IManageCategories
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGroceryRepository _groceryRepositroy;
        private readonly IProductRepository _productRepository;

        public ManageCategories(ICategoryRepository categoryRepository, IGroceryRepository groceryRepositroy, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _groceryRepositroy = groceryRepositroy;
            _productRepository = productRepository;
        }

        public async Task<ICollection<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAll();
            return categories.Select(category => CategoryMapper.ToDto(category)).ToList();
        }

        public async Task<ICollection<CategoryDto>> GetEmptyCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAll();
            var groceryItems = await _productRepository.GetAllProducts();

            var emptyCategories = new List<CategoryDto>();

            foreach (var category in categories)
            {
                if (!groceryItems.SelectMany(i => i.ListProducts).Select(lp => lp.CategoryId).Contains(category.Id))
                {
                    emptyCategories.Add(CategoryMapper.ToDto(category));
                }
            }

            return emptyCategories;
        }

        public async Task<CategoryDto> GetCategoryAsync(Guid id)
        {
            return CategoryMapper.ToDto(await _categoryRepository.FindAsync(id));
        }

        public async Task<CategoryDto> AddCategoryAsync(CategoryDto dto)
        {
            dto.Name = dto.Name.Capitalize();
            var category = await _categoryRepository.AddAsync(new Category(dto.Name, dto.ColorCode));

            return CategoryMapper.ToDto(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await _categoryRepository.FindAsync(categoryDto.Id);
            category.UpdateName(categoryDto.Name);
            category.UpdateColorCode(categoryDto.ColorCode);
            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            return CategoryMapper.ToDto(updatedCategory);
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _categoryRepository.FindAsync(categoryId);
            await _categoryRepository.DeleteAsync(category);
        }
    }
}
