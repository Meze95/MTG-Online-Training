using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MTG_OnlineTraining.Data;
using MTG_OnlineTraining.Data.Enum;
using MTG_OnlineTraining.Models;
using MTG_OnlineTraining.ViewModel;

namespace MTG_OnlineTraining.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbConntext _db;

        public StudentServices(ApplicationDbConntext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public List<DropDown> GetDropDownEnumsList()
        {
            var common = new DropDown()
            {
                Id = 0,
                Name = "-- Select --"

            };
            var enumList = ((TrainingEnum[])Enum.GetValues(typeof(TrainingEnum))).Select(c => new DropDown() { Id = (int)c, Name = c.ToString() }).ToList();
            enumList.Insert(0, common);
            return enumList;
        }

        public string StudentProgramRegList(StudentProgramsViewModel studentProgramsViewModel, string username)
        {
            var Duration = _db.AdminProgram.Where(d => d.Id == studentProgramsViewModel.AdminProgramId).FirstOrDefault().Duration;

            var student = _userManager.FindByNameAsync(username).Result;

            var programCheck = _db.StudentPrograms.Where(n => n.AdminProgramId == studentProgramsViewModel.AdminProgramId && n.UserId == student.Id).FirstOrDefault();
            if(programCheck != null)
            {
                return "Program already registered";
            }

            var ProgRegList = new StudentPrograms
            {
                AdminProgramId = studentProgramsViewModel.AdminProgramId,
                UserId = student.Id,
                ModeOfTraining = studentProgramsViewModel.ModeOfTraining,
                StartingDate = studentProgramsViewModel.StartingDate.Date,
                EndingDate = studentProgramsViewModel.StartingDate.AddDays(Duration).Date,
                IsActive = false,
            };
            if (ProgRegList != null)
            {
                _db.StudentPrograms.Add(ProgRegList);
                _db.SaveChanges();
                return ("Program Registered Successfully");
            }
            return ("Program Registraion Failed");
        }

        public List<StudentPrograms> MyRegisteredPrograms(string username)
        {
            var student = _userManager.FindByNameAsync(username).Result;
            var myPrograms = new List<StudentPrograms>();
            if (student != null)
            {
                myPrograms = _db.StudentPrograms.Where(s => s.UserId == student.Id).Include(p => p.AdminProgram).ToList();

                if (myPrograms != null)
                {
                    return myPrograms;
                }
            }
            return myPrograms;
        }

        //Students Program Edit Post Action
        public string UpdateEditedStudentProgram(StudentProgramsViewModel studentProgramsViewModel)
        {
            var OldProgram = _db.StudentPrograms.Find(studentProgramsViewModel.Id);
            var Duration = _db.AdminProgram.Where(d => d.Id == studentProgramsViewModel.AdminProgramId).FirstOrDefault().Duration;
            if (DateTime.Now >= OldProgram?.StartingDate)
            {
                return "Program Already Started";
            }
            else if (OldProgram != null)
            {
                OldProgram.AdminProgramId = studentProgramsViewModel.AdminProgramId;
                OldProgram.ModeOfTraining = studentProgramsViewModel.ModeOfTraining;
                OldProgram.StartingDate = studentProgramsViewModel.StartingDate.Date;
                OldProgram.EndingDate = studentProgramsViewModel.StartingDate.AddDays(Duration).Date;
                OldProgram.IsActive = false;

                _db.StudentPrograms.Update(OldProgram);
                _db.SaveChanges();

                return "Program Updated Successfully";
            }
            else
            {
                return ("Program Updated Failed");
            }
        }

        //Students Program Delete Post Action
        public string DeleteSelectedStudentProgram(StudentProgramsViewModel studentProgramsViewModel)
        {
            var OldProgram = _db.StudentPrograms.Find(studentProgramsViewModel.Id);
            if (DateTime.Now >= OldProgram?.StartingDate)
            {
                return "Program Already Started";
            }
            else if (OldProgram != null)
            {
                _db.StudentPrograms.Remove(OldProgram);
                _db.SaveChanges();

                return "Program Deleted Successfully";
            }
            else
            {
                return ("Program Deletion Failed");
            }
        }

        public List<StudentPrograms> AllProgramReg()
        {
            var myPrograms = _db.StudentPrograms.Where(s => s.UserId != null).Include(p => p.AdminProgram).Include(u => u.Trainees).ToList();

            if (myPrograms.Any())
            {
                return myPrograms;
            }
            return myPrograms;
        }

        public List<Materials> StudentActiveMaterials(string username)
        {
            var materials = new List<Materials>();
            var user = _userManager.FindByNameAsync(username).Result;
            var activePrograms = _db.StudentPrograms.Where(s => s.UserId == user.Id && s.IsActive == true).ToList();

            if (activePrograms.Any())
            { 
                foreach(var program in activePrograms)
                {
                    var getMaterial = _db.Materials.Where(m => m.AdminProgramId == program.AdminProgramId).Include(a => a.AdminProgram).ToList();

                    if (getMaterial.Any())
                    {
                        materials.AddRange(getMaterial);
                    }
                }
                return materials;
            }
            return materials;
        }

    }
}
