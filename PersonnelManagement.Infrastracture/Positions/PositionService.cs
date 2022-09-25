using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Positions;
using PersonnelManagement.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.Positions
{
    public class PositionService : IPositionService
    {
        private readonly IApplicationDbContext _dbContext;

        public PositionService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Position> GetAsync(Guid Id)
        {
            return await _dbContext.Positions.FindAsync(Id);
        }

        public async Task<Position> CreateAsync(Position position)
        {
            await _dbContext.Positions.AddAsync(position);
            var added = await _dbContext.SaveChangesAsync() > 0;

            if (added)
            {
                var newPosition = await GetAsync(position.Id);
                return newPosition;
            }

            return null;
        }
    }
}
