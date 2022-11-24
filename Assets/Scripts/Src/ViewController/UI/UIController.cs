using System;
using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class UIController : Shooting2DController
    {
        private IStatSystem mStatSystem;
        private IGunSystem mGunSystem;
        private IPlayerModel mPlayerModel;
        private int mMaxBulletCount;

        private void Awake()
        {
            mStatSystem = this.GetSystem<IStatSystem>();
            mGunSystem = this.GetSystem<IGunSystem>();
            mPlayerModel = this.GetModel<IPlayerModel>();
            mMaxBulletCount = this.SendQuery(new MaxBulletCountQuery(mGunSystem.CurrentGun.Name.Value ));

            this.RegisterEvent<OnCurrentGunChange>(e =>
            {
                mMaxBulletCount = this.SendQuery(new MaxBulletCountQuery(mGunSystem.CurrentGun.Name.Value));
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private readonly Lazy<GUIStyle> mLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            fontSize = 40
        });

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 400, 100), $"生命: {mPlayerModel.HP.Value}/3", mLabelStyle.Value);
            GUI.Label(new Rect(10, 60, 400, 100), $"枪内子弹数: {mGunSystem.CurrentGun.BulletCountInGun.Value}/{mMaxBulletCount}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 110, 400, 100), $"枪外子弹数: {mGunSystem.CurrentGun.BulletCountOutGun.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 160, 400, 100), $"枪械名称: {mGunSystem.CurrentGun.Name.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 210, 400, 100), $"枪械状态: {mGunSystem.CurrentGun.State.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(Screen.width - 10 - 300, 10, 300, 100), $"击杀数: {mStatSystem.KillCount.Value}", mLabelStyle.Value);
        }

        private void OnDestroy()
        {
            mStatSystem = null;
            mGunSystem = null;
            mPlayerModel = null;
        }
    }
}
