using Core.Persistence.Repositories;
using Persistence.Contexts;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;

namespace RentACar.Persistence.Repositories
{
    public class BrandRepository : EfRepositoryBase<Brand, BaseDbContext>, IBrandRepository
    {
        public BrandRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
