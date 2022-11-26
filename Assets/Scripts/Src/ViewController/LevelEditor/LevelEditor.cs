using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System;

namespace ShootingDemo
{
    public class LevelEditor : MonoBehaviour
    {
        public enum DrawMode
        {
            DRAW,
            ERASE
        }

        public enum BrushType
        {
            GROUND,
            PLAYER
        }

        struct LevelItemInfo
        {
            public string Name;
            public float X;
            public float Y;
        }

        public SpriteRenderer EmptyHighlight;
        public Text mText;
        public Button mDrawBtn;
        public Button mEraseBtn;
        public Button mSaveBtn;
        public Button mGroundBtn;
        public Button mPlayerBtn;
        private bool mCanDraw;
        private DrawMode mCurrentMode = DrawMode.DRAW;
        private BrushType mCurrentBrush = BrushType.GROUND;
        private GameObject mCurrentGOMouseOn;

        private void Start()
        {
            mDrawBtn.onClick.AddListener(() =>
            {
                mCurrentMode = DrawMode.DRAW;
                mText.text = DrawMode.DRAW.ToString();
                ShowDrawRelatedBtns(true);
            });

            mEraseBtn.onClick.AddListener(() =>
            {
                mCurrentMode = DrawMode.ERASE;
                mText.text = DrawMode.ERASE.ToString();
                ShowDrawRelatedBtns(false);
            });

            mSaveBtn.onClick.AddListener(() =>
            {
                var infos = new List<LevelItemInfo>(transform.childCount);

                // 获取LevelEditor对象的所有子对象
                foreach (Transform child in transform)
                {
                    infos.Add(new LevelItemInfo()
                    {
                        Name = child.name,
                        X = child.position.x,
                        Y = child.position.y
                    });
                    // Debug.Log($"Name: {child.name}, X: {child.position.x}, Y: {child.position.y}");
                }

                // 创建XML文档
                var document = new XmlDocument();
                var declaration = document.CreateXmlDeclaration("1.0", "UTF-8", "");
                document.AppendChild(declaration);

                var level = document.CreateElement("Level");
                document.AppendChild(level);

                foreach (LevelItemInfo info in infos)
                {
                    var item = document.CreateElement("LevelItem");
                    item.SetAttribute("name", info.Name);
                    item.SetAttribute("x", info.X.ToString());
                    item.SetAttribute("y", info.Y.ToString());
                    level.AppendChild(item);
                }

                // XML格式化
                StringBuilder stringBuilder = new();
                StringWriter stringWriter = new(stringBuilder);
                XmlTextWriter xmlWriter = new(stringWriter)
                {
                    Formatting = Formatting.Indented
                };
                document.WriteTo(xmlWriter);

                // Unity默认文件保存路径
                var levelFileFolder = Application.persistentDataPath + "/LevelFiles";
                Debug.Log(levelFileFolder);

                // 创建文件夹
                if (!Directory.Exists(levelFileFolder))
                {
                    Directory.CreateDirectory(levelFileFolder);
                }

                // 保存XML文档
                var levelFilePath = levelFileFolder + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                document.Save(levelFilePath);
            });

            mGroundBtn.onClick.AddListener(() =>
            {
                mCurrentBrush = BrushType.GROUND;
            });

            mPlayerBtn.onClick.AddListener(() =>
            {
                mCurrentBrush = BrushType.PLAYER;
            });
        }

        private void ShowDrawRelatedBtns(bool active)
        {
            mGroundBtn.gameObject.SetActive(active);
            mPlayerBtn.gameObject.SetActive(active);
        }

        private void FixedUpdate()
        {
            var mousePos2 = Mouse.current.position.ReadValue();
            Vector3 mousePos = new(mousePos2.x, mousePos2.y, 0);
            // Debug.Log($"Mouse {mousePos}"); // 不完全相等，有些微误差
            // Debug.Log($"Input {Input.mousePosition}");

            var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

            mouseWorldPos.x = Mathf.Floor(mouseWorldPos.x + 0.5f);
            mouseWorldPos.y = Mathf.Floor(mouseWorldPos.y + 0.5f);
            mouseWorldPos.z = 0;

            // 让高亮块始终显示在最上层
            var highlightPos = mouseWorldPos;
            highlightPos.z = -9;
            EmptyHighlight.transform.position = highlightPos;

            if (MouseOnUI())
            {
                EmptyHighlight.gameObject.SetActive(false);
            }
            else
            {
                EmptyHighlight.gameObject.SetActive(true);
            }

            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            // 用Update(0.007)更新，会出现点击一次仍然绘制很多次的情况。使用FixedUpdate(0.02)则正常。
            // 原因可能是，Physics2D是物理系统，物理系统是在FixedUpdate中更新的。第一次点击绘制物体后，物理系统没有及时更新，导致多次绘制。
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero, Mathf.Infinity);

            if (hit.collider)
            {
                if (mCurrentMode == DrawMode.DRAW)
                {
                    EmptyHighlight.color = new Color(1.0f, 0, 0, 0.5f);
                }
                else if (mCurrentMode == DrawMode.ERASE)
                {
                    EmptyHighlight.color = new Color(1.0f, 0.5f, 0, 0.5f);
                }
                mCanDraw = false;
                mCurrentGOMouseOn = hit.collider.gameObject;
            }
            else
            {
                if (mCurrentMode == DrawMode.DRAW)
                {
                    EmptyHighlight.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                }
                else if (mCurrentMode == DrawMode.ERASE)
                {
                    EmptyHighlight.color = new Color(0, 0, 1.0f, 0.5f);
                }
                mCanDraw = true;
                mCurrentGOMouseOn = null;
            }

            if (Mouse.current.leftButton.isPressed && !MouseOnUI())
            {
                if (mCanDraw && mCurrentMode == DrawMode.DRAW)
                {
                    if (mCurrentBrush == BrushType.GROUND)
                    {
                        var groundPrefab = Resources.Load<GameObject>("Prefabs/Ground");
                        var groundGO = Instantiate(groundPrefab, transform);
                        groundGO.transform.position = mouseWorldPos;
                        groundGO.name = "Ground";
                    }
                    else
                    {
                        var groundPrefab = Resources.Load<GameObject>("Prefabs/Ground");
                        var groundGO = Instantiate(groundPrefab, transform);
                        groundGO.transform.position = mouseWorldPos;
                        groundGO.name = "Player";
                        groundGO.GetComponent<SpriteRenderer>().color = Color.cyan;
                    }
                }
                else if (mCurrentGOMouseOn && mCurrentMode == DrawMode.ERASE)
                {
                    Destroy(mCurrentGOMouseOn);
                }
            }
        }

        private bool MouseOnUI()
        {
            return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }
    }
}
