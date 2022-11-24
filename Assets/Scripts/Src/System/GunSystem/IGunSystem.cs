using QFramework;
using System.Collections.Generic;
using System.Linq;

namespace ShootingDemo
{
    public interface IGunSystem : ISystem
    {
        GunInfo CurrentGun { get; }

        // 补给箱功能需要给每一把枪添加弹药
        Queue<GunInfo> GunInfos { get; }

        /// <summary>
        /// 1. 捡到当前枪 补充子弹  
        /// 2. 捡到已有枪 补充已有枪的子弹  
        /// 3. 捡到新枪   换枪
        /// </summary>
        /// <param name="name">捡到的枪械名称</param>
        /// <param name="bulletInGun">捡到的枪械现有子弹数</param>
        /// <param name="bulletOutGun">捡到的枪械备用子弹数</param>
        void PickUpGun(string name, int bulletInGun, int bulletOutGun);

        /// <summary>
        /// 缓存当前枪的信息到队列中
        /// 从队列中取出之前枪的信息，并设为当前枪
        /// </summary>
        void ShiftGun();
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

        public Queue<GunInfo> GunInfos => mGunInfos;

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
                EnqueueCurrentGun(name, bulletInGun, bulletOutGun);
                this.SendEvent(new OnCurrentGunChange() { Name = name });
            }
        }

        public void ShiftGun()
        {
            if (mGunInfos.Count == 0)
                return;
            var prevGun = mGunInfos.Dequeue();
            EnqueueCurrentGun(prevGun.Name.Value, prevGun.BulletCountInGun.Value, prevGun.BulletCountOutGun.Value);
            this.SendEvent(new OnCurrentGunChange() { Name = prevGun.Name.Value });
        }

        private void EnqueueCurrentGun(string name, int bulletInGun, int bulletOutGun)
        {
            var currentGunInfo = new GunInfo()
            { // 保存当前枪的信息
                Name = new BindableProperty<string>() { Value = CurrentGun.Name.Value },
                BulletCountInGun = new BindableProperty<int>() { Value = CurrentGun.BulletCountInGun.Value },
                BulletCountOutGun = new BindableProperty<int>() { Value = CurrentGun.BulletCountOutGun.Value },
                State = new BindableProperty<GunState>() { Value = GunState.IDLE }
            };
            mGunInfos.Enqueue(currentGunInfo);

            CurrentGun.Name.Value = name;
            CurrentGun.BulletCountInGun.Value = bulletInGun;
            CurrentGun.BulletCountOutGun.Value = bulletOutGun;
            CurrentGun.State.Value = GunState.IDLE;
        }

        protected override void OnInit()
        {

        }
    }
}
