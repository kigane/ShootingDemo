using System.Collections.Generic;
using QFramework;

namespace ShootingDemo
{
    public interface IGunConfigModel : IModel
    {
        GunConfigItem GetGunConfigByName(string name);
    }

    public class GunConfigItem
    {
        public string Name { get; set; }
        public int BulletMaxCount { get; set; }
        public float Attack { get; set; }
        public float Frequency { get; set; }
        public float ShootDistance { get; set; }
        public bool NeedBullet { get; set; }
        public float ReloadSeconds { get; set; }
        public string Description { get; set; }

        public GunConfigItem(
            string name,
            int bulletMaxCount,
            float attack,
            float frequency,
            float shootDistance,
            bool needBullet,
            float reloadSeconds,
            string description
        )
        {
            Name = name;
            BulletMaxCount = bulletMaxCount;
            Attack = attack;
            Frequency = frequency;
            ShootDistance = shootDistance;
            NeedBullet = needBullet;
            ReloadSeconds = reloadSeconds;
            Description = description;
        }
    }

    public class GunConfigModel : AbstractModel, IGunConfigModel
    {
        private Dictionary<string, GunConfigItem> mItems = new(){
            {"Pistol", new GunConfigItem("Pistol", 7, 1, 2, 3, false, 1, "初始武器")},
            {"TommyGun", new GunConfigItem("TommyGun", 30, 1, 5, 1f, true, 2, "冲锋枪")},
            {"Rifle", new GunConfigItem("Rifle", 40, 1, 2, 8, true, 2, "步枪")},
            {"SniperRifle", new GunConfigItem("SniperRifle", 5, 1, 1, 12f, true, 3, "狙击枪")},
            {"Bazooka", new GunConfigItem("Bazooka", 1, 8, 1, 7, true, 3, "火箭筒")},
        };

        public GunConfigItem GetGunConfigByName(string name)
        {
            return mItems[name];
        }

        protected override void OnInit()
        {

        }
    }
}
