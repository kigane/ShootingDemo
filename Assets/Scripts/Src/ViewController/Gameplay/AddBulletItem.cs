using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class AddBulletItem : Shooting2DController
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.SendCommand<AddBulletCommand>();
                Destroy(gameObject);
            }
        }
    }
}
