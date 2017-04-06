/* Class Name: ImagePlus
 * Describe: http://www.xuanyusong.com/archives/3492
 * Author: 雨松MOMO
 * Create Time: 2015/5/15
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
[AddComponentMenu("UI/Image Plus")]
public class ImagePlus : Image
{
    PolygonCollider2D collider;
    void Awake()
    {
        collider = GetComponent<PolygonCollider2D>();
    }
    override public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return ContainsPoint(collider.points, sp);
    }
    bool ContainsPoint(Vector2[] polyPoints, Vector2 p)
    {
        var j = polyPoints.Length - 1;
        var inside = false;
        for (int i = 0; i < polyPoints.Length; j = i++)
        {
            polyPoints[i].x += transform.position.x;
            polyPoints[i].y += transform.position.y;
            if (((polyPoints[i].y <= p.y && p.y < polyPoints[j].y) || (polyPoints[j].y <= p.y && p.y < polyPoints[i].y)) &&
                (p.x < (polyPoints[j].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[j].y - polyPoints[i].y) + polyPoints[i].x))
                inside = !inside;
        }
        return inside;
    }
}