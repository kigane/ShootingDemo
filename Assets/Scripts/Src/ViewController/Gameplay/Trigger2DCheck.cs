using UnityEngine;

namespace ShootingDemo
{
    public class Trigger2DCheck : MonoBehaviour
    {
        public LayerMask mTargetLayers;
        private int EnterCount;

        public bool Triggered {
            get {
                return EnterCount > 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (IsInLayerMask(other.gameObject, mTargetLayers))
            {
                EnterCount++;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (IsInLayerMask(other.gameObject, mTargetLayers))
            {
                EnterCount--;
            }
        }

        private bool IsInLayerMask(GameObject go, LayerMask mask) {
            var goMask = 1 << go.layer;
            return (mask.value & goMask) > 0;
        }
    }
}
