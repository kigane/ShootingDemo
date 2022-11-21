using UnityEngine;

namespace ShootingDemo.Test
{
    public class TimeSystemTest : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(Time.time);

            Shooting2D.Interface.GetSystem<ITimeSystem>().AddDelayTask(3, () =>
            {
                Debug.Log(Time.time);
            });
        }
    }
}
