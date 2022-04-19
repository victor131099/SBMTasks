#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SBMPracticeTasks;

namespace SBMPracticeTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInformation>>> GetProduct()
        {
            //var query = from pc in _context.ProductCategory
            //             join psc in _context.ProductSubCategory on pc.ProductCategoryId equals psc.ProductCategoryId into table1
            //             from t1 in table1.DefaultIfEmpty()
            //             join p in _context.Product on t1.ProductSubCategoryId equals p.ProductSubCategoryId into table2
            //             from t2 in table2.DefaultIfEmpty()
            //             select new ProductInformation
            //             {
            //                 ProductId = t2.ProductId,
            //                 Name = t2.Name,
            //                 ProductNumber = t2.ProductNumber,
            //                 FinishedGoodsFlag = t2.FinishedGoodsFlag,
            //                 ReorderPoint = t2.ReorderPoint,
            //                 Color = t2.Color,
            //                 SafetyStockLevel = t2.SafetyStockLevel,
            //                 StandardCost = t2.StandardCost,
            //                 ListPrice = t2.ListPrice,
            //                 DaysToManufacture = t2.DaysToManufacture,
            //                 SellStartDate = t2.SellStartDate,
            //                 ProductCategoryName = pc.Name,
            //                 ProductSubCategoryName = t1.Name


            //             };
            var query = from p in _context.Product
                        join psc in _context.ProductSubCategory on p.ProductSubCategoryId equals psc.ProductSubCategoryId into table1
                        from t1 in table1.DefaultIfEmpty()
                        join pc in _context.ProductCategory on t1.ProductCategoryId equals pc.ProductCategoryId into table2
                        from t2 in table2.DefaultIfEmpty()
                        select new ProductInformation
                        {
                            ProductId = p.ProductId,
                            Name = p.Name,
                            ProductNumber = p.ProductNumber,
                            FinishedGoodsFlag = p.FinishedGoodsFlag,
                            ReorderPoint = p.ReorderPoint,
                            Color = p.Color,
                            SafetyStockLevel = p.SafetyStockLevel,
                            StandardCost = p.StandardCost,
                            ListPrice = p.ListPrice,
                            DaysToManufacture = p.DaysToManufacture,
                            SellStartDate = p.SellStartDate,
                            ProductCategoryName = t2.Name,
                            ProductSubCategoryName = t1.Name


                        };
            return await query.ToListAsync();
        }

        // GET: api/Products/FiniahedProducts
        [HttpGet("FinishedProducts")]
        public async Task<ActionResult<IEnumerable<ProductInformation>>> getfinishedproduct()
        {
            var query = from p in _context.Product
                        join psc in _context.ProductSubCategory on p.ProductSubCategoryId equals psc.ProductSubCategoryId into table1
                        from t1 in table1.DefaultIfEmpty()
                        join pc in _context.ProductCategory on t1.ProductCategoryId equals pc.ProductCategoryId into table2
                        from t2 in table2.DefaultIfEmpty()
                        select new ProductInformation
                        {
                            ProductId = p.ProductId,
                            Name = p.Name,
                            ProductNumber = p.ProductNumber,
                            FinishedGoodsFlag = p.FinishedGoodsFlag,
                            ReorderPoint = p.ReorderPoint,
                            Color = p.Color,
                            SafetyStockLevel = p.SafetyStockLevel,
                            StandardCost = p.StandardCost,
                            ListPrice = p.ListPrice,
                            DaysToManufacture = p.DaysToManufacture,
                            SellStartDate = p.SellStartDate,
                            ProductCategoryName = t2.Name,
                            ProductSubCategoryName = t1.Name,
                            ProductSubCategoryId= p.ProductSubCategoryId,
                            ProductCategoryId = t2.ProductCategoryId

                        };
            return await query.Where(b => b.FinishedGoodsFlag.Equals(true)).ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
