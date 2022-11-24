using QFramework;

namespace ShootingDemo
{
    /// <summary>
    /// 将所有枪的当前弹夹填满
    /// </summary>
    public class FullBulletCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gunSystem = this.GetSystem<IGunSystem>();
            var gunConfigModel = this.GetModel<IGunConfigModel>();

            var currentGun = gunSystem.CurrentGun;
            FullBullet(gunConfigModel, currentGun);

            foreach (var gunInfo in gunSystem.GunInfos)
            {
                FullBullet(gunConfigModel, gunInfo);
            }
        }

        private static void FullBullet(IGunConfigModel gunConfigModel, GunInfo gunInfo)
        {
            var gunConfigItem = gunConfigModel.GetGunConfigByName(gunInfo.Name.Value);
            gunInfo.BulletCountInGun.Value = gunConfigItem.BulletMaxCount;
        }

    }
}
