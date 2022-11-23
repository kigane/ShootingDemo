using QFramework;
using UnityEngine;

namespace ShootingDemo
{
    public class ShootCommand : AbstractCommand
    {
        public static readonly ShootCommand Single = new();

        protected override void OnExecute()
        {
            var gunInfo = this.GetSystem<IGunSystem>().CurrentGun;
            gunInfo.BulletCountInGun.Value--;
            gunInfo.State.Value = GunState.SHOOTING;

            var gunConfig = this.GetModel<IGunConfigModel>().GetGunConfigByName(gunInfo.Name.Value);
            this.GetSystem<ITimeSystem>().AddDelayTask(1 / gunConfig.Frequency, () =>
            {
                gunInfo.State.Value = GunState.IDLE;

                // 自动填弹
                if (gunInfo.BulletCountInGun.Value == 0 &&
                    gunInfo.BulletCountOutGun.Value > 0)
                {
                    this.SendCommand<ReloadCommand>();
                }
            });
        }
    }
}
