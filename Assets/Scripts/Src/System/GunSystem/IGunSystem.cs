using QFramework;
using System.Collections.Generic;
using System.Linq;

namespace ShootingDemo
{
    public interface IGunSystem : ISystem
    {
        GunInfo CurrentGun { get; }

        void PickUpGun(string name, int bulletInGun, int bulletOutGun);
    }

    public class GunSystem : AbstractSystem, IGunSystem
    {
        private Queue<GunInfo> mGunInfos = new();

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

        public void PickUpGun(string name, int bulletInGun, int bulletOutGun)
        {
            if (CurrentGun.Name.Value == name)
            {
                CurrentGun.BulletCountInGun.Value += bulletInGun;
                CurrentGun.BulletCountOutGun.Value += bulletOutGun;
            }
            else if (mGunInfos.Any(gunInfo => gunInfo.Name.Value == name))
            {
                var gunInfo = mGunInfos.First(gunInfo => gunInfo.Name.Value == name);
                gunInfo.BulletCountInGun.Value += bulletInGun;
                gunInfo.BulletCountOutGun.Value += bulletOutGun;
            }
            else
            {
                var currentGunInfo = new GunInfo()
                {
                    Name = new BindableProperty<string>() { Value = name },
                    BulletCountInGun = new BindableProperty<int>() { Value = bulletInGun },
                    BulletCountOutGun = new BindableProperty<int>() { Value = bulletOutGun },
                    State = new BindableProperty<GunState>() { Value = GunState.IDLE }
                };

                mGunInfos.Enqueue(currentGunInfo);

                CurrentGun.Name.Value = name;
                CurrentGun.BulletCountInGun.Value = bulletInGun;
                currentGunInfo.BulletCountOutGun.Value = bulletOutGun;
                currentGunInfo.State.Value = GunState.IDLE;

                this.SendEvent(new OnCurrentGunChange() { Name = name });
            }
        }

        protected override void OnInit()
        {

        }
    }
}
