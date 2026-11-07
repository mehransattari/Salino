using Salino.DataLayer;

namespace SalinoMvc5.Controllers.WebApi
{
    /// <summary>Port of the Web API 2 ColorsProductsController (GET endpoints only).</summary>
    [Route("api/ColorsProducts")]
    public class ColorsProductsController : ControllerBase
    {
        // GET: api/ColorsProducts                -> all rows
        // GET: api/ColorsProducts?ProductId=5    -> colour names of a product
        [HttpGet("{id?}")]
        public IActionResult GetColorsProduct(int? id, [FromQuery] int? ProductId)
        {
            using var db = new SalinoContext();
            if (!ProductId.HasValue)
                return Ok(db.ColorsProducts.Select(x => new { x.Id, x.ColorId, x.ProductId }).ToList());

            var colors = (from cp in db.ColorsProducts
                          join c in db.Colors on cp.ColorId equals c.Id
                          where cp.ProductId == ProductId.Value
                          orderby cp.Id
                          select c.color).ToList();
            return Ok(colors);
        }
    }
}
