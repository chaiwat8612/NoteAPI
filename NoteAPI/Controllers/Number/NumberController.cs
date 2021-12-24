using Microsoft.AspNetCore.Mvc;
using NoteAPI.Models.Number;
using NoteAPI.Models.Result;
using NoteAPI.Services.Number;

namespace NoteAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Number")]

    public class NumberController : Controller
    {
        readonly private INumberService _numberService;

        public NumberController(INumberService numberService)
        {
            this._numberService = numberService;
        }

        [HttpGet("GetNumberList")]
        public JsonResult GetNumberList()
        {
            ResultModel numberResult = new ResultModel
            {
                status = 200,
                message = "Success",
                data = this._numberService.GetNumberList()
            };
            return Json(numberResult);
        }

        [HttpPost("SaveNewNumber")]
        public JsonResult SaveNewNumber([FromBody]SaveNewNumberModel saveNewNumber)
        {
            ResultModel resultNewNumberModel = this._numberService.SaveNewNumber(saveNewNumber);
            return Json(resultNewNumberModel);
        }
    }
}
