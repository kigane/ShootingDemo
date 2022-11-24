using QFramework;

namespace ShootingDemo
{
    public class ShiftGunCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetSystem<IGunSystem>().ShiftGun();
        }
    }
}
