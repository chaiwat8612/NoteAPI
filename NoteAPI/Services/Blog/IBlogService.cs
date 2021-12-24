using System.Collections.Generic;
using NoteAPI.Models.Blog;
using NoteAPI.Models.Result;

namespace NoteAPI.Services.Blog
{
    public interface IBlogService
    {
        List<BlogModel> GetBlogList();
        BlogModel GetBlogById(string blogId);
        ResultModel SaveNewBlog(SaveNewBlogModel saveNewnumberModel);
    }
}