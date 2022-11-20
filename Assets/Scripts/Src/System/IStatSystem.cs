using QFramework;

namespace ShootingDemo
{
    public interface IStatSystem : ISystem
    {
        BindableProperty<int> KillCount { get; }
    }

    public class StatSystem : AbstractSystem, IStatSystem
    {
        public BindableProperty<int> KillCount {get;} = new BindableProperty<int>()
        {
            Value = 0
        };

        protected override void OnInit()
        {
            
        }
    }
}
