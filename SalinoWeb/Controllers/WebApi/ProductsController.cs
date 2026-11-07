using Salino.DataLayer;
using Salino.Models;

namespace SalinoMvc5.Controllers.WebApi
{
    /// <summary>
    /// Port of the Web API 2 ProductsController. Only the GET endpoints used by the front end
    /// (Majorshopping, PagedIndex, Search) are kept; the scaffolded PUT/POST/DELETE endpoints
    /// were unauthenticated and unused.
    /// </summary>
    [Route("api/Products")]
    public class ProductsController : ControllerBase
    {
        

        // GET: api/Products            -> all products
        // GET: api/Products?GroupId=5  -> products of a group (the old GetProduct(int GroupId))
        [HttpGet("{id?}")]
        public IActionResult GetProducts(int? id, [FromQuery] int? GroupId)
        {
            using var db = new SalinoContext();
            IQueryable<Product> query = db.Products;
            if (GroupId.HasValue)
            {
                if (GroupId.Value == 0) return BadRequest();
                query = query.Where(x => x.GroupId == GroupId.Value);
            }

            // Scalar columns only: serialising lazy-loading proxies would pull in the whole object graph.
            var result = query.Select(p => new
            {
                p.Id, p.CodeKala, p.GroupId, p.Name, p.Imgdetail, p.ImgdetailMob, p.ImgMain, p.ImgMainMob,
                p.Description, p.Seen, p.CreateDate, p.CreateTime, p.IsShow, p.ColorId, p.CreateDateEnglish,
            }).ToList();
            return Ok(result);
        }
    }
}
