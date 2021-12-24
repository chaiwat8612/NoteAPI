using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteAPI.Models.Number
{
    [Table("numberTab")]
    public class NumberModel
    {
        public string numberId { get; set; }
        public string status { get; set; }
        public int numberValue { get; set; }
    }

    public class SaveNewNumberModel
    {
        public string status { get; set; }
        public int numberValue { get; set; }
    }
}