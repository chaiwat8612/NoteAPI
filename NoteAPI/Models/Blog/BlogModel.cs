using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteAPI.Models.Blog
{
    [Table("blogTab")]
    public class BlogModel
    {
        public string blogId { get; set; }
        public string blogDescription { get; set; }
    }

    public class SaveNewBlogModel
    {
        public string blogDescription { get; set; }
    }
}