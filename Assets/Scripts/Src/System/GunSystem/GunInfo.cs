using QFramework;
using System;

namespace ShootingDemo
{
    public enum GunState
    {
        IDLE,
        SHOOTING,
        RELOAD,
        EMPTY,
        COOLDOWN
    }

    public class GunInfo
    {
        public BindableProperty<int> BulletCountInGun;
        public BindableProperty<int> BulletCountOutGun;
        public BindableProperty<string> Name;
        public BindableProperty<GunState> State;
    }
}
