using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class AttackPlayer : Shooting2DController
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                this.SendCommand<HurtPlayerCommand>();
            }
        }
    }
}
