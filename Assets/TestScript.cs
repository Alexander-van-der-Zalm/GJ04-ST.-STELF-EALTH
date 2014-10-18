using UnityEngine;
using UnityEditor;
using System.Collections;

[System.Serializable]
public class TestScript : ScriptableObject 
{
    public int Test = 1;
    public float Bla = 20.1f;
    public AI_AgentBBAccessParameter Parameter;

    public void Init()
    {
        Test = 10;
        Bla = 12.3f;
        
        Parameter = new AI_AgentBBAccessParameter("TestValue");

        hideFlags = HideFlags.DontSave;
    }

    public void CreateAsset()
    {
        AssetDatabase.CreateAsset(this, "Assets/Test.asset");
    }

}
