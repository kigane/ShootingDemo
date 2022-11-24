using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace ShootingDemo
{
    public class LevelEditor : MonoBehaviour
    {
        public enum DrawMode
        {
            Draw,
            Erase
        }

        public SpriteRenderer EmptyHighlight;
        public Text mText;
        private bool mCanDraw;
        private DrawMode mMode;

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

            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            // 用Update(0.007)更新，会出现点击一次仍然绘制很多次的情况。使用FixedUpdate(0.02)则正常。
            // 原因可能是，Physics2D是物理系统，物理系统是在FixedUpdate中更新的。第一次点击绘制物体后，物理系统没有及时更新，导致多次绘制。
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero, Mathf.Infinity);

            if (hit.collider)
            {
                EmptyHighlight.color = new Color(1.0f, 0, 0, 0.5f);
                mCanDraw = false;
            }
            else
            {
                EmptyHighlight.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                mCanDraw = true;
            }

            if (Mouse.current.leftButton.isPressed && mCanDraw)
            {
                var groundPrefab = Resources.Load<GameObject>("Prefabs/Ground");
                var groundGO = Instantiate(groundPrefab, transform);
                groundGO.transform.position = mouseWorldPos;
                groundGO.name = "Ground";
            }
        }
    }
}
