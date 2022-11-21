using QFramework;

namespace ShootingDemo
{
    public interface IGunSystem : ISystem
    {
        GunInfo CurrentGun { get; }
    }

    public class GunSystem : AbstractSystem, IGunSystem
    {
        // 使用默认的get实现，并指定初始化值
        public GunInfo CurrentGun { get; } = new GunInfo()
        {
            BulletCountInGun = new BindableProperty<int>()
            {
                Value = 3
            },
            Name = new BindableProperty<string>()
            {
                Value = "Pistol"
            },
            State = new BindableProperty<GunState>()
            {
                Value = GunState.IDLE
            },
            BulletCountOutGun = new BindableProperty<int>()
            {
                Value = 0
            }
        };

        protected override void OnInit()
        {

        }
    }
}
