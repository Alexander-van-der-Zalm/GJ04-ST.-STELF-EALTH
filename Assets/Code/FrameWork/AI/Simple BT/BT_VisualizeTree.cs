using UnityEngine;
using System.Collections;

public class BT_VisualizeTree : Singleton<BT_VisualizeTree> 
{
    private static string visualizer = "BT_Visualizer";
    private static string node = "BT_Visualizer";

    public static void ShowTree(BT_BehaviorTree tree)
    {
        if(instance == null)
        {

        }
    }
	
    public static void HideTree()
    {
        instance.enabled = false;
    }

}
