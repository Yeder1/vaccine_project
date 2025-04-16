using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        Image? FindImage(Expression<Func<Image, bool>> expression);
    }
}
