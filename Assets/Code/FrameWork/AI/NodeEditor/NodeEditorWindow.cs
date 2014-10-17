using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NodeEditorWindow : EditorWindow 
{
    // Fields
    // Replace by custom type

    List<NodeWindow> windows;

    // Constructor
    [MenuItem("CustomTools/Node Editor")]
    public static void ShowWindow()
    {
        NodeEditorWindow window = EditorWindow.GetWindow<NodeEditorWindow>();
        window.Init();
    }

    public void Init()
    {
        //if (windows != null)
        //    return;

        windows = new List<NodeWindow>();
        generateTestNodes();
    }

    private void generateTestNodes()
    {
        float width = 100;
        float height = 100;

        List<Vector2> topLeftPos = new List<Vector2>();
        topLeftPos.Add(new Vector2(110, 10));
        topLeftPos.Add(new Vector2(10, 210));
        topLeftPos.Add(new Vector2(210, 210));

        for (int i = 0; i < 3; i++)
        {
            windows.Add(NodeWindow.CreateInstance<NodeWindow>());
            windows[i].Init(i, topLeftPos[i], width, height, "TestNode " + i);
        }

        windows[0].AddChildren(windows[1], windows[2]);
    }

    // OnGui
    void OnGUI()
    {
        // Check if tree is selected
        GUILayout.Label("Test string");

        // Draw parent to child connections
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].DrawConnectionLines();
        }

        // Draw the windows
        BeginWindows();
        for(int i = 0; i < windows.Count; i++)
        {
            windows[i].DrawWindow();
        }
        EndWindows();

        // Draw Type Buttons
    }

    public static void DrawNodeCurve(Vector2 parentPos, Vector2 childPos, Vector2 parentTangent, Vector2 childTangent)
    {
        // Todo Shadow
        Handles.DrawBezier(parentPos, childPos, parentTangent, parentTangent, Color.black, null, 1);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
