namespace Application.Repositories;

using Application.Repositories.Common;
using Domain.Entities;

public interface IConsultantRepository : IBaseAsyncRepository<Consultant>
{
    Task<List<Consultant>> GetAllWithMissionsAsync(CancellationToken cancellationToken);
}
