using Microsoft.EntityFrameworkCore;
using MTG_OnlineTraining.Data;
using MTG_OnlineTraining.Models;
using MTG_OnlineTraining.ViewModel;

namespace MTG_OnlineTraining.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbConntext _db;

        public AdminServices(IWebHostEnvironment webHostEnvironment, ApplicationDbConntext db)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }

        public List<AdminProgram> GetProgramFromTheTable()
        {
            var adminProgramss = _db.AdminProgram.Where(p => p.IsActive && p.Id > 0).ToList();
            if (adminProgramss.Any())
            {
                return adminProgramss;
            }
            return adminProgramss;
        }

        public string ProgramCreate(AdminProgramViewModel adminProgramViewModel)
        {
            string ProgramPixFilePath = string.Empty;

            if (adminProgramViewModel.ProgramImageUrl == null)
            {
                return ("Upload Program Image");
            }
            else
            {
                ProgramPixFilePath = UploadedFile(adminProgramViewModel.ProgramImageUrl, "/ProgramImageRep/");
            }
            var adminprogramss = new AdminProgram
            {
                ProgramName = adminProgramViewModel.ProgramName,
                ProgramDescription = adminProgramViewModel.ProgramDescription,
                Duration = adminProgramViewModel.Duration,
                ProgramImg = ProgramPixFilePath,
                CreatedDate = DateTime.Now,
                IsActive = true,
            };

            if (adminprogramss != null)
            {
                _db.AdminProgram.Add(adminprogramss);
                _db.SaveChanges();
                return ("Program Created Successfully");
            }

            return ("Registion Fail");
        }



        public string UploadedFile(IFormFile filesUrl, string fileLocation)
        {
            string uniqueFileName = string.Empty;

            if (filesUrl != null)
            {
                var upPath = fileLocation.Trim('/');
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, upPath);
                
                string pathString = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", upPath);
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + filesUrl.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    filesUrl.CopyTo(fileStream);
                }
            }
            var generatedPictureFilePath = fileLocation + uniqueFileName;
            return generatedPictureFilePath;
        }


        public List<Materials> GetMaterialsFromTheTable()
        {
            var materialss = _db.Materials.Where(m => m.AdminProgramId != null && m.Id > 0).Include(m => m.AdminProgram).ToList();
            if (materialss.Any())
            {
                return materialss;
            }
            return materialss;
        }


        public string MaterialCreate(MaterialsViewModel materialsViewModel)
        {
            string MaterialPixFilePath = string.Empty;

            if (materialsViewModel.HandOutUrl == null)
            {
                return ("Upload Material");
            }
            else
            {
                MaterialPixFilePath = UploadedFile(materialsViewModel.HandOutUrl, "/TraningMaterials/");
            }
            var TrainingMaterial = new Materials
            {
                AdminProgramId = materialsViewModel.AdminProgramId,
                Title = materialsViewModel.Title,
                FlowOrder = materialsViewModel.FlowOrder,
                File = MaterialPixFilePath,
            };

            if (TrainingMaterial != null)
            {
                _db.Materials.Add(TrainingMaterial);
                _db.SaveChanges();
                return ("Material Created Successfully");
            }

            return ("Material Upload Failed");
        }

        public List<AdminProgram> AllPrograms()
        {
            var myPrograms = _db.AdminProgram.Where(s => s.Id != null).ToList();

            if (myPrograms.Any())
            {
                return myPrograms;
            }
            return myPrograms;
        }

        public StudentPrograms ProgramActivation(Guid Id)
        {
            var updateme = _db.StudentPrograms.Where(i => i.Id == Id).FirstOrDefault();
            if (updateme != null)
            {
                updateme.IsActive = true;

                _db.StudentPrograms.Update(updateme);
                _db.SaveChanges();

                return updateme;
            }
            else
            {
                return null;
            }

        }

        public StudentPrograms ProgramDeactivation(Guid Id)
        {
            var programIdForDeactivation = _db.StudentPrograms.Where(i => i.Id == Id).FirstOrDefault();
            if (programIdForDeactivation != null)
            {
                programIdForDeactivation.IsActive = false;

                _db.StudentPrograms.Update(programIdForDeactivation);
                _db.SaveChanges();

                return programIdForDeactivation;
            }
            else
            {
                return null;
            }
        }

        //Admin Program Edit and Delete Get Action
        public AdminProgramViewModel EditandDeleteProgramView(int? Id)
        {
            var programToEdit = _db.AdminProgram.Find(Id);
            if(programToEdit != null)
            {
                var oldProgram = new AdminProgramViewModel
                {
                    ProgramName = programToEdit.ProgramName,
                    ProgramDescription = programToEdit.ProgramDescription,
                    Duration = programToEdit.Duration,
                    IsActive = programToEdit.IsActive,
                };
                if (oldProgram != null)
                {
                    return (oldProgram);
                }
            }
            return null;
        }

        //Admin Program Edit and Delete Post Action
        public string UpdateTheEditedProgram(AdminProgramViewModel adminProgramViewModel, int? Id)
        {
            string ProgramPixFilePath = string.Empty;
            if (adminProgramViewModel.ProgramImageUrl == null)
            {
                return ("Upload Image");
            }
            else
            {
                ProgramPixFilePath = UploadedFile(adminProgramViewModel.ProgramImageUrl, "/ProgramImageRep/");
            }
            
            var programToEdit = _db.AdminProgram.Find(Id);
            if (programToEdit != null)
            {
                programToEdit.ProgramName = adminProgramViewModel.ProgramName;
                programToEdit.ProgramDescription = adminProgramViewModel.ProgramDescription;
                programToEdit.Duration = adminProgramViewModel.Duration;
                programToEdit.ProgramImg = ProgramPixFilePath;
                programToEdit.IsActive = true;

                _db.AdminProgram.Update(programToEdit);
                _db.SaveChanges();

                return "Program Updated Successfully";
            }
            else
            {
                return ("Program Updated Failed");
            }
        }

        public string DeleteSelectedProgram(AdminProgramViewModel adminProgramViewModel, int? Id)
        {
            var programToEdit = _db.AdminProgram.Find(Id);
            if (programToEdit != null)
            {
                programToEdit.IsActive = false;

                _db.AdminProgram.Update(programToEdit);
                _db.SaveChanges();

                return "Program Deleted Successfully";
            }
            else
            {
                return ("Program Removal Failed");
            }
        }

        //Material Edit Post Action
        public string UpdateEditedMaterial(MaterialsViewModel materialsViewModel, string base64)
        {
            if(base64 == null)
            {
                return ("Upload Training Document");
            }
            else if (materialsViewModel != null)
            {
                var OldMaterial = _db.Materials.Find(materialsViewModel.Id);

                OldMaterial.AdminProgramId = materialsViewModel.AdminProgramId;
                OldMaterial.Title = materialsViewModel.Title;
                OldMaterial.FlowOrder = materialsViewModel.FlowOrder ;
                OldMaterial.File = base64;

                _db.Materials.Update(OldMaterial);
                _db.SaveChanges();

                return "Material Updated Successfully";
            }
            else
            {
                return ("Material Updated Failed");
            }
        }

        //Materail Delete Post Action
        public string DeleteSelectedMaterial(MaterialsViewModel materialsViewModel)
        {
            var OldMaterial = _db.Materials.Find(materialsViewModel.Id);
            if (OldMaterial != null)
            {
                _db.Materials.Remove(OldMaterial);
                _db.SaveChanges();

                return "Program Deleted Successfully";
            }
            else
            {
                return ("Program Deletion Failed");
            }
        }
    }
}
