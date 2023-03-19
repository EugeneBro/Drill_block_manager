using DrillBlockManager.Controllers.Models;
using DrillBlockManager.Database;
using DrillBlockManager.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DrillBlockManager.Services
{
    public class HoleService : ServiceBase
    {
        public HoleService(IServiceProvider serviceProvider
            , ILogger<HoleService> logger) : base(serviceProvider, logger)
        {
        }

        public async Task CreateAsync(HoleCreateApiModel model
            , CancellationToken stoppingToken = default)
        {
            await using var context = GetContext<PostgreDbContext>();

            if (await context.Holes.AnyAsync(x => x.Name.ToUpper() == model.Name.ToUpper()
                    , stoppingToken))
                throw new Exception("Entity already exists");

            var added = context.Holes.Add(new Hole()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Depth = model.Depth
            }).Entity;

            if (model.HolePoints.Any())
            {
                //TODO: check coordinates

                var points =
                    model.HolePoints.Select(x => new HolePoint()
                    {
                        Id = Guid.NewGuid(),
                        HoleId = added.Id,
                        Coordinate = x.Coordinate
                    });

                context.HolePoints.AddRange(points);
            }

            await context.SaveChangesAsync(stoppingToken);
        }

        public async Task UpdateAsync(HoleUpdateApiModel model
            , CancellationToken stoppingToken = default)
        {
            await using var context = GetContext<PostgreDbContext>();

            var updateItem = await context.Holes
                .Include(x => x.HolePoints)
                .FirstOrDefaultAsync(x => x.Id == model.Id, stoppingToken);

            if (updateItem == null)
                throw new Exception("Entity not found");

            updateItem.Name = model.Name;
            updateItem.Depth = model.Depth;

            context.Holes.Update(updateItem);

            if (model.HolePoints.Any())
            {
                //TODO: check coordinates

                var newPoints =
                    model.HolePoints.Select(x => new HolePoint()
                    {
                        Id = Guid.NewGuid(),
                        HoleId = updateItem.Id,
                        Coordinate = x.Coordinate
                    });

                context.HolePoints.RemoveRange(updateItem.HolePoints);
                context.HolePoints.AddRange(newPoints);
            }

            await context.SaveChangesAsync(stoppingToken);
        }

        public async Task DeleteAsync(IEnumerable<Guid> ids
            , CancellationToken stoppingToken = default)
        {
            await using var context = GetContext<PostgreDbContext>();

            var deleteItems = context.Holes
                .Where(x => ids.Contains(x.Id)); // query recompilation, not good

            if (deleteItems.Count() != ids.Count())
                throw new Exception("ivalid ids");

            // we can set up a cascading delete on db
            var deletePoints = context.HolePoints
                .Where(x => ids.Contains(x.HoleId)); // query recompilation

            context.HolePoints.RemoveRange(deletePoints);
            context.Holes.RemoveRange(deleteItems);

            await context.SaveChangesAsync(stoppingToken);
        }
    }
}
