using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NodeEditorWindow : EditorWindow
{
    #region Fields
    // Fields
    [SerializeField]
    private List<NodeWindow> windows;

    private float panX;
    private float panY;
    private float GroupSize;

    [SerializeField]
    private Object[] selection;
    [SerializeField]
    private TestListSO test;

    // Settings
    public static float TangentStrength = 80;

    // Not getting serialized, needs work around
    public static int FocusID = -1;

    #endregion

    #region Init

    // Constructor
    [MenuItem("CustomTools/Node Editor")]
    public static void ShowWindow()
    {
        NodeEditorWindow window = EditorWindow.GetWindow<NodeEditorWindow>();
        window.Init();
    }

    void OnEnable()
    {
        if (windows.Count == 0)
            Init();
    }

    public void Init()
    {
        //if (windows != null)
        //    return;
        Debug.Log("INIT");

        windows = new List<NodeWindow>();
        
        generateTestNodes();

        // Test selection
        test = ScriptableObject.CreateInstance<TestListSO>();
        test.Init();

        selection = new Object[1];
        selection[0] = test;

        panX = 0;
        panY = 0;
        GroupSize = 1000;
    }

    #endregion

    void Update()
    {
        //// Check if tree is selected
        //if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<BT_Tree>() != null)
        //{
        //    //Todo tree processing if not done yet
        //}
    }

    #region GUI
    // OnGui
    void OnGUI()
    {
        GUI.BeginGroup(new Rect(panX, panY, GroupSize, GroupSize));
         
        // Draw parent to child connections
        DrawNodeConnections();

        // Draw the windows
        DrawWindows();

        GUI.EndGroup();

        // Draw Type Creation Buttons
        DrawButtons();
    }

    #endregion

    #region GUI sections

    private string path = "Assets/TestNode.asset";

    protected virtual void DrawButtons()
    {
        // Temp test buttons for functionality
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create new node"))
            addTestNode();
        if (GUILayout.Button("Connect 1 to 0"))
            ConnectChild(1, 0);
        if (GUILayout.Button("Connect 0 to 1"))
            ConnectChild(0, 1);
        if (GUILayout.Button("Select Test Object"))
            Selection.objects = selection;
        if (GUILayout.Button("Create Test Object"))
            test.CreateAsset();
        if (GUILayout.Button("Print 0 childCount"))
            Debug.Log(windows[0].Children.Count);

        EditorGUILayout.LabelField("Focus on window: " + FocusID);
        EditorGUILayout.EndHorizontal();

        // Temp move buttons
        if (GUI.RepeatButton(new Rect(20, 40, 20, 20), "<"))
        {
            panX++;
            Repaint();
        }

        if (GUI.RepeatButton(new Rect(40, 40, 20, 20), ">"))
        {
            panX--;
            Repaint();
        }

        if (GUI.RepeatButton(new Rect(30, 20, 20, 20), "^"))
        {
            panY++;
            Repaint();
        }

        if (GUI.RepeatButton(new Rect(30, 60, 20, 20), "v"))
        {
            panY--;
            Repaint();
        }
        EditorGUILayout.BeginHorizontal();
        path = EditorGUILayout.TextField("Path", path);
        if (GUILayout.Button("CreateNode"))
            BT_TreeNode.CreateObjAndAsset(path);
        EditorGUILayout.EndHorizontal();
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

    #endregion

    #region Static helpers

    public static void DrawNodeCurve(Vector2 parentPos, Vector2 childPos, Vector2 parentTangent, Vector2 childTangent)
    {
        // Todo Shadow
        Handles.DrawBezier(parentPos, childPos, parentTangent, childTangent, Color.black, null, 1);
    }

    #endregion

    public void ConnectChild(int parentIndex, int childIndex)
    {
        windows[parentIndex].AddChildren(windows[childIndex]);
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

        windows[0].AddChildren(windows[1], windows[2]);
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
