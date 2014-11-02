﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class NodeWindow : TreeNodeLogic<NodeWindow>
{
    #region Fields

    [SerializeField]
    private Rect rect;

    [SerializeField]
    private GUIContent header;

    [SerializeField]
    private Color bgColor;

    #endregion

    #region Properties

    public Color BGColor { get { return bgColor; } set { bgColor = value; } }

    public override int ID
    {
        get { return base.ID; }
        set
        {
            // Change the focus id if the id is changed
            if (NodeEditorWindow.Instance.FocusID == id)
                NodeEditorWindow.Instance.FocusID = value;
            base.ID = value;
        }
    }

    public Rect Rect { get { return rect; } protected set { rect = value; } }

    public GUIContent Header { get { return header; } protected set { header = value; } }

    public Vector2 Position { get { return new Vector2(Rect.x, Rect.y); } set { rect.x = value.x; rect.y = value.y; } }
    public float Width { get { return Rect.width; } set { rect.width = value; } }
    public float Height { get { return Rect.height; } set { rect.height = value; } }
    public Vector2 Mid { get { return new Vector2(Rect.width, Rect.height); } }

    protected Vector2 ChildPos { get { return new Vector2(Rect.x + Rect.width * 0.5f, Rect.y); } }
    protected Vector2 ParentPos { get { return new Vector2(Rect.x + Rect.width * 0.5f, Rect.y + Rect.height); } }

    #endregion

    #region Init

    public void Init(int id, Vector2 topleft, float width, float height, string header)
    {
        Init(id,new Rect(topleft.x,topleft.y,width,height),new GUIContent(header));
    }

    public void Init(int id, Rect startPos, GUIContent header)//, List<NodeWindow> Children)
    {
        ID = id;
        Rect = startPos;
        Header = header;
        Children = new List<NodeWindow>();
        hideFlags = HideFlags.HideAndDontSave;
    }

    #endregion

    #region GuiDrawing

    public void DrawWindow()
    {
        Color oldColor = GUI.color;
        
        if(bgColor!=null)
            GUI.color = bgColor;

        Rect = GUI.Window(ID, Rect, DrawWindowContent, header);

       // if (bgColor.a > 0)
        GUI.color = oldColor;

        if (Rect.Contains(Event.current.mousePosition)&&Event.current.clickCount>0)
            NodeEditorWindow.Instance.FocusID = ID;
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
