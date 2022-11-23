using QFramework;

namespace ShootingDemo
{
    public class PickUpGunCommand : AbstractCommand
    {
        private readonly string mName;
        private readonly int mBulletCountInGun;
        private readonly int mBulletCountOutGun;

        public PickUpGunCommand(string name, int bulletCountInGun, int bulletCountOutGun)
        {
            mName = name;
            mBulletCountInGun = bulletCountInGun;
            mBulletCountOutGun = bulletCountOutGun;
        }

        protected override void OnExecute()
        {
            this.GetSystem<IGunSystem>()
                .PickUpGun(mName, mBulletCountInGun, mBulletCountOutGun);
        }
    }
}