using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class SupplyStation : Shooting2DController
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.SendCommand<FullBulletCommand>();
            }
        }
    }
}
