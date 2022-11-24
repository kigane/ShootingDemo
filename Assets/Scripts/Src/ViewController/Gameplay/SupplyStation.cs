using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class SupplyStation : MonoBehaviour, IController
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.SendCommand<FullBulletCommand>();
            }
        }

        public IArchitecture GetArchitecture()
        {
            return Shooting2D.Interface;
        }
    }
}
