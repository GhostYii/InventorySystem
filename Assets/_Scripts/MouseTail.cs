/* Class Name: MouseTail
 * Describe: Display a cute particle to follow the mouse
 * Author: Ghostyii
 * Create Time: 2016/2/22
 */ 

using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(TrailRenderer))]
public class MouseTail : MonoBehaviour
{
    //show or not?
    public bool isOn = true;
    //ui Camera, use this for transform the position,default is the camera taged MainCamera
    public Camera uiCamera;
    //a cute particle
    public GameObject particle;
    //you can set this value to control the tail size
    [Range(10,30)]
    public float distance = 20f;   
       
    //Single Script
    public static MouseTail Instance;
    
    private Vector3 pos;
    private GameObject particleIns = null;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (!particle)
        {
            Debug.Log("ParticlePrefabs Non't Exists!");
            return;
        }

        //Create the tail object in start and set it active
        particleIns = Instantiate(particle, transform.position, Quaternion.identity) as GameObject;
        particleIns.SetActive(isOn);
    }

    void Update()
    {
        if (!particle || !particleIns)
            return;

        particleIns.SetActive(isOn);

        if (isOn)
            ShowTail();

    }
    
    //display the tail
    public void ShowTail()
    {
        if (particleIns && particleIns.activeSelf)
            particleIns.transform.position = uiCamera == null ?
                                                                Camera.main.ViewportToWorldPoint(ScreenToWorldPoint(Input.mousePosition, distance)) :
                                                                uiCamera.ViewportToWorldPoint(ScreenToWorldPoint(Input.mousePosition, distance));
    }

    //calculate the mouse position to the screen(Retain the depth of information, also Z axis)
    public static Vector3 ScreenToWorldPoint(Vector3 screenPos,float distance)
    {
        screenPos.x = Mathf.Clamp01(screenPos.x / Screen.width);
        screenPos.y = Mathf.Clamp01(screenPos.y / Screen.height);
        screenPos.z = distance;

        return screenPos;
    }

    public static Vector3 ScreenToWorldPoint(Vector3 screenPos, float width,float height,float distance)
    {
        screenPos.x = Mathf.Clamp01(screenPos.x / width);
        screenPos.y = Mathf.Clamp01(screenPos.y / height);
        screenPos.z = distance;

        return screenPos;
    }
}
