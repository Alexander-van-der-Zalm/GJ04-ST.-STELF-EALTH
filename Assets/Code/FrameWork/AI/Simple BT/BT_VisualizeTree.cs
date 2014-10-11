using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BT_VisualizeTree : Singleton<BT_VisualizeTree> 
{
    public float NodeSize = 1.0f;
    //public float 
    private List<GameObject> uiNodes;
    private static string visualizer = "BT_Visualizer";
    private static string node = "BT_UINode";

    public static void ShowTree(BT_BehaviorTree tree)
    {
        if(instance == null)
        {
            // Create the instance
            GameObject obj = Resources.Load<GameObject>(visualizer);
            instance = obj.GetComponent<BT_VisualizeTree>();
        }

        instance.GenerateNodes(tree);
    }

    private void GenerateNodes(BT_BehaviorTree tree)
    {
        List<BT_UINode> list = tree.GetUINodes();

        if (uiNodes == null)
            uiNodes = new List<GameObject>();

        // Create extra nodes if there are not enough 
        // disabled ones in the private uiNodes list
        int nodesNeeded = list.Count - uiNodes.Count;

        for (int i = 0; i < nodesNeeded; i++ )
        {
            // Create from prefab
            GameObject newNode = Resources.Load<GameObject>(node);
            newNode.transform.parent = instance.transform;
            uiNodes.Add(newNode);
        }

        // Copy the BT_UInode in the gameobjects
        for(int i = 0; i < list.Count; i++)
        {
            BT_UINode node = list[i];
            GameObject obj = uiNodes[i];
            // Change the Node component to the new one from the tree
            BT_UINode objUiNode = obj.GetComponent<BT_UINode>();
            objUiNode.ChangeNode(node);
            
            // Change position
            RectTransform rtr = obj.GetComponent<RectTransform>();
            rtr.position = node.Position;
        }

        changeNodesScale(NodeSize);
    }

    private void changeNodesScale(float NodeSize)
    {
        foreach (GameObject node in uiNodes)
        {
            uGuiLockScale scale = node.GetComponent<uGuiLockScale>();
            if (scale == null)
                continue;
            scale.SetScaleFromUnits(NodeSize);
        }
    }
	
    public static void HideTree()
    {
        instance.enabled = false;
    }

}
