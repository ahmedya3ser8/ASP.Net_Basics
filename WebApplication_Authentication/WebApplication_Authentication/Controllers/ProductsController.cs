using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication_Authentication.Authorization;
using WebApplication_Authentication.Data;
using WebApplication_Authentication.Entities;

namespace WebApplication_Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("")]
        // [CheckPermission(Permission.ReadProducts)]
        // [Authorize(Roles = "Admin")]
        // [Authorize(Roles = "SuperUser")]
        // [Authorize(Policy = "SuperUsersOnly")]
        [Authorize(Policy = "AgeGreaterThan25")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var records = _dbContext.Set<Product>().ToList();
            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        // [AllowAnonymous]
        [CheckPermission(Permission.ReadProducts)]
        public ActionResult<Product> GetById(int id)
        {
            var userName = User.Identity.Name;
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var record = _dbContext.Set<Product>().Find(id);
            return record == null ? NotFound() : Ok(record);
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
    }
}
