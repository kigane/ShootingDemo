using QFramework;

namespace ShootingDemo
{
    public class ReloadCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var currentGun = this.GetSystem<IGunSystem>().CurrentGun;
            var gunConfig = this.GetModel<IGunConfigModel>().GetGunConfigByName(currentGun.Name.Value);
            var needBulletCount = gunConfig.BulletMaxCount - currentGun.BulletCountInGun;

            currentGun.State.Value = GunState.RELOAD; // 切换状态

            this.GetSystem<ITimeSystem>().AddDelayTask(gunConfig.ReloadSeconds, () =>
            {
                currentGun.State.Value = GunState.IDLE;
                if (currentGun.BulletCountOutGun >= needBulletCount) // 弹药充足，填满
                {
                    currentGun.BulletCountInGun.Value += needBulletCount;
                    currentGun.BulletCountOutGun.Value -= needBulletCount;
                }
                else // 弹药不足，有多少填多少
                {
                    currentGun.BulletCountInGun.Value += currentGun.BulletCountOutGun.Value;
                    currentGun.BulletCountOutGun.Value = 0;
                }
            });
        }
    }
}
