/* Class Name: Item
 * Describe: Item's attribute
 * Author: Ghostyii
 * Create Time: 2016/4/13
 */ 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace InventorySystem
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Image))]
    public class Item : ItemBase, IDropHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
    {
        public int sum = 0;

        public Sprite sprite;
        public Text sumText;
        public ToolTip tooltip;

        private Image image;
        [SerializeField]
        private int gridIndex = 0;
        //return grid index
        public int GridIndex
        {
            get { return gridIndex; }
            set { gridIndex = value; }
        }
        //Return gird index in range[0,ItemGrid.maxShowSum]
        public int FixedGridIndex
        {
            get { return gridIndex >= ItemGrid.Instance.maxShowSum ? gridIndex % ItemGrid.Instance.maxShowSum : gridIndex; }
        }
        //Use this to save
        [SerializeField]
        private int dataID = 0;
        public int DataID
        {
            get { return IsNull ? -1 : dataID; }
            set { dataID = value; }
        }
        //Get this item is null or not
        public bool IsNull
        { get { return (sprite && sprite.name == "alpha0" && gridIndex < 0) ; } }
        public bool IsFull
        { get { return repeatable ? true : sum >= maxSum; } }

        private class ClickEvent : UnityEvent { }
        private ClickEvent OnClick = new ClickEvent();

        void Awake()
        {
            image = GetComponent<Image>();            
        }
        //Use [protected override void OnStart()] instand of [void Start()]
        protected override void OnStart()
        { base.OnStart(); }

        //Swap two item
        public void Swap(int gridIndex)
        {
            if (this.gridIndex == gridIndex)
                return;

            gridIndex = gridIndex >= ItemGrid.Instance.maxShowSum ? gridIndex % ItemGrid.Instance.maxShowSum : gridIndex;

            ItemData temp = ToItemData();
            SetItem(ItemGrid.Instance.FindItemByGridIndex(gridIndex).ToItemData());
            ItemGrid.Instance.FindItemByGridIndex(gridIndex).SetItem(temp);
        }
        public void Swap(Item item)
        {
            if (item.gridIndex == gridIndex)
                return;
            
            Swap(item.gridIndex);
            ItemGrid.Instance.SwapItemDataByDataID(dataID, item.dataID);
            ItemGrid.Instance.Refresh(ItemGrid.Instance.CurrntPage);
        }

        //Set sprite to "Null"(alpha0)
        public void SetNull()
        {
            base.Clear();
            gridIndex = -1;
            sprite = Resources.Load<Sprite>("ItemSprites/alpha0");
            image.sprite = sprite;
        }
        //Change Text's text (sum = num)
        public void ChangeSum(int num)
        {
            sum = num;
            sum = Mathf.Clamp(sum, 1, maxSum);
            sumText.text = sum.ToString();
        }
        //Add sum (sum += num)
        public void AddSum(int num)
        {
            sum += num;
            sum = Mathf.Clamp(sum, 1, maxSum);
            sumText.text = sum.ToString();
        }
        public void AddSum(int num, out int remainder)
        {
            if (sum + num <= maxSum)
            {
                sum += num;
                remainder = 0;
                return;
            }

            remainder = num - (maxSum - sum);
            sum = maxSum;
            sumText.text = sum.ToString();

        }

        //Set this item's message
        public override void SetItem(ItemData itemData)
        {
            base.SetItem(itemData);

            dataID = itemData.dataID;

            //sum = itemData.num;
            sprite = Resources.Load<Sprite>("ItemSprites/" + itemData.picName);
            image.sprite = sprite;

            if (sumText)
            {
                sumText.text = itemData.repeatable ? itemData.num.ToString() : "";
                sumText.enabled = itemData.repeatable;
                AddSum(itemData.num);
            }

            if (tooltip)
                tooltip.displayData = itemData.tooltip;

        }
        //Clear message
        public override void Clear()
        {
            base.Clear();
            sum = 0;
            sprite = null;
            image.sprite = null;

            if (sumText)
                sumText.enabled = false;

            if (tooltip)
                tooltip.displayData.Clear();
        }

        //AddListener to agent
        //protected bool AddListenter(ClickMenu agentType, UnityAction call)
        //{
        //    var agent = RightClickMenuManager.Instance.GetAgent(agentType);
        //    if (agent == null)
        //        return false;

        //    agent.SetListener(new UnityAction[] { call });
        //    return true;
        //}
        
        //Parse to ItemData

        public override ItemData ToItemData()
        {
            ItemData result = base.ToItemData();
            result.dataID = dataID;
            result.num = sum;
            result.tooltip = tooltip.displayData;
            result.picName = sprite == null ? "alpha0" : sprite.name;

            return result;
        }

        #region Update SeletedItem
        //Implement interface
        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsNull)
                return;
            RightClickMenuManager.Instance.Hide();
            InventoryManager.Instance.SeletedItem = this;
            DragItem.Instance.Target = this;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (IsNull)
                return;

            InventoryManager.Instance.SeletedItem = null;

            var tmp = DragItem.Instance.PrevTarget;
            if (DragItem.Instance.PrevTarget == this)
                return;

            if(tmp != null)
            {
                if (tmp.id == id && tmp.sum < maxSum)
                {
                    if (tmp.sum + sum <= maxSum)
                    {
                        tmp.AddSum(sum);
                        ItemGrid.Instance.AllItemData[tmp.dataID] = tmp.ToItemData();
                        ItemGrid.Instance.Refresh(ItemGrid.Instance.CurrntPage);
                        if (!ItemGrid.Instance.RemoveItem(ToItemData()))
                            Debug.Log("Delete failed");
                    }
                    else
                    {
                        sum -= (maxSum - tmp.sum);
                        ItemGrid.Instance.AllItemData[dataID] = ToItemData();
                        tmp.sum = maxSum;
                        ItemGrid.Instance.AllItemData[tmp.dataID] = tmp.ToItemData();
                        print(dataID + " tmp: " + tmp.dataID);
                        ItemGrid.Instance.Refresh(ItemGrid.Instance.CurrntPage);
                    }

                    return;
                }
            }

            try
            { Swap(DragItem.Instance.PrevTarget); }
            catch
            { /*nothing*/ }

            //DragItem.Instance.Target = null;
            DragItem.Instance.Clear();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (IsNull)
                return;

            DragItem.Instance.Target = eventData.pointerDrag.GetComponent<Item>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsNull)
                return;

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Vector2 pos = (transform as RectTransform).anchoredPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(tooltip.canvas, Input.mousePosition, tooltip.uiCamera, out pos);

                pos = InventoryManager.Instance.GetPositionByRoughEdge(pos, RightClickMenuManager.Instance.root.transform as RectTransform, RoughEdge.RightBottom);

                //if (itemType == ItemType.Item)
                //    RightClickMenuManager.Instance.AddListenter(ClickMenu.Use, Use);
                //else if (itemType == ItemType.Equipment)
                //    RightClickMenuManager.Instance.AddListenter(ClickMenu.Equip, Equipment);

                //RightClickMenuManager.Instance.AddListenter(ClickMenu.Sell, () => Sell());
                //RightClickMenuManager.Instance.AddListenter(ClickMenu.Delete, Delete);

                RightClickAction ra = new RightClickAction();
                if (itemType == ItemType.Item)
                {
                    ra.menu = ClickMenu.Use;
                    ra.actions = new UnityAction[] { Use };
                }
                else if (itemType == ItemType.Equipment)
                {
                    ra.menu = ClickMenu.Equip;
                    ra.actions = new UnityAction[] { Equipment };
                }

                RightClickAction rb = new RightClickAction();
                rb.menu = ClickMenu.Delete;
                rb.actions = new UnityAction[] { Delete };

                RightClickAction rc = new RightClickAction();
                rc.menu = ClickMenu.Sell;
                rc.actions = new UnityAction[] { Sell };

                RightClickMenuManager.Instance.SetListenter(ra, rb, rc);

                if (itemType == ItemType.Equipment)
                    RightClickMenuManager.Instance.RemoveListenter(ClickMenu.Sell, Sell);

                RightClickMenuManager.Instance.Show(pos);
            }
        }

        #endregion

        public void Use()
        {
            print( name +  "is this work?");
        }

        public void Delete()
        {
            if(IsNull)
                return;

            ItemGrid.Instance.RemoveItem(dataID);
        }

        public void Equipment()
        {
            if (itemType == ItemType.Equipment)
                print("装备了" + name);
        }

        public void Sell()
        {
            print("出售了" + name + "道具");
            ItemGrid.Instance.RemoveItem(dataID);
        }        
    }
}
