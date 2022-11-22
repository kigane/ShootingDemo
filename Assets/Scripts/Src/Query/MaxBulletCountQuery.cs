using QFramework;

namespace ShootingDemo
{
    public class MaxBulletCountQuery : AbstractQuery<int>
    {
        private readonly string mGunName;

        public MaxBulletCountQuery(string gunName)
        {
            mGunName = gunName;
        }

        protected override int OnDo()
        {
            var gunConfig = this.GetModel<IGunConfigModel>();
            var gunConfigItem = gunConfig.GetGunConfigByName(mGunName);
            return gunConfigItem.BulletMaxCount;
        }
    }
}
