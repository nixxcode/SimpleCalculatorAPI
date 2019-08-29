using System;
using Microsoft.AspNetCore.Mvc;
using SimpleCalculatorAPI.Models;


namespace SimpleCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {
        // api/calculator
        public ActionResult Index(CalculatorModel model)
        {
            return new JsonResult(new { operations = Enum.GetValues(typeof(CalculatorOperations)) });
        }

        // api/calculator/{operation}/{operands}
        [HttpGet("{Operation}/{Ops?}")]
        public ActionResult Calculate(
            CalculatorModel model,
            [ModelBinder(BinderType = typeof(CustomFloatArrayModelBinder))] float[] ops)
        {
            // If ops is null, it means we failed to bind the operands to a float array, likely because they are missing or invalid
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
