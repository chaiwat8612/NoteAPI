using Microsoft.EntityFrameworkCore;
using NoteAPI.Models.Blog;

namespace NoteAPI.Contexts.Blog
{
    public interface IBlogContext
    {
        DbSet<BlogModel> blogModel { get; set; }
        int BlogSaveChange();
    }
}