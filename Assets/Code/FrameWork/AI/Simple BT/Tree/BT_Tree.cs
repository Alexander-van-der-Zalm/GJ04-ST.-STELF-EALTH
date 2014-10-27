using UnityEngine;
using System.Linq;
using Framework.Collections;
using System.Collections;
using System.Collections.Generic;
using Status = BT_Behavior.Status;
using fc = Framework.Collections;


using System;
using System.Reflection;

[System.Serializable]
public class BT_Tree : EasyScriptableObject<BT_Tree>
{
    #region classes

    [Serializable]
    public class TreeInfo
    {
        public string Name = "TreeNoName";
        
        [Range(0.00000000001f, 60)]
        public float UpdateFrequency = 10;

        public int TreeIteration = 0;

        public float NodeSide = 1;
    }

    #endregion

    #region Fields

    // Stores meta information of the tree
    public TreeInfo Info;
    
    // Collection of functional tree elements
    [SerializeField]
    private List<BT_TreeNode> treeNodes;

    // Collection of visual representations of tree elements
    [SerializeField]
    private List<BTNodeWindow> nodeWindows;

    #endregion

    #region Properties

    public BT_TreeNode Root;

    public List<BT_TreeNode> TreeNodes
    {
        get { return treeNodes != null ? treeNodes : treeNodes = new List<BT_TreeNode>(); }
        private set { treeNodes = value; }
    }

    public List<BTNodeWindow> NodeWindows
    {
        get { return nodeWindows != null ? nodeWindows : nodeWindows = new List<BTNodeWindow>(); }
        private set { nodeWindows = value; }
    }

    public BT_TreeNode this[int id]
    {
        get { return TreeNodes[id]; }
    }

    public int Version { get { return Info.TreeIteration; } }

    #endregion

    #region Init


    #endregion

    #region Update Co-Routine

    //// Use this for initialization
    //void Start () 
    //{
    //    //TestTreeFunctionality();
    //    if(Tree != null)
    //        StartCoroutine(updateCR());
    //}

    // Update loop
    public IEnumerator updateCR(AI_Agent agent)
    {
        if(Root == null)
        {
            Debug.Log("BT_BehaviorTree not populated.");
             yield break;
        }
        while (Application.isPlaying)
        {
            Root.Tick(agent);

            yield return new WaitForSeconds(1.0f / Info.UpdateFrequency);
        }
    }

    #endregion

    #region Rebuild Tree (recursive)

    private int IDcounter;
    private int treeDepth;

    private void RebuildTree()
    {
        Root = FindRoot(Root);
        RebuildTree(Root);
    }

    private BT_TreeNode FindRoot(BT_TreeNode root)
    {
        while(root.Parent != null)
        {
            root = root.Parent;
        }
        return root;
    }

    private void RebuildTree(BT_TreeNode root)
    {
        Root = root;
        IDcounter = 0; // Provides the unique id's for this tree by increment
        
        //// Recursive crawl to fill the dictionary
        //TreeNodes = new UDictionaryIntBT_TreeNode();
        //TreeNodes = RecursiveTreeNodeCrawl(TreeNodes, root);

        // Set the new tree iteration
        Info.TreeIteration = Info.TreeIteration + 1 % int.MaxValue;

        //Debug.Log("Tree iteration: " + TreeIteration);
    }

    private UDictionaryIntBT_TreeNode RecursiveTreeNodeCrawl(UDictionaryIntBT_TreeNode dic, BT_TreeNode node)
    {
        // All the nodes have already a valid parent and children
        // Ids need to be set and unique for this rebuild 
        // Then they need to be added to the new dictionary
        
        // First loop over children recursively
        for (int i = 0; i < node.Children.Count; i++)
        {
            dic = RecursiveTreeNodeCrawl(dic, node.Children[i]);
        }
        //Debug.Log(IDcounter);

        // Set ID
        node.ID = IDcounter;
        
        // Add self to the dictionary
        dic[IDcounter] = node;

        // Increment the counter after adding
        IDcounter++;

        //Debug.Log(IDcounter);

        return dic;
    }

    #endregion

    #region Get children

    public BT_TreeNode GetFirstChild(int index)
    {
        return TreeNodes[index].Children.First();
    }

    public List<BT_TreeNode> GetChildren(int index)
    {
        return TreeNodes[index].Children;
    }

    #endregion

    #region Set Children

    //public BT_TreeNode AddChildren(BT_TreeNode parent, params BT_TreeNode[] children)
    //{
    //    // Check if parents type supports children
    //    Type type = parent.Behavior.GetType();
    //    if (!typeof(BT_HasChild).IsAssignableFrom(type))
    //        Debug.LogError("BT_BehaviorTree.AddChildren adding children is not supported for:" + type.ToString());

    //    if (typeof(BT_Decorator).IsAssignableFrom(type) && children.Count() > 1)
    //        Debug.LogError("BT_BehaviorTree.AddChildren adding multiple children is not supported for:" + type.ToString());

    //    if (children.Count() == 0)
    //        return parent;

    //    // Set parents children
    //    parent.Children.AddRange(children);

    //    // Set childrens parent
    //    foreach(BT_TreeNode child in children)
    //        child.Parent = parent;

    //    return parent;
    //}

    #endregion

    #region Tree creation


    #endregion

    #region Node management

    // Add node
    public BT_TreeNode CreateNode(BT_BBParameters behavior)
    {
        // Create the asset and connect it to this asset
        // Functional node part
        BT_TreeNode newNode = BT_TreeNode.CreateNode(behavior, this, TreeNodes.Count);
        // Create UI counterpart
        BTNodeWindow newWindow = BTNodeWindow.CreateWindow(newNode, this, NodeWindows.Count);

        // Add to lists
        TreeNodes.Add(newNode);
        NodeWindows.Add(newWindow);

        // Make root if there is none
        if (Root == null)
            Root = newNode;

        return newNode;
    }
    // Remove node

    // Connect (child & parent)


    // Disconnect (child & parent)

    #endregion

    #region UI nodes

    internal List<BT_UINodeInfo> GetUINodes()
    {
        //if (UINodes == null)
        //    return DefaultUINodeList();

        // Decide how to handle script initilalization of extra tree nodes
        // Decide what to do when there is already ui nodes...
        return DefaultUINodeList();
    }

    private List<BT_UINodeInfo> DefaultUINodeList()
    {
        // Reset tree info
        resetTreeInfo();

        // Fill the 2d list
        List<List<BT_UINodeInfo>> list = new List<List<BT_UINodeInfo>>();
        
        // Recursive fill
        Fill(list, Root, null);

        // Set grid positions

        // Scale positions?

        List<BT_UINodeInfo> flatList = GetFlatList(list);

        return flatList;
    }

    private List<BT_UINodeInfo> GetFlatList(List<List<BT_UINodeInfo>> masterlist)
    {
        List<BT_UINodeInfo> output = new List<BT_UINodeInfo>();

        foreach (List<BT_UINodeInfo> list in masterlist)
            output.AddRange(list);
        
        return output;
    }

    private void Fill(List<List<BT_UINodeInfo>> list, BT_TreeNode node, BT_UINodeInfo parent)
    {
        // Empty tree exit out
        if (node == null)
            return;
        // Set depth based on the parents depth
        int depth = parent == null ? 0 : parent.Depth + 1;
        
        // Set new treeDepth if now deeper then before
        // Also create a new list for that depth
        if (depth > treeDepth)
        {
            treeDepth = depth;
            list.Add(new List<BT_UINodeInfo>());
        }
            
        // index in the row (0 for slot 0, 1 for slot 1, etc.)
        int index = list[depth].Count; 
        
        // Create a new nodeInfo
        BT_UINodeInfo nodeInfo = new BT_UINodeInfo(depth, index, node, parent, this);

        // Add to datastructure
        list[depth].Add(nodeInfo);

        Debug.Log("GenerateNode[d " + depth + " :i " + index + "] NI: " + nodeInfo + " P: " + list[depth][index].Parent);

        // Add children
        for (int i = 0; i < node.Children.Count; i++)
            Fill(list, node.Children[i], nodeInfo);
        // Could add children to nodeInfo as well if needed
    }

    private void resetTreeInfo()
    {
        treeDepth = -1;
    }

    #endregion

    

    //// Disconnect (child & parent)
    //public UDictionaryIntBT_Status GetNewNodeStatus()
    //{
    //    UDictionaryIntBT_Status dic = new UDictionaryIntBT_Status();
    //    foreach(KeyValuePair<int, BT_TreeNode> node in TreeNodes)
    //    {
    //        dic[node.Key] = Status.Invalid;
    //    }
    //    return dic;
    //}
}
