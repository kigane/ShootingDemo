using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class Gun : MonoBehaviour, IController
    {
        public GameObject mBullet;
        private GunInfo mGunInfo;
        private int mMaxBulletCount;

        private void Start()
        {
            mBullet = transform.Find("Bullet").gameObject;
            // 弹药量只和一把枪关联
            mGunInfo = this.GetSystem<IGunSystem>().CurrentGun;
            mMaxBulletCount = this.SendQuery(new MaxBulletCountQuery(mGunInfo.Name.Value));
        }

        public void Shoot()
        {
            if (mGunInfo.BulletCountInGun.Value > 0 && mGunInfo.State == GunState.IDLE)
            {
                var bullet = Instantiate(mBullet, mBullet.transform.position, mBullet.transform.rotation);
                // 原来Bullet是Gun的子对象，Instantiate出来的会在根节点，因此缩放需要调整。
                bullet.transform.localScale = mBullet.transform.lossyScale;
                bullet.SetActive(true);

                // 不用每次都new一个新对象
                this.SendCommand(ShootCommand.Single);
            }
        }

        public void Reload()
        {
            if (mGunInfo.State.Value == GunState.IDLE &&
                mGunInfo.BulletCountInGun.Value != mMaxBulletCount &&
                mGunInfo.BulletCountOutGun.Value > 0)
            {
                this.SendCommand<ReloadCommand>();
            }
        }

        private void OnDestroy()
        {
            mGunInfo = null;
        }

        public IArchitecture GetArchitecture()
        {
            return Shooting2D.Interface;
        }
    }
}
