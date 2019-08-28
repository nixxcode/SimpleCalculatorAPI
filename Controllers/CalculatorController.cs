using Microsoft.AspNetCore.Mvc;
using SimpleCalculatorAPI.Models;


namespace SimpleCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {
        public ActionResult Index()
        {
            return new ContentResult();
        }

        [HttpGet("{Operation}/{Ops?}")]
        public ActionResult Calculate(
            CalculatorModel model,
            [ModelBinder(BinderType = typeof(CustomFloatArrayModelBinder))] float[] ops)
        {
            if (ops == null)
            {
                ModelState.AddModelError("Operands", "Invalid operands");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Operands = ops;

            model.calculate();
            return new JsonResult(model.Result);
        }
    }
}
