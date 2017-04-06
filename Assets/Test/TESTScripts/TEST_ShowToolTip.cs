/* Class Name: TEST_ShowToolTip
 * Describe: 
 * Author: Ghostyii
 * Create Time: 
 */ 
 
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TEST_ShowToolTip : MonoBehaviour ,IPointerEnterHandler ,IPointerExitHandler
{
    public RectTransform canvas;
    private bool enter = false;
    private Vector2 pos = Vector2.zero;

    void Update()
    {
        if (enter)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, null, out pos);
            //向一平公式如下
            pos.x += ((TEST_ToolTip.Instance.transform as RectTransform).sizeDelta.x / 2);
            pos.y -= ((TEST_ToolTip.Instance.transform as RectTransform).sizeDelta.y / 2);
            TEST_ToolTip.Instance.Show(pos);
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
