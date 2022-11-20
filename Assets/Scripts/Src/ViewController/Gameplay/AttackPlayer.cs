using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class AttackPlayer : MonoBehaviour, IController
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                this.SendCommand<HurtPlayerCommand>();
            }
        }

        public IArchitecture GetArchitecture()
        {
            return Shooting2D.Interface;
        }
    }
}
