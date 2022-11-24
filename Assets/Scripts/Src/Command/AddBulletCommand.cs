using QFramework;

namespace ShootingDemo
{
    /// <summary>
    /// 为持有的每一把枪添加一个弹夹的弹药
    /// </summary>
    public class AddBulletCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gunSystem = this.GetSystem<IGunSystem>();
            var gunConfigModel = this.GetModel<IGunConfigModel>();
            var currentGun = gunSystem.CurrentGun;
            AddBullet(gunConfigModel, currentGun);

            foreach (var gunInfo in gunSystem.GunInfos)
            {
                AddBullet(gunConfigModel, gunInfo);
            }
        }

        private static void AddBullet(IGunConfigModel gunConfigModel, GunInfo gunInfo)
        {
            var gunConfigItem = gunConfigModel.GetGunConfigByName(gunInfo.Name.Value);
            if (!gunConfigItem.NeedBullet)
            {
                // 手枪无限子弹
            }
            else
            {
                gunInfo.BulletCountOutGun.Value += gunConfigItem.BulletMaxCount;
            }
        }
    }
}
