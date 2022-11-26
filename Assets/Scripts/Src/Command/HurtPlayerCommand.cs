using QFramework;
using UnityEngine.SceneManagement;

namespace ShootingDemo
{
    public class HurtPlayerCommand : AbstractCommand
    {
        private int mHurt;

        public HurtPlayerCommand(int hurt)
        {
            mHurt = hurt;
        }

        protected override void OnExecute()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.HP.Value -= mHurt;

            if (playerModel.HP.Value <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}