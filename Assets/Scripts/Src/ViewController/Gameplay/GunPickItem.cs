using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class GunPickItem : Shooting2DController
    {
        public string Name;
        public int BulletCountInGun;
        public int BulletCountOutGun;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.SendCommand(new PickUpGunCommand(Name, BulletCountInGun, BulletCountOutGun));
                Destroy(gameObject);
            }
        }
    }
}
