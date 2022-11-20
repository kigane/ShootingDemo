using QFramework;
using UnityEngine;

namespace ShootingDemo
{
    public class KillEnemyCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetSystem<IStatSystem>().KillCount.Value++;

            var randNum = Random.Range(0, 100);
            if (randNum < 80)
            {
                this.GetSystem<IGunSystem>().CurrentGun.BulletCount.Value += Random.Range(1, 4);
            }
        }
    }
}
