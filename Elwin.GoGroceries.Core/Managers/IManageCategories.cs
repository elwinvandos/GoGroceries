using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Core.Extensions;
using Elwin.GoGroceries.Domain.Models;
using Elwin.GoGroceries.Infrastructure.Repositories;

namespace Elwin.GoGroceries.Core.Managers
{
    public interface IManageCategories
    {
        Task<ICollection<CategoryDto>> GetAllCategoriesAsync();
        Task<ICollection<CategoryDto>> GetEmptyCategoriesAsync();
        Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(Guid categoryId);
    }

    public class ManageCategories : IManageCategories
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGroceryRepository _groceryRepositroy;

        public ManageCategories(ICategoryRepository categoryRepository, IGroceryRepository groceryRepositroy)
        {
            _categoryRepository = categoryRepository;
            _groceryRepositroy = groceryRepositroy;
        }

        public async Task<ICollection<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAll();
            return categories.Select(category => new CategoryDto() { Id = category.Id, Name = category.Name }).ToList();
        }

        public async Task<ICollection<CategoryDto>> GetEmptyCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAll();
            var groceryItems = await _groceryRepositroy.GetAllGroceryItems();

            var emptyCategories = new List<CategoryDto>();

            foreach (var category in categories)
            {
                if (!groceryItems.Select(i => i.CategoryId).Contains(category.Id))
                {
                    emptyCategories.Add(new CategoryDto() { Id = category.Id, Name = category.Name });
                }
            }

            return emptyCategories;
        }
        public async Task<CategoryDto> AddCategoryAsync(CategoryDto dto)
        {
            //todo: proper validation
            if (string.IsNullOrEmpty(dto?.Name))
            {
                throw new ArgumentNullException(nameof(dto.Name));
            }

            dto.Name = dto.Name.Capitalize();
            var res = await _categoryRepository.AddAsync(new Category(dto.Name));

            return new CategoryDto() { Id = res.Id, Name = res.Name };
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _categoryRepository.FindAsync(categoryId);
            if (category is null) throw new ArgumentNullException(nameof(categoryId));

            await _categoryRepository.DeleteAsync(category);
        }
    }
}
