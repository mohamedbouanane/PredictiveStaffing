namespace Infrastructure.Repositories;

using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Application.Repositories;
using System.Threading.Tasks;
using Domain.Entities;

public class ConsultantRepository(DbContext dbContext) : BaseAsyncRepository<Consultant>(dbContext), IConsultantRepository
{
    public async Task<List<Consultant>> GetAllWithMissionsAsync(CancellationToken cancellationToken)
    {
        return await base._dbSet
            .AsNoTracking()
            .Include(c => c.Missions)
            .ToListAsync(cancellationToken);
    }
}
