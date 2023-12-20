using AutoMapper;
using Ecommerce.Dto;
using Ecommerce.Models;
using Ecommerce.Services;
using Ecommerce.Services.Iservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IProduct _productservice;

        public ProductController(IMapper mapper , IProduct productservice)
        {
            _mapper = mapper;
            _productservice = productservice;
            
        }


        [HttpGet]
        public async Task<ActionResult<PaginationMetadataDto>> GetProducts(int size , int page)
        {
            var paginationmetadata = await _productservice.GetProductsAsync(size,page);
            if (paginationmetadata.products.Count == 0) return NotFound("No products found");
            return Ok(paginationmetadata);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<Product>>> GetFilteredProducts(string productname , int price , int size, int page)
        {
            var response = await _productservice.GetProductsAsync(page , size);

            var products = response.products;


            if (!string.IsNullOrEmpty(productname))
            {
                products = products.Where(x => x.Name == productname).ToList();
                return Ok(products);
            }

            if (!string.IsNullOrEmpty(productname) && price != null)
            {

                products = products.Where(x => x.Name == productname).ToList();
                products = products.Where(x => x.Price == price).ToList();

                return Ok(products);
            }

            if (products == null) return NotFound("No products found");
            return Ok(products);
        }

        [HttpPost]
        [Authorize(policy: "AdminPolicy")]


        public async Task<ActionResult<string>> AddProduct(AddProductDto newproduct ) {

            var mappedproduct = _mapper.Map<Product>(newproduct);

            var response = await _productservice.AddProductAsync(mappedproduct);

            return Created($"Product/{mappedproduct.Id}", response);
        
        }

        [HttpGet("{Id}")]

        public async Task<ActionResult<Product>> GetProduct(Guid Id) {

            var product = await _productservice.GetProductAsync(Id);

            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpDelete("{Id}")]
        [Authorize]

        public async Task<ActionResult> DeleteProduct(Guid Id) {

            var product = await _productservice.GetProductAsync(Id);

            if(product == null) return NotFound();
            var response = await _productservice.DeleteProductsAsync(product);

            return NoContent();
        
        }

        [HttpPut("{Id}")]
        [Authorize(policy: "AdminPolicy")]


        public async Task<ActionResult<string>> UpdateProduct(Guid Id , AddProductDto updatedproduct)
        {
            var existingproduct = await _productservice.GetProductAsync(Id);
            var mappedproduct = _mapper.Map(updatedproduct,existingproduct);

            var response = await _productservice.UpdateProductAsync();

            return Ok(response);


        }
    }
}
