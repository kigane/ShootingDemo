using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootingDemo
{
    public class NextLevel : MonoBehaviour
    {
        public string levelName;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(levelName);
            }
        }
    }
}
