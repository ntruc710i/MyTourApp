using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTour.Services
{
    public interface IToursData<T>
    {
        Task<IEnumerable<T>> GetTourAsync(bool forceRefresh = false);
    }
}
