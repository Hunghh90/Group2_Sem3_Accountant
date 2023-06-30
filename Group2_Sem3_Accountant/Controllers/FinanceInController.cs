using Group2_Sem3_Accountant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Group2_Sem3_Accountant.Controllers
{
    [ApiController]
    [Route("api/financeins")]
    public class FinanceInController : ControllerBase
    {
        private readonly Group2Sem3Context _context;
        public FinanceInController(Group2Sem3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index() 
        {
            var fi = _context.Financeins.ToList<Financein>();
            return Ok(fi);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get (int id)
        {
            var fi = _context.Financeins.Find(id);
            return Ok(fi);
        }
    }
}
