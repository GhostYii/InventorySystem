/* Class Name: MouseDrag
 * Describe: Allow Ui elements drag by mouse
 * Author: Ghostyii
 * Create Time: 2016/4/13
 */
 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace InventorySystem
{
    [RequireComponent(typeof(RectTransform))]
    public class MouseDrag : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
    {
        //the switch of allow drag
        public bool allowDrag = true;
        public PointerEventData.InputButton mouseMode = PointerEventData.InputButton.Left;
        public Canvas rootCanvas;
        public DisplayStruct display;

        private Vector3 rawPos = Vector3.zero;
        private Vector2 curPos = Vector3.zero;

        private RectTransform targetRectTrans;

        void Start()
        {
            targetRectTrans = GetComponent<RectTransform>();
            rawPos = GetComponent<RectTransform>().anchoredPosition3D;
        }

        //check press button
        private bool IsRightButton(PointerEventData eventData)
        {
            return eventData.button == mouseMode;
        }

        //implement interface
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!allowDrag || !IsRightButton(eventData))
                return;

            if (DragItem.Instance == null)
                Debug.Log("DragItem's instance is null");
            else
            {
                display.Display();
                DragItem.Instance.Hide();
            }

        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!allowDrag || !IsRightButton(eventData))
                return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(DragItem.Instance.itemImage.rectTransform.parent as RectTransform, Input.mousePosition, rootCanvas == null ? null : rootCanvas.worldCamera, out curPos);

            DragItem.Instance.SetPosition(curPos);
            DragItem.Instance.Display(display);

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!allowDrag || !IsRightButton(eventData))
                return;

            if (DragItem.Instance == null)
                Debug.Log("DragItem's instance is null");
            else
            {
                display.Hide();
                
                RectTransformUtility.ScreenPointToLocalPointInRectangle(DragItem.Instance.itemImage.rectTransform.parent as RectTransform, Input.mousePosition, rootCanvas == null ? null : rootCanvas.worldCamera, out curPos);
                DragItem.Instance.SetPosition(curPos);
                DragItem.Instance.Display(display);
            }

        }

        [System.Serializable]
        public struct DisplayStruct
        {
            public Image itemImage;
            public Text numText;

            public bool IsNull
            { get { return itemImage == null; } }

            public void Hide()
            {
                if (numText != null)
                    itemImage.enabled = numText.enabled = false;
                else
                    itemImage.enabled = false;
            }

            public void Display()
            {
                if (numText != null)
                {
                    itemImage.enabled = true;
                    int result = 1;
                    int.TryParse(numText.text, out result);
                    numText.enabled = result != 1;
                }
                else
                    itemImage.enabled = true;
            }
        }
    }
}