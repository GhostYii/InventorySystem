/* Class Name: TEST_TEST
 * Describe: 
 * Author: Ghostyii
 * Create Time: 
 */ 
 
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TEST_TEST : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            print("yes");
        else
            print("fuck");
    }
}
