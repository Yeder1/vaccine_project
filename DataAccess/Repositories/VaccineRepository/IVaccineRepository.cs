using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories
{
    public interface IVaccineRepository: IBaseRepository<Vaccine>
    {
        public void Deactivate(Vaccine vaccine);
    }

}
