using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class TrainerrService : ITrainerrService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerrService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public bool CreateTrainer(CreateTrainerViewModel CreateTrainer)
        {
            var Repo = _unitOfWork.GetRepository<Trainer>();
            if (IsEmailExists(CreateTrainer.Email) || IsPhoneExists(CreateTrainer.Phone))
                return false;
            var trainer = new Trainer()
            {
                Name = CreateTrainer.Name,
                Email = CreateTrainer.Email,
                Phone = CreateTrainer.Phone,
                DateOfBirth = CreateTrainer.DateOfBirth,
                Specialties = CreateTrainer.Specialites,
                Gender = CreateTrainer.Gender,
                Address = new Address()
                {
                    BuildingNumber = CreateTrainer.BuildingNumber,
                    Street = CreateTrainer.Street,
                    City = CreateTrainer.City
                }
            };
            Repo.Add(trainer);
            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (Trainers is null || !Trainers.Any()) return [];
            return Trainers.Select(x => new TrainerViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Specialites = x.Specialties
            });
        }

        public TrainerViewModel? GetTrainerDetails(int TrainerId)
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (Trainers is null) return null;
            return new TrainerViewModel
            {
                Email = Trainers.Email,
                Phone = Trainers.Phone,
                Name = Trainers.Name,
                Specialites = Trainers.Specialties
            };
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId)
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (Trainers is null) return null;
            return new TrainerToUpdateViewModel
            {
                Name = Trainers.Name,
                Email = Trainers.Email,
                Phone = Trainers.Phone,
                Street = Trainers.Address.Street,
                BuildingNumber = Trainers.Address.BuildingNumber,
                City = Trainers.Address.City,
                Specialites = Trainers.Specialties
            };
        }

        public bool RemoveTrainer(int TrainerId)
        {
            var Repo = _unitOfWork.GetRepository<Trainer>();
            var TrainerToRemove = Repo.GetById(TrainerId);
            if (TrainerToRemove is null || HasActiveSessions(TrainerId)) return false;
            Repo.Delete(TrainerToRemove.Id);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool UpdateTrainerDetails(UpdateTrainerViewModel UdateTrainer, int TrainerId)
        {
            var Repo = _unitOfWork.GetRepository<Trainer>();
            var TrainerToUpdate = Repo.GetById(TrainerId);
            if (TrainerToUpdate is null || IsEmailExists(UdateTrainer.Email) || IsPhoneExists(UdateTrainer.Phone)) return false;

            TrainerToUpdate.Email = UdateTrainer.Email;
            TrainerToUpdate.Phone = UdateTrainer.Phone;
            TrainerToUpdate.Address.BuildingNumber = UdateTrainer.BuildingNumber;
            TrainerToUpdate.Address.Street = UdateTrainer.Street;
            TrainerToUpdate.Address.City = UdateTrainer.City;
            TrainerToUpdate.Specialties = UdateTrainer.Specialites;
            TrainerToUpdate.UpdatedAt = DateTime.Now;
            Repo.Update(TrainerToUpdate);
            return _unitOfWork.SaveChanges() > 0;

        }



        #region Helper Method
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email).Any();
        }

        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == phone).Any();
        }
        private bool HasActiveSessions(int Id)
        {
            var ActiveSessions = _unitOfWork.GetRepository<Session>()
                        .GetAll(x => x.TrainerId == Id && x.StartDate > DateTime.Now).Any();
            return ActiveSessions;
        }

        #endregion
    }
}
