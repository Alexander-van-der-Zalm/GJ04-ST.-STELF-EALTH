using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class NodeEditorWindow : EditorWindow 
{
    // Fields
    // Replace by custom type
    List<Rect> windows;

    // Constructor
    [MenuItem("CustomTools/Node Editor")]
    public static void ShowWindow()
    {
        NodeEditorWindow window = EditorWindow.GetWindow<NodeEditorWindow>();
        window.Init();
    }

    public void Init()
    {
        if (windows != null)
            return;
        // Check if tree selected?

        // Populate
        windows = new List<Rect>();
        windows.Add(new Rect(10, 10, 100, 100));
        windows.Add(new Rect(210, 210, 100, 100));
    }

    private void DrawNodeWindow(int id)
    {
        GUI.DragWindow();
    }

    // OnGui
    void OnGUI()
    {
        GUILayout.Label("Test string");

        BeginWindows();
        for(int i = 0; i < windows.Count; i++)
        {
            windows[i] = GUI.Window(i, windows[i], DrawNodeWindow, "Window" + i.ToString());
        }
        EndWindows();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
