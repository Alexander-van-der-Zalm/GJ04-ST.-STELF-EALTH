using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TesteSerializableObject : MonoBehaviour 
{
    public SerializableObject Obj;

    public float FloatValue = 10.0f;
    public string StringValue = "";
    public int IntValue = 10;
    public Vector3 V3;

    public UnityEngine.Object UnityObject;

    public SetType Type; 

    public enum SetType
    {
        Float,
        String,
        Int,
        V3
    }

    public bool ResetObject = false;

	// Use this for initialization
	void Start () 
    {
        if (Obj != null && !ResetObject)
            return;
        
        Obj = new SerializableObject() { Object = 10.0f };
        Debug.Log("Created new SerializableObject");
	}

    void Update()
    {
        if(ResetObject)
        {
            ResetObject = false;

            switch (Type)
            {
                case SetType.Float:
                    Obj = new SerializableObject() { Object = FloatValue };
                    break;
                case SetType.Int:
                    Obj = new SerializableObject() { Object = IntValue };
                    break;
                case SetType.String:
                    Obj = new SerializableObject() { Object = StringValue };
                    break;
                case SetType.V3:
                    Obj = new SerializableObject() { Object = V3 };
                    break;
            }
        }
    }
}
