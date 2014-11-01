using UnityEngine;
using System.Collections;
using UnityEditor;

public class BTNodeWindow : NodeWindow
{
    #region Classes
    //TODO tree info & Pos info

    #endregion

    // TreeInfo
    int currentTreeRevision;
    BT_Tree Tree;

    // Pos Info

    // Functional info (used for drawing)
    [SerializeField]
    private BT_TreeNode treeNode;

    public BT_TreeNode TreeNode { get { return treeNode; } private set { treeNode = value; } }

    public override int ID
    {
        get { return base.ID; }
        set
        {
            base.ID = value;
            SetName();
        }
    }

    private void SetName()
    {
        name = id + " | WINDOW | " + (TreeNode != null ? TreeNode.Behavior.Description.Name : "");
        Header = new GUIContent((TreeNode != null ? TreeNode.Behavior.Description.Type.ToString() : "") + " [" + id + "]");
        //RefreshAsset();
    }

    internal static BTNodeWindow CreateWindow(BT_TreeNode node, UnityEngine.Object asset, int id)
    {
        BTNodeWindow window = ScriptableObject.CreateInstance<BTNodeWindow>();
        window.Init();
        window.TreeNode = node;
        window.ID = id;
        window.SetName();
        window.BGColor = GUI.color;

        window.Rect = new Rect(100, 100, 100, 100);
        
        // Add object to asset
        window.AddObjectToAsset(asset);

        return window;
    }

    protected override void DrawWindowContent(int id)
    {
        // Makes it draggable
        base.DrawWindowContent(id);

        //TODO render this window
        EditorGUILayout.LabelField(treeNode.Behavior.Description.Name);
        EditorGUILayout.LabelField(treeNode.Name);

        if (((BTNodeWindowEditor)BTNodeWindowEditor.Instance).IsPressedParent(ID))
            BGColor = Color.green;
        else
            BGColor = new Color(.8f,0.9f,0.9f,1.0f);
    }

    
}
