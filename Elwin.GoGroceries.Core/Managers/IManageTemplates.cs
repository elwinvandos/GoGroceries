using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Contracts.Post;
using Elwin.GoGroceries.Contracts.Product;
using Elwin.GoGroceries.Core.Extensions;
using Elwin.GoGroceries.Domain.Models;
using Elwin.GoGroceries.Domain.Models.GroceryLists.Templates;
using Elwin.GoGroceries.Infrastructure.Mappers;
using Elwin.GoGroceries.Infrastructure.Repositories;

namespace Elwin.GoGroceries.Core.Managers
{
    public interface IManageTemplates
    {
        Task<TemplateDto> GetTemplateAsync(Guid id);
        Task<ICollection<TemplateDto>> GetAllTemplatesAsync();
        Task<bool> DoesTemplateProductExistAsync(Guid templateId, string name, Guid categoryId);
        Task<TemplateDto> AddTemplateAsync(TemplateDto templateDto);
        Task<TemplateDto> AddProductToTemplateAsync(Guid templateId, PostProductDto productDto);
        Task<TemplateDto> PutTemplateUpdateAsync(TemplateDto templateDto);
        Task<TemplateProductDto> PutProductUpdate(Guid templateId, PostProductDto productDto);
        Task ToGroceryListAsync(Guid id);
        Task DeleteTemplateAsync(Guid templateId);
        Task<TemplateDto> RemoveProductFromTemplateAsync(Guid templateId, Guid productId);
    }

    public class ManageTemplates : IManageTemplates
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryRepository _groceryRepository;

        public ManageTemplates(ITemplateRepository templateRepository, ICategoryRepository categoryRepostiroy, IProductRepository productRepository, IGroceryRepository groceryRepository)
        {
            _templateRepository = templateRepository;
            _categoryRepository = categoryRepostiroy;
            _productRepository = productRepository;
            _groceryRepository = groceryRepository;
        }

        public async Task<TemplateDto> GetTemplateAsync(Guid id)
        {
            var template = await _templateRepository.FindAsync(id);
            return TemplateMapper.ToDto(template);
        }

        public async Task<ICollection<TemplateDto>> GetAllTemplatesAsync()
        {
            var templates = await _templateRepository.GetAll();
            return templates.Select(TemplateMapper.ToDto).ToList();
        }

        public async Task<bool> DoesTemplateProductExistAsync(Guid templateId, string name, Guid categoryId)
        {
            var template = await _templateRepository.FindAsync(templateId);
            return template.ValidateProductNotDuplicate(name, categoryId);
        }

        public async Task<TemplateDto> AddTemplateAsync(TemplateDto templateDto)
        {
            templateDto.Name = templateDto.Name.Capitalize();
            var res = await _templateRepository.AddAsync(new GroceryListTemplate(templateDto.Name));

            return TemplateMapper.ToDto(res);
        }

        public async Task<TemplateDto> AddProductToTemplateAsync(Guid templateId, PostProductDto dto)
        {
            var template = await _templateRepository.FindAsync(templateId);

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException(nameof(dto.Name));

            // Consider refactoring below to Chain of Responsibilities design pattern in the future
            Guid categoryId;

            if (dto.Category is null || dto.Category.Id == Guid.Empty)
            {
                dto.Category.Name = dto.Category.Name.Capitalize();
                var category = await _categoryRepository.AddAsync(new Category(dto.Category.Name, dto.Category.ColorCode));
                categoryId = category.Id;
            }
            else
            {
                var category = await _categoryRepository.FindAsync(dto.Category.Id);
                if (category == null) throw new ArgumentNullException(nameof(dto.Category));
                categoryId = dto.Category.Id;
                await _categoryRepository.IncreaseUserWeight(category);
            }

            var product = await _productRepository.FindProductByNameAsync(dto.Name);

            if (product is null)
            {
                dto.Name = dto.Name.Capitalize();

                await _templateRepository.AddProductAsync(template, new Product(dto.Name), dto.Category.Id, dto.Quantity, dto.Measurement, dto.MeasurementQuantity);
            }
            else
            {
                await _templateRepository.AddProductAsync(template, product, dto.Category.Id, dto.Quantity, dto.Measurement, dto.MeasurementQuantity);
                await _productRepository.IncreaseUserWeight(product);
            }

            return TemplateMapper.ToDto(template);
        }

        public async Task<TemplateDto> PutTemplateUpdateAsync(TemplateDto templateDto)
        {
            if (string.IsNullOrEmpty(templateDto.Name)) throw new ArgumentNullException(nameof(templateDto));

            var template = await _templateRepository.FindAsync(templateDto.Id);

            if (template is null) throw new ArgumentNullException(nameof(templateDto));

            var res = await _templateRepository.UpdateAsync(template, templateDto.Name);

            return TemplateMapper.ToDto(res);
        }

        public async Task<TemplateProductDto> PutProductUpdate(Guid templateId, PostProductDto productDto)
        {
            var template = await _templateRepository.FindAsync(templateId);
            var templateProduct = template.TemplateProducts.Single(lp => lp.Product.Id == productDto.Id);

            if (templateProduct.Product.Name != productDto.Name)
            {
                var product = await _productRepository.FindProductByNameAsync(productDto.Name);

                if (product is null)
                {
                    productDto.Name = productDto.Name.Capitalize();
                    var res = await _templateRepository.AddProductAsync(template, new Product(productDto.Name), productDto.Category.Id, productDto.Quantity, productDto.Measurement, productDto.MeasurementQuantity);
                    templateProduct = res.TemplateProducts.Single(lp => lp.Id == templateProduct.Id);
                }
                else
                {
                    var res = await _templateRepository.AddProductAsync(template, product, productDto.Category.Id, productDto.Quantity, productDto.Measurement, productDto.MeasurementQuantity);
                    templateProduct = res.TemplateProducts.Single(lp => lp.Id == templateProduct.Id);
                }

                await _templateRepository.RemoveProductAsync(template, templateProduct);

            }
            else
            {
                await _templateRepository.UpdateProductAsync(template, templateProduct.Id, productDto.Category.Id, productDto.Quantity, productDto.Measurement, productDto.MeasurementQuantity);
            }

            return ProductMapper.ToDto(templateProduct);
        }

        public async Task ToGroceryListAsync(Guid id)
        {
            var template = await _templateRepository.FindAsync(id);
            var groceryList = template.ToGroceryList();
            await _groceryRepository.AddAsync(groceryList);
        }

        public async Task DeleteTemplateAsync(Guid templateId)
        {
            var template = await _templateRepository.FindAsync(templateId);
            await _templateRepository.DeleteAsync(template);
        }

        public async Task<TemplateDto> RemoveProductFromTemplateAsync(Guid templateId, Guid productId)
        {
            var template = await _templateRepository.FindAsync(templateId);

            var listProduct = template.TemplateProducts.Single(i => i.ProductId == productId);
            var res = await _templateRepository.RemoveProductAsync(template, listProduct);
            return TemplateMapper.ToDto(res);
        }
    }
}
