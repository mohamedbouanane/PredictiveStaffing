namespace Infrastructure.Repositories;

using Application.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Common;

public class MissionRepository(AppDbContext dbContext) : BaseAsyncRepository<Mission>(dbContext), IMissionRepository
{
}
