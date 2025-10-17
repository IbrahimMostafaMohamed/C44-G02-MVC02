using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.ViewModels.TrainerViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ITrainerrService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel CreateTrainer);
        TrainerViewModel? GetTrainerDetails(int TrainerId);
        TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId);
        bool UpdateTrainerDetails(UpdateTrainerViewModel UdateTrainer, int TrainerId);
        bool RemoveTrainer(int TrainerId);

    }
}
