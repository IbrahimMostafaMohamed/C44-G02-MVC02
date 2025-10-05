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
    internal class HealthRecordRepository : IHealthRecordRepositories
    {
        private readonly GymDbContext _dbContext = new GymDbContext();
        public int Add(HealthRecord H)
        {
            _dbContext.Add(H);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var H = _dbContext.HealthRecords.Find(Id);
            if (H == null)
                return 0;
            _dbContext.HealthRecords.Remove(H);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<HealthRecord> GetAll() => _dbContext.HealthRecords.ToList();


        public HealthRecord? GetById(int Id)
        {
            return _dbContext.HealthRecords.Find(Id);
        }

        public int Update(HealthRecord H)
        {
            _dbContext.HealthRecords.Update(H);
            return _dbContext.SaveChanges();
        }
    }
}
