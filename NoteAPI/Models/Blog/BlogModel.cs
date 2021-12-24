using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteAPI.Models.Blog
{
    [Table("blog_tab")]
    public class BlogModel
    {
        public string blogId { get; set; }
        public string description { get; set; }
    }

    public class SaveNewBlogModel
    {
        public string description { get; set; }
    }
}