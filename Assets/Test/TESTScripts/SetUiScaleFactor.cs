using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetUiScaleFactor : MonoBehaviour
{
    public CanvasScaler scaler;
    public Slider slider;

    public void SetScale()
    {
        scaler.scaleFactor = slider.value;
    }
}
