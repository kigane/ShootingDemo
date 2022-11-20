using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootingDemo
{
    public class BtnFunctions : MonoBehaviour
    {
        public void GameStart()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void GameRestart()
        {
            SceneManager.LoadScene("GameStart");
        }
    }
}
