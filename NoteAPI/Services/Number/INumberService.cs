using System.Collections.Generic;
using NoteAPI.Models.Number;
using NoteAPI.Models.Result;

namespace NoteAPI.Services.Number
{
    public interface INumberService
    {
        List<NumberModel> GetNumberList();
        ResultModel SaveNewNumber(SaveNewNumberModel saveNewnumberModel);
    }
}