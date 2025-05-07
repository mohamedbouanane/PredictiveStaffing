namespace Infrastructure.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

public class ConsultantRepository(AppDbContext dbContext) : BaseAsyncRepository<Consultant>(dbContext), IConsultantRepository
{
    public async Task<List<Consultant>> GetAllWithMissionsAsync(CancellationToken cancellationToken)
    {
        return await base._dbSet
            .AsNoTracking()
            .Include(c => c.Missions)
            .ToListAsync(cancellationToken);
    }
}
