using UnityEngine;

namespace ShootingDemo
{
    public class CameraController : MonoBehaviour
    {
        private Transform mPlayerTransform;
        private Vector3 mTargetPos;

        private void FixedUpdate()
        {
            if (!mPlayerTransform)
            {
                var playerGO = GameObject.FindWithTag("Player");
                if (playerGO)
                {
                    mPlayerTransform = playerGO.transform;
                }
                else
                {
                    return;
                }
            }

            var isRight = Mathf.Sign(mPlayerTransform.localScale.x);

            var camPos = transform.position;

            mTargetPos.x = mPlayerTransform.position.x + 3f * isRight;
            mTargetPos.y = mPlayerTransform.position.y + 2f;
            mTargetPos.z = -10;

            transform.position = Vector3.Lerp(camPos, mTargetPos, 10f * Time.deltaTime);
        }
    }
}
