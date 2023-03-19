using System.Diagnostics.SymbolStore;
using DrillBlockManager.Controllers.Models;
using DrillBlockManager.Database;
using DrillBlockManager.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DrillBlockManager.Services
{
    public class DrillBlockService : ServiceBase
    {
        public DrillBlockService(IServiceProvider serviceProvider
            , ILogger<DrillBlockService> logger) : base(serviceProvider, logger)
        {
        }

        public async Task CreateAsync(DrillBlockCreateApiModel model
            , CancellationToken stoppingToken = default)
        {
            await using var context = GetContext<PostgreDbContext>();

            if (await context.DrillBlocks.AnyAsync(x => x.Name.ToUpper() == model.Name.ToUpper()
                    , stoppingToken))
                throw new Exception("Entity already exists");

            var added = context.DrillBlocks.Add(new DrillBlock()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                UpdateDate = DateTime.UtcNow
            }).Entity;

            if (model.DrillBlockPoints.Any())
            {
                if (model.DrillBlockPoints.DistinctBy(x => x.PositionInSequence).Count() !=
                    model.DrillBlockPoints.Count())
                    throw new Exception("Invalid drill block point`s position in sequence");

                //TODO: check coordinates

                var points = 
                    model.DrillBlockPoints.Select(x => new DrillBlockPoint()
                    {
                        Id = Guid.NewGuid(),
                        DrillBlockId = added.Id,
                        Coordinate = x.Coordinate,
                        PositionInSequence = x.PositionInSequence
                    });

                context.DrillBlockPoints.AddRange(points);
            }

            await context.SaveChangesAsync(stoppingToken);
        }

        public async Task UpdateAsync(DrillBlockUpdateApiModel model
            , CancellationToken stoppingToken = default)
        {
            await using var context = GetContext<PostgreDbContext>();

            var updateItem = await context.DrillBlocks
                .Include(x => x.DrillBlockPoints)
                .FirstOrDefaultAsync(x => x.Id == model.Id, stoppingToken);

            if (updateItem == null)
                throw new Exception("Entity not found");

            updateItem.Name = model.Name;
            updateItem.UpdateDate = DateTime.UtcNow;
            context.DrillBlocks.Update(updateItem);

            if (model.DrillBlockPoints.Any())
            {
                if (model.DrillBlockPoints.DistinctBy(x => x.PositionInSequence).Count() !=
                    model.DrillBlockPoints.Count())
                    throw new Exception("Invalid drill block point`s position in sequence");

                //TODO: check coordinates

                var newPoints =
                    model.DrillBlockPoints.Select(x => new DrillBlockPoint()
                    {
                        Id = Guid.NewGuid(),
                        DrillBlockId = updateItem.Id,
                        Coordinate = x.Coordinate,
                        PositionInSequence = x.PositionInSequence
                    });

                context.DrillBlockPoints.RemoveRange(updateItem.DrillBlockPoints);
                context.DrillBlockPoints.AddRange(newPoints);
            }

            await context.SaveChangesAsync(stoppingToken);
        }

        public async Task DeleteAsync(IEnumerable<Guid> ids
            , CancellationToken stoppingToken = default)
        {
            await using var context = GetContext<PostgreDbContext>();

            var deleteItems = context.DrillBlocks
                .Where(x => ids.Contains(x.Id)); // query recompilation, not good

            if (deleteItems.Count() != ids.Count())
                throw new Exception("ivalid ids");

            // we can set up a cascading delete on db
            var deletePoints = context.DrillBlockPoints
                .Where(x => ids.Contains(x.DrillBlockId)); // query recompilation

            context.DrillBlockPoints.RemoveRange(deletePoints);
            context.DrillBlocks.RemoveRange(deleteItems);

            await context.SaveChangesAsync(stoppingToken);
        }
    }
}
