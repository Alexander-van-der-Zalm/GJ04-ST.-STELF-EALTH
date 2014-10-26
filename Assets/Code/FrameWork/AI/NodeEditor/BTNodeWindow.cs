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


    internal static BTNodeWindow CreateWindow(BT_TreeNode node, UnityEngine.Object asset, int id)
    {
        BTNodeWindow window = (BTNodeWindow)Create();
        window.ID = id;
        window.TreeNode = node;
        window.name = id + " | WINDOW | " + node.Behavior.Description.Name;

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
