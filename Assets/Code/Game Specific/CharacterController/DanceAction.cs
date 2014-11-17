using UnityEngine;
using System.Collections;

public class DanceAction : ICharacterAction 
{
    private bool finished;
    
    public bool Interuptable
    {
        get { return true; }
    }

    public bool CanInterupt
    {
        get { return false; }
    }

    public bool Finished
    {
        get { return finished; }
    }

    public bool CannotMoveDuringAction
    {
        get { return true; }
    }

    public string Name
    {
        get { return "Dance"; }
    }

    public void StartAction(ICharacterActionController controller)
    {
        finished = false;
        // Set Dancing animation = true
    }

    public void StopAction()
    {
        finished = true;
        // Set Dancing animation = false
    }
}
