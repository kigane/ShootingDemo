using QFramework;

namespace ShootingDemo
{
    public class Shooting2D : Architecture<Shooting2D>
    {
        protected override void Init()
        {
            RegisterSystem<IStatSystem>(new StatSystem());
            RegisterSystem<IGunSystem>(new GunSystem());
            RegisterSystem<ITimeSystem>(new TimeSystem());
            RegisterModel<IGunConfigModel>(new GunConfigModel());
            RegisterModel<IPlayerModel>(new PlayerModel());
        }
    }
}
