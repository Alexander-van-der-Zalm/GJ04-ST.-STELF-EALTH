using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BT_VisualizeTree : Singleton<BT_VisualizeTree> 
{
    public float NodeSize = 1.0f;
    //public GameObject Root;
    //public GameObject Node;
    //private GameObject spawnedRoot;

    //public float 
    private List<GameObject> uiNodes;
    private static string visualizer = "BT_Visualizer";
    private static string node = "BTNode";

    public static void ShowTree(BT_BehaviorTree tree)
    {
        
        if(Instance == null)
        {
            //Has no instance of the singleton yet, so create one
            GameObject go = (GameObject)Instantiate(Resources.Load(visualizer, typeof(GameObject)));
            //GameObject go = (GameObject)GameObject.Instantiate(instance.Root);
            instance = go.AddComponent<BT_VisualizeTree>();
            //go.name = "BT_Visualizer";
            //instance.spawnedRoot = go;
        }
        
        //if (instance.spawnedRoot == null)
        //{
        //    instance.spawnedRoot = (GameObject)GameObject.Instantiate(instance.Root);
        //}
        
        instance.GenerateNodes(tree);
    }

    private void GenerateNodes(BT_BehaviorTree tree)
    {
        //TODO
        List<BT_UINode> list = tree.GetUINodes();

        if (uiNodes == null)
            uiNodes = new List<GameObject>();

        // Create extra nodes if there are not enough 
        // disabled ones in the private uiNodes list
        int nodesNeeded = list.Count - uiNodes.Count;

        Debug.Log("Need " + nodesNeeded + " nodes");

        for (int i = 0; i < nodesNeeded; i++ )
        {
            // Create from prefab
            GameObject newNode = (GameObject)Instantiate(Resources.Load(node, typeof(GameObject)));
            uiNodes.Add(newNode);
        }

        // Copy the BT_UInode in the gameobjects
        // Set the parenting right
        for(int i = 0; i < list.Count; i++)
        {
            BT_UINode node = list[i];
            GameObject obj = uiNodes[i];

            // Change the Node component to the new one from the tree
            BT_UINode objUiNode = obj.GetComponent<BT_UINode>();
            objUiNode.ChangeNode(node);
            
            RectTransform rtr = obj.GetComponent<RectTransform>();
            // Change position
            rtr.position = node.Position;
            
            // Change the parent
            rtr.parent = node.gameObject.GetComponent<RectTransform>();
            
        }

        // TODO
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
