using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Data;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    internal class TrainerRepository : ITrainerRepositories
    {
        private readonly GymDbContext _dbContext;

        public TrainerRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Trainer T)
        {
            _dbContext.Add(T);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var T = _dbContext.Trainers.Find(Id);
            if (T == null)
                return 0;
            _dbContext.Trainers.Remove(T);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAll() => _dbContext.Trainers.ToList();


        public Trainer? GetById(int Id)
        {
            return _dbContext.Trainers.Find(Id);
        }

        public int Update(Trainer T)
        {
            _dbContext.Trainers.Update(T);
            return _dbContext.SaveChanges();
        }
    }
}
