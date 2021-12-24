using Microsoft.EntityFrameworkCore;
using NoteAPI.Models.Number;

namespace NoteAPI.Contexts.Number
{
    public interface INumberContext
    {
        DbSet<NumberModel> numberModel { get; set; }
        int NumberSaveChange();
    }
}