using System.IO;
using System.Xml;
using UnityEngine;
using System.Linq;

namespace ShootingDemo
{
    public class LevelPlayer : MonoBehaviour
    {
        private static string mLevelFolder;
        public enum Phase
        {
            SELECTION,
            PLAYING
        }

        private Phase mCurrentPhase = Phase.SELECTION;

        private void Start()
        {
            mLevelFolder = Application.persistentDataPath + "/LevelFiles";
        }

        private void OnGUI()
        {
            if (mCurrentPhase == Phase.SELECTION)
            {
                string[] levelFiles = Directory.GetFiles(mLevelFolder);

                int y = 10;
                foreach (var levelFile in levelFiles.Where(f => f.EndsWith(".xml")))
                {
                    var fileName = Path.GetFileName(levelFile);

                    if (GUI.Button(new Rect(10, y, 100, 40), fileName))
                    {
                        string xmlText = File.ReadAllText(levelFile);
                        ParseAndRun(xmlText);
                        mCurrentPhase = Phase.PLAYING;
                    }

                    y += 40;
                }
            }
        }

        private void ParseAndRun(string xml)
        {
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
