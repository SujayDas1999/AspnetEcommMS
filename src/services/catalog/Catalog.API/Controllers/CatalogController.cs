using Catalog.API.Entities;
using Catalog.API.Entities.Dtos;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;    

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {

            return Ok(await _productRepository.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpGet("{category}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string category)
        {
            var products = await _productRepository.GetByCategory(category);
            if(products == null)
            {
                _logger.LogError("not found");
                return NotFound();
            }

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Create([FromBody] ProductDto productdto)
        {
            var product = new Product
            {
                Name = productdto.Name,
                Category = productdto.Category,
                Description = productdto.Description,
                ImageFile = productdto.ImageFile,
                Price = productdto.Price,
                Summary = productdto.Summary
            };

            await _productRepository.Save(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateById(string id, [FromBody] ProductDto productdto)
        {

            var product = new Product
            {
                Name = productdto.Name,
                Category = productdto.Category,
                Description = productdto.Description,
                ImageFile = productdto.ImageFile,
                Price = productdto.Price,
                Summary = productdto.Summary
            };

            var success = await _productRepository.Update(id, product);

            if(success)
            {
                return Ok();
            } 
            return BadRequest("unable to update");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> DeleteById(string id)
        {
            var success = await _productRepository.DeleteById(id);
            if (success) return Ok("deleted");
            return BadRequest("not deleted");
        }

    }
}
