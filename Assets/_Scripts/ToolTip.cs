/* Class Name: ToolTip
 * Describe: Set and show the tooltip data of the inventory system
 * Author: Ghostyii
 * Create Time: 2016/4/14
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

namespace InventorySystem
{
    [RequireComponent(typeof(RectTransform))]
    public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public RectTransform canvas;
        public Camera uiCamera;
        //public Vector2 offset = new Vector2();
        public RoughEdge showPosition = RoughEdge.RightBottom;
        public TooltipData displayData;        

        private bool enter = false;
        private bool hided = true;
        //private RectTransform target;
        private Vector2 localPos = Vector2.zero;

        //void Start()
        //{
        //    target = GetComponent<RectTransform>();            
        //}

        void Update()
        {
            if (enter && !displayData.IsNullOrEmpty() && InventoryManager.Instance.SeletedItem == null)
            {
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(target, Input.mousePosition, uiCamera, out localPos);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, uiCamera, out localPos);
                ToolTipSetting.Instance.SetToolTip(displayData);
                localPos = InventoryManager.Instance.GetPositionByRoughEdge(localPos, ToolTipSetting.Instance.toolTip.transform as RectTransform, showPosition);
                ToolTipSetting.Instance.Show(localPos);
                hided = false;
            }
            else
            {
                if (!hided)
                {
                    ToolTipSetting.Instance.Hide();
                    hided = true;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            enter = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            enter = false;
        }
    }
}

