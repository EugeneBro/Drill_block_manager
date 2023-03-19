using DrillBlockManager.Database.Models;

namespace DrillBlockManager.Controllers.Models
{
    public abstract class HoleBodyApiModel
    {
        public string Name { get; init; }
        public int Depth { get; init; }
        public IEnumerable<HolePointCreateApiModel> HolePoints { get; init; }
    }

    public class HoleCreateApiModel : HoleBodyApiModel
    {

    }

    public class HoleUpdateApiModel : HoleBodyApiModel
    {
        public Guid Id { get; init; }
    }

    public class HolePointCreateApiModel
    {
        public Coordinate Coordinate { get; init; }
    }
}
