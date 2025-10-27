using AutoMapper;
using EcommerceApp.Application.Services.Interfaces;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Core.Entities;
using EcommerceApp.Core.Repositories;

namespace EcommerceApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _uof.ProductRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            var product = await _uof.ProductRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _uof.ProductRepository.GetProductsByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task CreateAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<ProductModel>(productDto);
            await _uof.ProductRepository.CreateAsync(product);
            await _uof.CommitAsync();
        }

        public async Task UpdateAsync(int id, ProductDTO productDto)
        {
            var productDb = await _uof.ProductRepository.GetByIdAsync(id);
            if (productDb != null)
            {
                _mapper.Map(productDto, productDb);
                await _uof.CommitAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var productDb = await _uof.ProductRepository.GetByIdAsync(id);
            if (productDb != null)
            {
                _uof.ProductRepository.Delete(productDb);
                await _uof.CommitAsync();
            }
        }
    }
}
