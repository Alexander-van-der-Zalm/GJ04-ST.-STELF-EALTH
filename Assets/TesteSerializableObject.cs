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
    public Vector4 V4;
    public Vector2 V2;
    public Quaternion Q;
    public Transform Tr;
    public Framework.Collections.IQueue<int> Queue;

    public SetType Type; 

    public enum SetType
    {
        Float,
        String,
        Int,
        V3,
        V2,
        V4,
        Quat,
        Null,
        CustomClassNoUnity,
        CustomClassUnity,
        Transform,
        Queue
    }

    public bool ResetObject = false;
    public bool PrintCurrentValue = false;

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
        if (PrintCurrentValue)
        {
            PrintCurrentValue = false;
            Debug.Log("Current value: " + Obj.Object.ToString());
        }
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
                case SetType.V2:
                    Obj = new SerializableObject() { Object = V2 };
                    break;
                case SetType.V4:
                    Obj = new SerializableObject() { Object = V4 };
                    break;
                case SetType.Quat:
                    Obj = new SerializableObject() { Object = Q };
                    break;
                case SetType.Null:
                    Obj = new SerializableObject();
                    break;
                case SetType.CustomClassNoUnity:
                    Obj = new SerializableObject() { Object = new CustomClassNoUnity(StringValue,FloatValue) };
                    break;
                case SetType.CustomClassUnity:
                    Obj = new SerializableObject() { Object = new CustomClassUnity(V3,V2) };
                    break;
                case SetType.Transform:
                    Obj = new SerializableObject() { Object = Tr };
                    break;
                case SetType.Queue:
                    Obj = new SerializableObject() { Object = Queue };
                    break;
            }
        }
    }
}
