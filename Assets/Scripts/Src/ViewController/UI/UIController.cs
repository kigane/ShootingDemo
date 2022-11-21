using System;
using UnityEngine;
using QFramework;

namespace ShootingDemo
{
    public class UIController : MonoBehaviour, IController
    {
        private IStatSystem mStatSystem;
        private IGunSystem mGunSystem;
        private IPlayerModel mPlayerModel;

        private void Awake()
        {
            mStatSystem = this.GetSystem<IStatSystem>();
            mGunSystem = this.GetSystem<IGunSystem>();
            mPlayerModel = this.GetModel<IPlayerModel>();
        }

        private readonly Lazy<GUIStyle> mLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            fontSize = 40
        });

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 100), $"生命: {mPlayerModel.HP.Value}/3", mLabelStyle.Value);
            GUI.Label(new Rect(10, 60, 300, 100), $"枪内子弹数: {mGunSystem.CurrentGun.BulletCountInGun.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 110, 300, 100), $"枪外子弹数: {mGunSystem.CurrentGun.BulletCountOutGun.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 160, 300, 100), $"枪械名称: {mGunSystem.CurrentGun.Name.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 210, 300, 100), $"枪械状态: {mGunSystem.CurrentGun.State.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(Screen.width - 10 - 300, 10, 300, 100), $"击杀数: {mStatSystem.KillCount.Value}", mLabelStyle.Value);
        }

        private void OnDestroy()
        {
            mStatSystem = null;
            mGunSystem = null;
            mPlayerModel = null;
        }

        public IArchitecture GetArchitecture()
        {
            return Shooting2D.Interface;
        }
    }
}
