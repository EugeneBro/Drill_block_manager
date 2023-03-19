using DrillBlockManager.Database.Models;

namespace DrillBlockManager.Controllers.Models
{
    public abstract class DrillBlockBodyApiModel
    {
        public string Name { get; init; }
        public IEnumerable<DrillBlockPointApiModel> DrillBlockPoints { get; init; }
    }

    public class DrillBlockCreateApiModel : DrillBlockBodyApiModel
    {

    }

    public class DrillBlockUpdateApiModel : DrillBlockBodyApiModel
    {
        public Guid Id { get; init; }
    }

    public class DrillBlockPointApiModel
    {
        public int PositionInSequence { get; init; }
        public Coordinate Coordinate { get; init; }
    }
}
