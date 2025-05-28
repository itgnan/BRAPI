using BREmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.DataAccess;
using Product.DataAccess.Models;
using System.Threading.Tasks;

namespace BREmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ProductController : ControllerBase
    {
        private static List<OldProduct> products = new List<OldProduct> { 
          new OldProduct { Id =1, Name="A", Desription ="Good" },
          new OldProduct { Id =2, Name="B", Desription ="Better" }
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ProductDBContext _context;

        public ProductController(ILogger<WeatherForecastController> logger, ProductDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<MyProduct>>> GetAllProducts()
        {
            return await _context.MyProducts.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<MyProduct>> AddProduct([FromBody] MyProduct product)
        {
            _context.MyProducts.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] MyProduct product)
        {
            if (product.Id <= 0)
                return BadRequest();

            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
            
        }

        [HttpPatch]
        public IActionResult UpdateProductDescription([FromBody] OldProduct product)
        {
            OldProduct oldProduct = products.Where(x => x.Id == product.Id).FirstOrDefault();
            products.Remove(oldProduct);
            product.Name = oldProduct.Name;
            products.Add(product);
            return Ok(products.ToList());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var prod = await _context.MyProducts.FindAsync(productId);

            if (prod == null)
                return NoContent();

            _context.MyProducts.Remove(prod);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
