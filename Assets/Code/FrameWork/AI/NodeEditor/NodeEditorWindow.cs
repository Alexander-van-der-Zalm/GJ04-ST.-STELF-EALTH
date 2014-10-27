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
    protected List<NodeWindow> windows;

    protected float panX;
    protected float panY;
    protected float GroupSize;

    [SerializeField]
    protected Object[] selection;

    [SerializeField]
    protected bool drawWindow = true;

    private static NodeEditorWindow instance;

    public static NodeEditorWindow Instance 
    {
        get { return instance != null ? instance : instance = EditorWindow.GetWindow<NodeEditorWindow>(); }
        protected set { instance = value; }
    }


    // Settings
    public static float TangentStrength = 80;

    [System.NonSerialized]
    private int focusID = -1;

    public int FocusID { get { return focusID; } set { focusID = value; ChangedFocus(); } }

    protected virtual void ChangedFocus()
    {
        
    }

    #endregion

    #region Init

    void OnEnable()
    {
        if (windows == null  || windows.Count == 0)
            Init();
    }

    public virtual void Init()
    {
        Debug.Log("Base Init");

        windows = new List<NodeWindow>();
        
        panX = 0;
        panY = 0;
        GroupSize = 1000;
    }

    #endregion

    #region GUI drawing

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

    private void DrawNodeConnections()
    {
        if (!drawWindow)
            return;
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].DrawConnectionLines();
        }
    }

    private void DrawWindows()
    {
        if (!drawWindow)
            return;
        BeginWindows();
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].DrawWindow();
        }
        EndWindows();
    }

    

    protected virtual void DrawButtons()
    {
      
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


    void OnDestroy()
    {
        Debug.Log("Window Destroy");
    }
}
