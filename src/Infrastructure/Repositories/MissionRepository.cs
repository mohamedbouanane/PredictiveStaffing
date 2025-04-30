namespace Infrastructure.Repositories;

using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Application.Repositories;
using Domain.Entities;

public class MissionRepository(DbContext dbContext) : BaseAsyncRepository<Mission>(dbContext), IMissionRepository
{
}
