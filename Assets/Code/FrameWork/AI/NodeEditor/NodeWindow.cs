using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class NodeWindow :ScriptableObject
{
    [SerializeField]
    private Rect rect;

    [SerializeField]
    private GUIContent header;

    [SerializeField]
    private int windowId;

    [SerializeField]
    private List<NodeWindow> children;

    #region Properties

    public Rect Rect { get { return rect; } protected set { rect = value; } }
    public int WindowID { get { return windowId; } protected set { windowId = value; } }
    public GUIContent Header { get { return header; } protected set { header = value; } }
    public List<NodeWindow> Children { get { return children; } protected set { children = value; } }

    protected Vector2 ChildPos { get { return new Vector2(Rect.x + Rect.width * 0.5f, Rect.y); } }
    protected Vector2 ParentPos { get { return new Vector2(Rect.x + Rect.width * 0.5f, Rect.y + rect.height); } }

    #endregion

    #region Init

    public void Init(int id, Vector2 topleft, float width, float height, string header)
    {
        Init(id,new Rect(topleft.x,topleft.y,width,height),new GUIContent(header));
    }

    public void Init(int id, Rect startPos, GUIContent header)//, List<NodeWindow> Children)
    {
        WindowID = id;
        Rect = startPos;
        Children = new List<NodeWindow>();
        Header = header;
        //Children = new List<NodeWindow>();
    }

    #endregion

    public void AddChildren(params NodeWindow[] windows)
    {
        Children.AddRange(windows.ToList());
    }

    public void RemoveChildren(params NodeWindow[] windows)
    {
        for(int i = 0; i < windows.Length; i++)
        {
            Children.Remove(windows[i]);
        }
    }

    public void DrawWindow()
    {
        Rect = GUI.Window(WindowID, Rect, DrawWindowContent, header);
    }

    private void DrawWindowContent(int id)
    {
        GUI.DragWindow();
        GUILayout.Label("Content goes in here");
    }

    public void DrawConnectionLines()
    {
        float tangentStrength = 10;
        foreach(NodeWindow child in Children)
        {
            NodeEditorWindow.DrawNodeCurve(ParentPos, child.ChildPos, ParentPos + tangentStrength * Vector2.up * -1, child.ChildPos + tangentStrength * Vector2.up);
        }
    }
}
