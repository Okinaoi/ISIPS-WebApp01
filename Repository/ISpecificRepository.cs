using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface ISpecificRepository<T> where T : class
    {
        IEnumerable<T> SelectByTechnician(int technicianId);
        IEnumerable<T> SelectForAdmin();
        
    }
}
