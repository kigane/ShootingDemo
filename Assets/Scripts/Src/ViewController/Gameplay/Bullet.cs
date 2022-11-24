using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class Bullet : Shooting2DController
    {
        private Rigidbody2D mRig;

        private void Awake()
        {
            mRig = GetComponent<Rigidbody2D>();
            // 一定时间后销毁
            Destroy(gameObject, 3f);
        }

        private void Start()
        {
            int isRight = (int)Mathf.Sign(transform.localScale.x);
            mRig.velocity = 10f * isRight * Vector2.right;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                this.SendCommand<KillEnemyCommand>();
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
