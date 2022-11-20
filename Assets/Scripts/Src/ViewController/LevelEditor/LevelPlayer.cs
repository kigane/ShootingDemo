using System.Xml;
using UnityEngine;

namespace ShootingDemo
{
    public class LevelPlayer : MonoBehaviour
    {
        public TextAsset LevelFile;

        private void Start()
        {
            var xml = LevelFile.text;
            var document = new XmlDocument();
            document.LoadXml(xml);
            var levelNode = document.SelectSingleNode("Level");
            foreach (XmlElement levelItemNode in levelNode)
            {
                string levelItemName = levelItemNode.Attributes["name"].Value;
                int levelItemX = int.Parse(levelItemNode.Attributes["x"].Value);
                int levelItemY = int.Parse(levelItemNode.Attributes["y"].Value);

                // 加载资源
                var levelItemPrefab = Resources.Load<GameObject>("Prefabs/" + levelItemName);
                // 实例化prefab并挂载到当前GO下
                var levelItemGO = Instantiate(levelItemPrefab, transform);
                // 设置GO的位置
                levelItemGO.transform.position = new Vector3(levelItemX, levelItemY, 0);
            }

        }
    }
}
