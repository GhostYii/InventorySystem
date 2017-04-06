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
    public bool allowDrag = true;
    public Image displayImage;
    public PointerEventData.InputButton mouseMode = PointerEventData.InputButton.Left;
    public Canvas rootCanvas;
    
    private Vector3 rawPos = Vector3.zero;
    private Vector2 curPos = Vector3.zero;

    private RectTransform targetRectTrans;

    void Start()
    {
        targetRectTrans = GetComponent<RectTransform>();
        rawPos = GetComponent<RectTransform>().anchoredPosition3D;
    }

    private bool IsRightButton(PointerEventData eventData)
    {
        return eventData.button == mouseMode;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!allowDrag || !IsRightButton(eventData))
            return;

        targetRectTrans.anchoredPosition3D = rawPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!allowDrag || !IsRightButton(eventData))
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTrans.parent as RectTransform, Input.mousePosition, rootCanvas == null ? null : rootCanvas.worldCamera, out curPos);

        targetRectTrans.anchoredPosition = curPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!allowDrag || !IsRightButton(eventData))
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRectTrans, Input.mousePosition, rootCanvas == null ? null : rootCanvas.worldCamera, out curPos);

        targetRectTrans.anchoredPosition = curPos;
    }

}

