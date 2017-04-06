/* Class Name: ToolWindow
 * Describe: 
 * Author: Ghostyii
 * Create Time: 
 */
 
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text;

public class ToolWindow : EditorWindow
{
    StringBuilder sb = new StringBuilder();

    [MenuItem("Tool/Messages")]
    static void Init()
    {
        EditorWindow window = GetWindow<ToolWindow>();
        window.Show();
    }

    void OnGUI()
    {
        int hour = (int)(EditorApplication.timeSinceStartup / 3600f);
        int min = (int)((EditorApplication.timeSinceStartup - hour * 3600f) / 60f);
        int sec = (int)(EditorApplication.timeSinceStartup - hour * 3600 - min * 60);

        EditorGUILayout.LabelField("Time since start: ", string.Format("{0} hour {1} min {2} sec", hour.ToString(), min.ToString(), sec.ToString()));
        //EditorGUILayout.LabelField("Time since start: ", (EditorApplication.timeSinceStartup / 60f).ToString("F0") + "min");
        //EditorGUILayout.LabelField("Time since start: ", (EditorApplication.timeSinceStartup / 3600f).ToString("F0") + "hour");
        this.Repaint();
    }
}
