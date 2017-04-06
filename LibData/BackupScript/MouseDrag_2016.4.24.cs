/* Class Name: MouseDrag
 * Describe: Allow Ui elements drag by mouse
 * Author: Ghostyii
 * Create Time: 2016/4/13
 */
 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class MouseDrag : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    //the togger of drag
    public bool allowDrag = true;
    //set the mouse control mode [LeftButton/RightButton/MiddleButton]
    public PointerEventData.InputButton mouseMode = PointerEventData.InputButton.Left;
    //use this to transform position,use the worldCamera
    public Canvas rootCanvas;
    //this Item's display struct
    public DisplayStruct display;
    //genera display struct
    public DisplayStruct tempDisplay;

    private bool enter = false;

    private Vector3 rawPos = Vector3.zero;
    private Vector2 curPos = Vector3.zero;

    private RectTransform targetRectTrans;

    void Start()
    {
        targetRectTrans = GetComponent<RectTransform>();
        rawPos = GetComponent<RectTransform>().anchoredPosition3D;

        if (display.IsNullOrEmpty)
            Debug.Log("display object don't initialization!");
    }

    //check input has error or not
    private bool IsRightButton(PointerEventData eventData)
    {
        return eventData.button == mouseMode;
    }

    //the interface - IPointerUpHandler
    //when the mouse button up,all thing will reset
    public void OnPointerUp(PointerEventData eventData)
    {
        enter = false;
        if (!allowDrag || !IsRightButton(eventData))
            return;

        if (tempDisplay.IsNullOrEmpty)
        {
            Debug.Log("temp display object isn't initialization!");
            return;
        }

        tempDisplay.Enabled = false;

        if (display.IsNullOrEmpty)
        {
            Debug.Log("display object don't initialization!");
            return;
        }
        
        display.Enabled = !tempDisplay.Enabled;
    }

    //the interface - IDragHandler
    //on drag the tempDisplay will set right display struct and follow the mouse point
    public void OnDrag(PointerEventData eventData)
    {        
        if (!allowDrag || !IsRightButton(eventData))
            return;

        if (tempDisplay.IsNullOrEmpty)
        {
            Debug.Log("temp display object isn't initialization!");
            return;
        }

        //Coordinate transformation
        tempDisplay.Enabled = RectTransformUtility.ScreenPointToLocalPointInRectangle(tempDisplay.image.rectTransform.parent as RectTransform, Input.mousePosition, rootCanvas == null ? null : rootCanvas.worldCamera, out curPos);

        if (enter)
        {
            if (display.numText)
            {
                tempDisplay.Set(display.image.sprite, display.numText.text);
                tempDisplay.numText.enabled = tempDisplay.image.sprite != null;
            }
            else
            {
                tempDisplay.image.sprite = display.image.sprite;
                tempDisplay.numText.text = "";
            }

            tempDisplay.image.rectTransform.anchoredPosition = curPos;

            if (display.IsNullOrEmpty)
            {
                Debug.Log("display object don't initialization!");
                return;
            }

            display.Enabled = !tempDisplay.Enabled;
        }
    }

    //the interface - IPointerDownHandler
    //when mouse button down, the tempDisplay will set right display struct and display
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!allowDrag || !IsRightButton(eventData))
            return;

        if (tempDisplay.IsNullOrEmpty)
        {
            Debug.Log("temp display object isn't initialization!");
            return;
        }

        //Coordinate transformation - targetRectTransform
        Vector2 localPos = Vector2.zero;
        enter = RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTrans, eventData.pressPosition, rootCanvas == null ? null : rootCanvas.worldCamera, out localPos);
        //Coordinate transformation - tempDisplay's RectTransform
        tempDisplay.Enabled = RectTransformUtility.ScreenPointToLocalPointInRectangle(tempDisplay.image.rectTransform.parent as RectTransform, Input.mousePosition, rootCanvas == null ? null : rootCanvas.worldCamera, out curPos);

        if (enter)
        {
            if (display.numText)
            {
                if (display.numText.text == "1")
                    display.numText.text = "";

                tempDisplay.Set(display.image.sprite, display.numText.text); 
                tempDisplay.numText.enabled = tempDisplay.image.sprite != null;
            }
            else
            {
                tempDisplay.image.sprite = display.image.sprite;
                tempDisplay.numText.text = "";
            }

            tempDisplay.image.rectTransform.anchoredPosition = curPos;

            if (display.IsNullOrEmpty)
            {
                Debug.Log("display object don't initialization!");
                return;
            }

            display.Enabled = !tempDisplay.Enabled;
        }
    }

    //Only include an image and a text
    //The text's parent should be that image
    [System.Serializable]
    public struct DisplayStruct
    {
        public Image image;
        public Text numText;

        public bool IsNullOrEmpty
        { get { return image == null; } }

        public bool Enabled
        {
            get { return image.enabled && numText.enabled; }
            set
            {
                if (numText)
                    image.enabled = numText.enabled = value;
                else
                    image.enabled = value;
            }
        }

        public void Set(Sprite sprite, string text)
        {
            image.sprite = sprite;
            if (numText)
                numText.text = text;
        }
    }

}

