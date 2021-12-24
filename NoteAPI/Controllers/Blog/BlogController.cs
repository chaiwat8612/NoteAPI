using Microsoft.AspNetCore.Mvc;
using NoteAPI.Models.Blog;
using NoteAPI.Models.Result;
using NoteAPI.Services.Blog;

namespace NoteAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Blog")]

    public class BlogController : Controller
    {
        readonly private IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            this._blogService = blogService;
        }

        [HttpGet("GetBlogList")]
        public JsonResult GetBlogList()
        {
            ResultModel blogResult = new ResultModel
            {
                status = 200,
                message = "Success",
                data = this._blogService.GetBlogList()
            };
            return Json(blogResult);
        }

        [HttpGet("GetBlogById/{blogId}")]
        public JsonResult GetBlogById(string blogId)
        {
            ResultModel blogResult = new ResultModel
            {
                status = 200,
                message = "Success",
                data = this._blogService.GetBlogById(blogId.Trim())
            };
            return Json(blogResult);
        }

        [HttpPost("SaveNewBlog")]
        public JsonResult SaveNewBlog([FromBody]SaveNewBlogModel saveNewBlog)
        {
            ResultModel resultNewNumberModel = this._blogService.SaveNewBlog(saveNewBlog);
            return Json(resultNewNumberModel);
        }
    }
}
