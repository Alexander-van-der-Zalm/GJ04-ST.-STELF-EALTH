using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NodeEditorWindow : EditorWindow 
{
    // Fields
    [SerializeField]
    private List<NodeWindow> windows;

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
        Debug.Log("INIT");
        //if(windows == null)
            windows = new List<NodeWindow>();
        generateTestNodes();
    }

    // OnGui
    void OnGUI()
    {
        // Check if tree is selected
        //GUILayout.Label("Selected: " + Selection.activeGameObject.name);
        Debug.Log(Selection.activeGameObject == null ? "empty" : Selection.activeGameObject.name);
        //if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<)
        GUILayout.Space(100);

        if (GUILayout.Button("Create new node"))
            addTestNode();

        // Draw parent to child connections
        DrawNodeConnections();

        // Draw the windows
        DrawWindows();
        
        // Draw Type Creation Buttons
        DrawButtons();
    }

    private void DrawButtons()
    {
       // TODO
    }

    private void DrawNodeConnections()
    {
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].DrawConnectionLines();
        }
    }

    private void DrawWindows()
    {
        BeginWindows();
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].DrawWindow();
        }
        EndWindows();
    }

    public static void DrawNodeCurve(Vector2 parentPos, Vector2 childPos, Vector2 parentTangent, Vector2 childTangent)
    {
        // Todo Shadow
        Handles.DrawBezier(parentPos, childPos, parentTangent, childTangent, Color.black, null, 1);
    }


    #region Test

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

        //windows[0].AddChildren(windows[1], windows[2]);
    }

    private void addTestNode()
    {
        windows.Add(NodeWindow.CreateInstance<NodeWindow>());
        int id = windows.Count - 1;
        windows[id].Init(id, new Vector2(10, 10), 100, 100, "TestNode " + id);
    }

    #endregion


    void OnDestroy()
    {
        Debug.Log("Window Destroy");
    }
}
