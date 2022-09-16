using Microsoft.EntityFrameworkCore;
using MTG_OnlineTraining.Data;
using MTG_OnlineTraining.Models;

namespace MTG_OnlineTraining.Services
{
    public class DropDownServices: IDropDownServices
    {
        private readonly ApplicationDbConntext _db;

        public DropDownServices(ApplicationDbConntext db)
        {
            _db = db;
        }

        public async Task<List<AdminProgram>>GetPrograms()
        {
            try
            {
                var common = new AdminProgram()
                {
                    Id = 0,
                    ProgramName = "Select Program"
                };
                var listOfPrograms = await _db.AdminProgram.Where(x => x.Id != 0 && x.IsActive != false).OrderBy(p => p.Id).ToListAsync();
                listOfPrograms.Insert(0, common);
                return listOfPrograms;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
