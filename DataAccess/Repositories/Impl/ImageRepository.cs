using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories.Impl
{
    public class ImageRepository : BaseRepository<Image>,IImageRepository
    {
        private readonly VaccinationManagementContext _context;

        public ImageRepository(VaccinationManagementContext context) : base(context)
        {
            _context = context;
        }

        public Image? FindImage(Expression<Func<Image, bool>> expression)
        {
            return _context.Images.Where(expression).FirstOrDefault();
        }
    }
}
