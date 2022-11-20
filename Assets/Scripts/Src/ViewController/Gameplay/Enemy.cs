using UnityEngine;

namespace ShootingDemo
{
    public class Enemy : MonoBehaviour
    {
        private Trigger2DCheck mGroundChecker;
        private Trigger2DCheck mFallChecker;
        private Trigger2DCheck mWallChecker;
        private Rigidbody2D mRig;
        public float mSpeed = 8f;

        private void Awake()
        {
            mRig = GetComponent<Rigidbody2D>();
            mGroundChecker = transform.Find("GroundCheck").gameObject.GetComponent<Trigger2DCheck>();
            mFallChecker = transform.Find("FallCheck").gameObject.GetComponent<Trigger2DCheck>();
            mWallChecker = transform.Find("WallCheck").gameObject.GetComponent<Trigger2DCheck>();
        }

        private void FixedUpdate()
        {
            if (!mWallChecker.Triggered && mFallChecker.Triggered && mGroundChecker.Triggered)
            {
                mRig.velocity = new Vector2(transform.localScale.x * mSpeed, mRig.velocity.y);
            }
            else
            {
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
        }
    }
}
