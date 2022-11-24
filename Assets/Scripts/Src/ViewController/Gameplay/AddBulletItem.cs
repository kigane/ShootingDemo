using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class AddBulletItem : MonoBehaviour, IController
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.SendCommand<AddBulletCommand>();
                Destroy(gameObject);
            }
        }

        public IArchitecture GetArchitecture()
        {
            return Shooting2D.Interface;
        }
    }
}
