using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NodeWindow :ScriptableObject
{
    #region Fields

    [SerializeField]
    private Rect rect;

    [SerializeField]
    private GUIContent header;

    [SerializeField]
    private int windowId;

    [SerializeField]
    private NodeWindow parent;

    [SerializeField]
    private List<NodeWindow> children;

    #endregion

    #region Properties

    public Rect Rect { get { return rect; } protected set { rect = value; } }
    public int WindowID { get { return windowId; } protected set { windowId = value; } }
    public GUIContent Header { get { return header; } protected set { header = value; } }
    public NodeWindow Parent { get { return parent; } protected set { parent = value; } }
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
        Header = header;
        Children = new List<NodeWindow>();
        hideFlags = HideFlags.HideAndDontSave;
    }

    #endregion

    #region Children & Parenting

    public void AddChildren(params NodeWindow[] windows)
    {
        foreach (NodeWindow newChild in windows)
        {
            // Check if the child already has a parent
            if(newChild.Parent != null)
            {
                // Remove new child from old parent
                newChild.Parent.RemoveChildren(newChild);
            }

            // Check if the parent is this node's parent
            if(Parent == newChild)
            {
                // Remove this window from the ex parent
                Parent.RemoveChildren(this);
            }

            // Set new parent
            newChild.Parent = this;
        }
            
        Children.AddRange(windows.ToList());
    }

    public void RemoveChildren(params NodeWindow[] windows)
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].Parent = null;
            Children.Remove(windows[i]);
        }
    }

    #endregion

    #region GuiDrawing

    public void DrawWindow()
    {
        Rect = GUI.Window(WindowID, Rect, DrawWindowContent, header);

        if (Rect.Contains(Event.current.mousePosition))
            NodeEditorWindow.FocusID = WindowID;
    }

    protected virtual void DrawWindowContent(int id)
    {
        GUI.DragWindow();
    }

    public void DrawConnectionLines()
    {
        // Draw a beziercurve for each child
        foreach(NodeWindow child in Children)
        {
            NodeEditorWindow.DrawNodeCurve(ParentPos, child.ChildPos, 
                                           ParentPos + NodeEditorWindow.TangentStrength * Vector2.up,
                                           child.ChildPos + NodeEditorWindow.TangentStrength * -1 * Vector2.up);
        }
    }

    #endregion
}
