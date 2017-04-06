/* Class Name: DragItem
 * Describe: 
 * Author: Ghostyii
 * Create Time: 2016/4/26
 */
 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace InventorySystem
{
    public class DragItem : MonoBehaviour
    {
        public Image itemImage;
        public Text numberText;

        private Item target;
        public Item Target
        { set { target = value; } }
        static public DragItem Instance;
        private Item prevTarget = null;
        public Item PrevTarget
        { get { return prevTarget; } }

        public bool IsNull
        { get { return itemImage == null; } }
        public bool Enabled
        { get { return itemImage.enabled; } }
        public bool ReadySwap
        { get { return prevTarget != null && prevTarget.GridIndex > 0; } }
        public bool IsHide
        {
            get
            { 
                return numberText == null ? itemImage.enabled && numberText.enabled : itemImage.enabled;
            }
        }

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        void Start()
        { Hide(); }

        public void SetPosition(Vector2 position)
        { itemImage.rectTransform.anchoredPosition = position; }

        public void Display(Sprite sprite, int num)
        {
            if (IsNull)
            {
                Debug.Log("DragItem's Image is null");
                return;
            }

            RightClickMenuManager.Instance.Hide();
            itemImage.sprite = sprite;
            numberText.text = num.ToString();

            if (numberText != null)
                itemImage.enabled = numberText.enabled = true;
            else
                itemImage.enabled = true;
        }
        public void Display(MouseDrag.DisplayStruct display)
        {
            if (IsNull)
            {
                Debug.Log("DragItem's Image is null");
                return;
            }

            RightClickMenuManager.Instance.Hide();
            itemImage.sprite = display.itemImage.sprite;
            if (display.numText != null && numberText != null)
                numberText.text = display.numText.text;

            if (numberText != null)
            {
                if (display.numText != null)
                {
                    itemImage.enabled = true;
                    int result = 1;
                    int.TryParse(display.numText.text, out result);
                    numberText.enabled = result != 1;
                }
                else
                    itemImage.enabled = true;
            }
            else
                itemImage.enabled = true;
        }

        public void Hide()
        {
            if (IsNull)
            {
                Debug.Log("DragItem's Image is null");
                return;
            }

            if (numberText != null)
                itemImage.enabled = numberText.enabled = false;
            else
                itemImage.enabled = false;
        }

        public void Clear()
        { target = null; prevTarget = null; }
        
        #region Interesting things
        public void OnTriggerEnter2D(Collider2D collision)
        {            
            prevTarget = collision.GetComponent<Item>();
            if (target == null)
                target = prevTarget;
        }
        public void OnTriggerStay2D(Collider2D collision)
        {            
            prevTarget = Enabled ? (collision.GetComponent<Item>()) : null;
        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            prevTarget = null;
        }
        #endregion

        /*
         * private bool IsTarget(Item item)
         * {
         *    if (InventoryManager.Instance.SeletedItem)
         *       return item.GridIndex == InventoryManager.Instance.SeletedItem.GridIndex;
         *    else
         *    {
         *       print("SeletedItem is null!");
         *       return false;
         *    }
         *  }
         */

        //Debug GUI
        //void OnGUI()
        //{
        //    GUI.Label(new Rect(Vector2.zero, new Vector2(200, 200)), string.Format("DragItem's girdIndex: {0}\nprevTarget's girdIndex: {1}", target == null ? "UnLoad" : target.GridIndex.ToString(), prevTarget == null ? "UnLoad" : (prevTarget.IsNull ? "UnLoad" : prevTarget.GridIndex.ToString())));
        //    GUI.Label(new Rect(new Vector2(200, 0), new Vector2(200, 200)), string.Format("DragItem's DataID: {0}\nprevTarget's DataID: {1}", target == null ? "UnLoad" : target.DataID.ToString(), prevTarget == null ? "UnLoad" : (prevTarget.IsNull ? "UnLoad" : prevTarget.DataID.ToString())));

        //    //if (GUI.Button(new Rect(200, 200, 200, 200), "Save"))
        //    //    ItemGrid.Instance.Save();
        //}
        //public string GetTargetName()
        //{
        //    if (target == null)
        //        return "null";

        //    return target.name == "" ? "Unload" : target.name;
        //}

    }
}