using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;

namespace GymManagementBLL.Services.Classes
{
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public bool CreatSession(CreateSessionViewModel creeatSession)
        {
            try
            {
                if (!IsTrainerExists(creeatSession.TrainerId)) return false;
                if (!IsCategoryExists(creeatSession.CategoryId)) return false;
                if (!IsDateTimeValid(creeatSession.StartDate, creeatSession.EndDate)) return false;
                if (creeatSession.Capacity > 25 || creeatSession.Capacity < 0) return false;
                var SessionEntity = _mapper.Map<Session>(creeatSession);
                _unitOfWork.GetRepository<Session>().Add(SessionEntity);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create Session Failed {ex}");
                return false;
            }
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainersAndCategories();
            if (!Sessions.Any()) return [];

            //return Sessions.Select(x => new SessionViewModel
            //{
            //    Id = x.Id,
            //    Description = x.Description,
            //    StartDate = x.StartDate,
            //    EndDate = x.EndDate,
            //    Capacity = x.Capacity,
            //    TrainerName = x.Trainer.Name,
            //    CategoryName = x.Category.CategoryName,
            //    AvailableSlots = x.Capacity - _unitOfWork.SessionRepository
            //                            .GetCountOfBookSlots(x.Id)

            //});
            var MappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(Sessions);
            foreach (var session in MappedSessions)
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id);
            return MappedSessions;
        }

        public SessionViewModel? GetSessionById(int id)
        {
            var sessions = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(id);
            if (sessions is null) return null;
            return new SessionViewModel
            {
                Description = sessions.Description,
                StartDate = sessions.StartDate,
                TrainerName = sessions.Trainer.Name,
                CategoryName = sessions.Category.CategoryName,
                Capacity = sessions.Capacity,
                EndDate = sessions.EndDate,
                AvailableSlots = sessions.Capacity - _unitOfWork.SessionRepository.GetCountOfBookSlots(sessions.Id)
            };
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int SessionId)
        {
            var session = _unitOfWork.SessionRepository.GetById(SessionId);
            if (!IsSessionAvilableForUpdating(session!)) return null;
            return _mapper.Map<UpdateSessionViewModel>(session);

        }

        public bool UpdateSession(UpdateSessionViewModel UpdateSession, int SessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(SessionId);
                if(!IsSessionAvilableForUpdating(Session!)) return false;
                if(!IsTrainerExists(UpdateSession.TrainerId)) return false;
                if(!IsDateTimeValid(UpdateSession.StartDate , UpdateSession.EndDate)) return false;
                _mapper.Map(UpdateSession, Session);
                Session!.UpdatedAt = DateTime.Now;
                _unitOfWork.SessionRepository.Update(Session);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Session Failed{ex}");
                return false;
            }
        }

        public bool RemoveSession(int SessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(SessionId);
                if (!IsSessionAvilableForRemoving(Session!)) return false;
            
                _unitOfWork.SessionRepository.Delete(Session!.Id);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Session Failed{ex}");
                return false;
            }

        }

        #region Helper Methods
        private bool IsSessionAvilableForUpdating(Session session)
        {
            if (session == null) return false;
            if (session.EndDate < DateTime.Now) return false;
            if (session.StartDate <= DateTime.Now) return false;
            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id) > 0;
            if (HasActiveBooking) return false;
            return true;
        }
        private bool IsSessionAvilableForRemoving(Session session)
        {
            if (session == null) return false;
            if (session.StartDate > DateTime.Now) return false;
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;
            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookSlots(session.Id) > 0;
            if (HasActiveBooking) return false;
            return true;
        }
        private bool IsTrainerExists(int TrainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(TrainerId) != null;
        }
        private bool IsCategoryExists(int CategoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(CategoryId) != null;
        }
        private bool IsDateTimeValid(DateTime StartDate, DateTime EndDate)
        {
            return StartDate < EndDate;
        }

        #endregion

    }
}
