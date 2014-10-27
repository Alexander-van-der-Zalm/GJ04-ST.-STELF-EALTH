using UnityEngine;
using System.Collections;

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
        Header = new GUIContent((TreeNode != null ? TreeNode.Behavior.Description.Name : "") + " [" + id+"]");
        //RefreshAsset();
    }

    internal static BTNodeWindow CreateWindow(BT_TreeNode node, UnityEngine.Object asset, int id)
    {
        BTNodeWindow window = ScriptableObject.CreateInstance<BTNodeWindow>();
        window.Init();
        window.TreeNode = node;
        window.ID = id;
        window.SetName();

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
    }

    
}
