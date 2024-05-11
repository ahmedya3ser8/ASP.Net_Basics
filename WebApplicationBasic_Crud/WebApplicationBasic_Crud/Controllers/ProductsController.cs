using Microsoft.AspNetCore.Mvc;
using WebApplicationBasic_Crud.Data;
using WebApplicationBasic_Crud.Entities;

namespace WebApplicationBasic_Crud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("")]
        public ActionResult<int> CreateProduct(Product product)
        {
            product.Id = 0;
            _dbContext.Set<Product>().Add(product);
            _dbContext.SaveChanges();
            return Ok(product.Id);
        }

        [HttpPut]
        [Route("")]
        public ActionResult UpdateProduct(Product product)
        {
            var existingProduct = _dbContext.Set<Product>().Find(product.Id);
            existingProduct.Name = product.Name;
            existingProduct.Sku = product.Sku;
            _dbContext.Set<Product>().Update(existingProduct);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var existingProduct = _dbContext.Set<Product>().Find(id);
            _dbContext.Set<Product>().Remove(existingProduct);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var records = _dbContext.Set<Product>().ToList();
            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var record = _dbContext.Set<Product>().Find(id);
            return record == null ? NotFound() : Ok(record);
        }
    }
}
