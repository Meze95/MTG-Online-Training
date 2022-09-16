using MTG_OnlineTraining.Models;

namespace MTG_OnlineTraining.Services
{
    public interface IDropDownServices
    {
        Task<List<AdminProgram>> GetPrograms();
    }
}
