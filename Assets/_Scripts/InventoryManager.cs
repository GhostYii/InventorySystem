/* Class Name: InventoryManager
 * Describe: Single Class.Manage all inventory's informations
 * Author: Ghostyii
 * Create Time: 2016/4/14
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System.Collections.Generic;

namespace InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private Item selectedItem = null;
        public Item SeletedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        public const int MAXSHOWSUM = 35;

        static public InventoryManager Instance;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0) && selectedItem == null)
                RightClickMenuManager.Instance.Hide();

        }


        public Vector2 GetPositionByRoughEdge(Vector2 rawPos, RectTransform rectTran, RoughEdge edge)
        {
            switch (edge)
            {
                case RoughEdge.LeftTop:
                    rawPos.x -= (rectTran.sizeDelta.x / 2);
                    rawPos.y += (rectTran.sizeDelta.y / 2);
                    return rawPos;
                case RoughEdge.RightTop:
                    rawPos.x += (rectTran.sizeDelta.x / 2);
                    rawPos.y += (rectTran.sizeDelta.y / 2);
                    return rawPos;
                case RoughEdge.LeftBottom:
                    rawPos.x -= (rectTran.sizeDelta.x / 2);
                    rawPos.y -= (rectTran.sizeDelta.y / 2);
                    return rawPos;
                case RoughEdge.RightBottom:
                    rawPos.x += (rectTran.sizeDelta.x / 2);
                    rawPos.y -= (rectTran.sizeDelta.y / 2);
                    return rawPos;
                default:
                    return GetPositionByRoughEdge(rawPos, rectTran, RoughEdge.RightBottom);
            }
        }
        public ItemType GetItemTypeByItemID(int id)
        { return (ItemType)(id / 1000 + 1); }
    }
}


