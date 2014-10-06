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
    AI_Agent agent;

    public void SetAgent(AI_Agent agent)
    {
        this.agent = agent;

        // TODO Setup UI nodes

    }

    #region Init & Update

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
        if(Tree == null)
        {
            Debug.Log("BT_BehaviorTree not populated.");
             yield break;
        }
        while (Application.isPlaying)
        {
            Tree.Tick(agent);

            yield return new WaitForSeconds(1.0f/UpdateFrequency);
        }
    }

    #endregion

    #region Test actions

    public void TestBTBasicCompontents()
    {
        int errors = 0;

        BT_BehaviorDelegator f = getBeh(failUpdate, "Fail");
        BT_BehaviorDelegator s = getBeh(succesUpdate, "Succes");
        BT_BehaviorDelegator r = getBeh(runningUpdate, "Running");
        BT_BehaviorDelegator b = getBeh(pauseUpdate, "Pause");

        // Check the selector
        errorCheck(new BT_Selector(f, f, r, s), Status.Running, ref errors);
        errorCheck(new BT_Selector(f, f, f, f), Status.Failed, ref errors);
        errorCheck(new BT_Selector(f, f, s, f), Status.Succes, ref errors);

        // Check the sequencer
        errorCheck(new BT_Sequencer(s, s, r, f), Status.Running, ref errors);
        errorCheck(new BT_Sequencer(f, f, f, f), Status.Failed, ref errors);
        errorCheck(new BT_Sequencer(s, s, s, s), Status.Succes, ref errors);

        if (errors != 0)
            Debug.Log("Behavior Tree test FAILED - " + errors + " Errors." );
        else
            Debug.Log("Behavior Tree test SUCCES - 0 Errors.");
    }

    public void TestDepth()
    {
        BT_BehaviorDelegator f = getBeh(failUpdate, "Fail");
        BT_BehaviorDelegator s = getBeh(succesUpdate, "Succes");
        BT_BehaviorDelegator r = getBeh(runningUpdate, "Running");
        BT_BehaviorDelegator b = getBeh(pauseUpdate, "Pause");

        BT_Behavior tree = sel(sel(sel(sel(b, s),s),s),s);

        tree.Tick(agent);

    }

    private BT_Selector sel(params BT_Behavior[] behaviors)
    {
        return new BT_Selector(behaviors);
    }

    private BT_BehaviorDelegator getBeh(BT_BehaviorDelegator.UpdateDelegate del, string name)
    {
        BT_BehaviorDelegator b = new BT_BehaviorDelegator(BT_Behavior.NodeDescription.BT_NodeType.Action, del);
        b.Description.Name = name;
        return b;
    }

    private void errorCheck(BT_Behavior behavior, Status returnStatus, ref int errors)
    {
        Status beh = behavior.Tick(agent);

        // Check if it is the correct return type and if its not invalid
        if (beh != returnStatus)
            errors++;
        if(beh == Status.Invalid)
            errors++;
    }

    private BT_Behavior.Status failUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Failed;
    }

    private BT_Behavior.Status succesUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Succes;
    }

    private BT_Behavior.Status runningUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        return BT_Behavior.Status.Running;
    }

    private BT_Behavior.Status pauseUpdate(AI_Agent agent, BT_Behavior.NodeDescription node)
    {
        int Depth = (int)agent["Depth"];
        Debug.Break();
        return Status.Succes;
    }

    #endregion
}
