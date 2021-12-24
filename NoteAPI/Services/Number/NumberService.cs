using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using NoteAPI.Contexts.Number;
using NoteAPI.Models.Number;
using NoteAPI.Models.Result;
using NoteAPI.Services.Configure;
using NoteAPI.Service.Utility;

namespace NoteAPI.Services.Number
{
    public class NumberService : INumberService
    {
        readonly private ConfigureService _config = new ConfigureService();
        readonly private IConfiguration _getConfig;
        readonly private INumberContext _numberContext;
        readonly private GenTransactionNumberService genTransactionNumberService;

        readonly private string _statusInActive = "N";
        readonly private string _bannerImage = ""; 

        public NumberService(INumberContext numberContext, GenTransactionNumberService genTransactionNumberService = null)
        {
            #region "Get_config"
            _getConfig = _config.GetConfigFromAppsetting();
            //_bannerImage = _getConfig["Config:__"];
            #endregion

            this._numberContext = numberContext;
            
            if(genTransactionNumberService == null)
            {
                this.genTransactionNumberService = new GenTransactionNumberService();
            }
            else
            {
                this.genTransactionNumberService = genTransactionNumberService;
            }

        }

        public List<NumberModel> GetNumberList()
        {
            return GenList(this._numberContext.numberModel
                    .Where(m => m.status != _statusInActive)
                    .OrderByDescending(m => m.numberId)
                    .ToList());
        }

        private List<NumberModel> GenList(List<NumberModel> numberList)
        {
            return numberList;
        }

        public ResultModel SaveNewNumber(SaveNewNumberModel saveNewnumberModel)
        {
            string errorMessage = "";
            ResultModel result = new ResultModel();

            NumberModel numberModel = new NumberModel();

            string maxTransactionNumber = GetMaxTransactionNumber();

            numberModel.numberId = this.genTransactionNumberService.GenTransactionNumber(maxTransactionNumber, DateTime.Now).Trim();
            numberModel.status = saveNewnumberModel.status.Trim();
            numberModel.numberValue = saveNewnumberModel.numberValue;

            if (IsSaveNewNumber(numberModel))
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

        public bool IsSaveNewNumber(NumberModel numberModel)
        {
            this._numberContext.numberModel.Add(numberModel);
            return this._numberContext.NumberSaveChange() > 0 ? true : false;
        }

        public string GetMaxTransactionNumber()
        {
            return this._numberContext.numberModel.Max(m => m.numberId);
        }
    }
}
