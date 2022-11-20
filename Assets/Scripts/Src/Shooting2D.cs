using QFramework;

namespace ShootingDemo
{
    public class Shooting2D : Architecture<Shooting2D>
    {
        protected override void Init()
        {
            this.RegisterSystem<IStatSystem>(new StatSystem());
            this.RegisterSystem<IGunSystem>(new GunSystem());
            this.RegisterModel<IPlayerModel>(new PlayerModel());
        }
    }
}
