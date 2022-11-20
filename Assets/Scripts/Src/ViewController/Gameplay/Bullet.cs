using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class Bullet : MonoBehaviour, IController
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
            mRig.velocity = Vector2.right * 10f * isRight;
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

        public IArchitecture GetArchitecture()
        {
            return Shooting2D.Interface;
        }
    }
}
