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
            {"Pistol", new GunConfigItem("Pistol", 7, 1, 1, 0.5f, false, 3, "初始武器")},
            {"c", new GunConfigItem("Pistol", 30, 1, 1, 1f, true, 3, "冲锋枪")},
            {"b", new GunConfigItem("Pistol", 40, 1, 1, 2f, true, 3, "步枪")},
            {"j", new GunConfigItem("Pistol", 1, 1, 1, 5f, true, 3, "狙击枪")},
            {"h", new GunConfigItem("Pistol", 1, 1, 1, 3f, true, 3, "火箭筒")},
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
