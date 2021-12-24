using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using NoteAPI.Contexts.Blog;
using NoteAPI.Models.Blog;
using NoteAPI.Models.Result;
using NoteAPI.Services.Configure;
using NoteAPI.Service.Utility;

namespace NoteAPI.Services.Blog
{
    public class BlogService : IBlogService
    {
        readonly private ConfigureService _config = new ConfigureService();
        readonly private IConfiguration _getConfig;
        readonly private IBlogContext _blogContext;
        readonly private GenTransactionNumberService genTransactionNumberService;

        readonly private string _statusInActive = "N";
        readonly private string _bannerImage = ""; 

        public BlogService(IBlogContext numberContext, GenTransactionNumberService genTransactionNumberService = null)
        {
            #region "Get_config"
            _getConfig = _config.GetConfigFromAppsetting();
            //_bannerImage = _getConfig["Config:__"];
            #endregion

            this._blogContext = numberContext;
            
            if(genTransactionNumberService == null)
            {
                this.genTransactionNumberService = new GenTransactionNumberService();
            }
            else
            {
                this.genTransactionNumberService = genTransactionNumberService;
            }

        }

        public List<BlogModel> GetBlogList()
        {
            return GenList(this._blogContext.blogModel
                    //.Where(m => m.status != _statusInActive)
                    .OrderByDescending(m => m.blogId)
                    .ToList());
        }

        public BlogModel GetBlogById(string blogId)
        {
            return GenList(this._blogContext.blogModel
                    .Where(m => m.blogId == blogId)
                    .OrderByDescending(m => m.blogId)
                    .ToList())
                    .DefaultIfEmpty()
                    .First();
        }

        private List<BlogModel> GenList(List<BlogModel> numberList)
        {
            return numberList;
        }

        public ResultModel SaveNewBlog(SaveNewBlogModel saveNewBlogModel)
        {
            string errorMessage = "";
            ResultModel result = new ResultModel();

            BlogModel blogModel = new BlogModel();

            string maxTransactionNumber = GetMaxTransactionNumber();

            //blogModel.blogId = this.genTransactionNumberService.GenTransactionNumber(maxTransactionNumber, DateTime.Now).Trim();
            blogModel.blogId = this.genTransactionNumberService.GenTransactionNumber(maxTransactionNumber, DateTime.Now).Trim();
            blogModel.blogDescription = saveNewBlogModel.blogDescription;

            if (IsSaveNewBlog(blogModel))
            {
                result.status = 200;
                result.message = "Success";
            }
            else
            {
                ErrorModel errorModel = new ErrorModel
                {
                    code = 505,
                    message = errorMessage,
                    target = "S3"
                };
                result.status = 500;
                result.message = "Save New Number Fail."; 
            }

            return result;
        }

        public bool IsSaveNewBlog(BlogModel blogModel)
        {
            this._blogContext.blogModel.Add(blogModel);
            return this._blogContext.BlogSaveChange() > 0 ? true : false;
        }

        public string GetMaxTransactionNumber()
        {
            return this._blogContext.blogModel.Max(m => m.blogId);
        }
    }
}
