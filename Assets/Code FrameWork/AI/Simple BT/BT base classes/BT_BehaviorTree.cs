using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Status = BT_Behavior.Status;

public class BT_BehaviorTree : MonoBehaviour 
{
    [Range(0.00000000001f,60)]
    public float UpdateFrequency = 10;

    BT_Behavior Tree;
    List<BT_UINode> Nodes;

    #region Init & Update

    // Use this for initialization
	void Start () 
    {
        //TestTreeFunctionality();

        StartCoroutine(updateCR());
	}

    // Update loop
    private IEnumerator updateCR()
    {
        while (Application.isPlaying)
        {
            Tree.Tick();

            yield return new WaitForSeconds(1.0f/UpdateFrequency);
        }
    }

    #endregion

    #region Test actions

    private void TestBTBasicCompontents()
    {
        int errors = 0;

        BT_BehaviorDelegator f = new BT_BehaviorDelegator(BT_Behavior.NodeDescription.BT_NodeType.Action, failUpdate);
        f.Description.Name = "Fail";
        BT_BehaviorDelegator s = new BT_BehaviorDelegator(BT_Behavior.NodeDescription.BT_NodeType.Action, succesUpdate);
        s.Description.Name = "Succes";
        BT_BehaviorDelegator r = new BT_BehaviorDelegator(BT_Behavior.NodeDescription.BT_NodeType.Action, runningUpdate);
        r.Description.Name = "Running";

        // Check the selector
        errorCheck(new BT_Selector(f, f, r, s), Status.Running, ref errors);
        errorCheck(new BT_Selector(f, f, f, f), Status.Failed, ref errors);
        errorCheck(new BT_Selector(f, f, s, f), Status.Succes, ref errors);

        // Check the sequencer
        errorCheck(new BT_Sequencer(s, s, r, f), Status.Running, ref errors);
        errorCheck(new BT_Sequencer(f, f, f, f), Status.Failed, ref errors);
        errorCheck(new BT_Sequencer(s, s, s, s), Status.Succes, ref errors);

        if (errors != 0)
            Debug.Log("Behavior Tree test UNSSUCCESFULL - Errors: " + errors);
    }

    private void errorCheck(BT_Behavior behavior, Status returnStatus, ref int errors)
    {
        Status beh = behavior.Tick();

        // Check if it is the correct return type and if its not invalid
        if (beh != returnStatus)
            errors++;
        if(beh == Status.Invalid)
            errors++;
    }

    private BT_Behavior.Status failUpdate(BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Failed;
    }

    private BT_Behavior.Status succesUpdate(BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Succes;
    }

    private BT_Behavior.Status runningUpdate(BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Running;
    }

    #endregion
}
