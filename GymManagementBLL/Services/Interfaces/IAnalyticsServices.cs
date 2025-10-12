using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.ViewModels.AnalyticsViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IAnalyticsServices
    {
        AnalyticsViewModel GetAnalyticsData();
    }
}
