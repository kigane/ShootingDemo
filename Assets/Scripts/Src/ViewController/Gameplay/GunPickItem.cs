using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class GunPickItem : MonoBehaviour, IController
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

        public IArchitecture GetArchitecture()
        {
            return Shooting2D.Interface;
        }
    }
}
