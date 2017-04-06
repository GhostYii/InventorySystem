/* Class Name: TEST_ToolTip
 * Describe: 
 * Author: Ghostyii
 * Create Time: 
 */ 
 
using UnityEngine;
using System.Collections;

public class TEST_ToolTip : MonoBehaviour 
{
    static public TEST_ToolTip Instance;

	void Start () 
	{
        Instance = this;
	}

    public void Show(Vector2 scrPos)
    {
        (transform as RectTransform).anchoredPosition = scrPos;
    }

    public void OnRectTransformDimensionsChange()
    {
        print("changed");
    }


    	
}
