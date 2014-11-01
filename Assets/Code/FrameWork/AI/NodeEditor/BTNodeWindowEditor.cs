using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

using Type = System.Type;
using Enum = System.Enum;
using NodeDescription = BT_Behavior.NodeDescription;



[System.Serializable]
public class BTNodeWindowEditor : NodeEditorWindow
{
    public enum nodeType
    {
        Action,
        Composite,
        Decorator,
        Condition
    }
    
    private BT_Tree selectedTree;

    [SerializeField]
    private string path = "Assets/TestNode.asset";

    [SerializeField]
    private int childIndex = 0;

    [SerializeField]
    private int parentIndex = 0;

    [SerializeField]
    private nodeType curType = nodeType.Action;


    private bool connectPress = false;
    private int selectedClass;
    private nodeType lastType;



    public BT_Tree SelectedTree
    {
        get { return selectedTree; }
        private set
        {
            selectedTree = value;
            if(value != null)
                windows = selectedTree.NodeWindows.Cast<NodeWindow>().ToList();
        }
    }

    // Constructor
    [MenuItem("CustomTools/BehaviorTree viewer")]
    public static void ShowWindow()
    {
        Instance = EditorWindow.GetWindow<BTNodeWindowEditor>();
        Instance.Init();
    }

    void OnSelectionChange()
    {
        BT_Tree oldTree = SelectedTree;

        //Debug.Log(Selection.activeObject.GetType());

        if (Selection.activeObject == null)
        {
            SelectedTree = null;
        }
        // Check if tree is selected via an agent
        else if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<AI_Agent>() != null)
        {
            //Todo tree processing if not done yet
            AI_Agent agent = Selection.activeGameObject.GetComponent<AI_Agent>();
            SelectedTree = agent.Tree;
        }
        else if (AssetDatabase.Contains(Selection.activeObject) && Selection.activeObject.GetType().Equals(typeof(BT_Tree)))
        {
            SelectedTree = (BT_Tree)Selection.activeObject;
        }
        else if (AssetDatabase.Contains(Selection.activeObject) && Selection.activeObject.GetType().Equals(typeof(BTNodeWindow)))
        {
            BTNodeWindow window = (BTNodeWindow)Selection.activeObject;
            SelectedTree = window.TreeNode.Tree;
        }
        else if (AssetDatabase.Contains(Selection.activeObject) && Selection.activeObject.GetType().Equals(typeof(BT_TreeNode)))
        {
            BT_TreeNode treeNode = (BT_TreeNode)Selection.activeObject;
            SelectedTree = treeNode.Tree;
        }
        else
            SelectedTree = null;

        if (SelectedTree != oldTree)
        {
            Repaint();
        }

        drawWindow = SelectedTree != null;
    }

    protected override void ChangedFocus()
    {
        if (FocusID >= 0 && FocusID < selectedTree.TreeNodes.Count)
            Selection.objects = new Object[] { selectedTree.TreeNodes[FocusID] };
    }

    #region Input handling
    //void Update()
    //{
    //    //Debug.Log("Update: " + Event.current.button);


    //}
    protected override void HandleInput()
    {
        Event e = Event.current;
        switch(e.rawType)
        {
            case EventType.KeyUp:
                if (e.keyCode == KeyCode.C)
                    ConnectKeyPress(); // Connect parent child

                if (e.keyCode == KeyCode.Escape)
                    connectPress = false; // Cancel parent child connecting

                if(e.keyCode == KeyCode.Delete)
                {
                    selectedTree.DestroyNode(FocusID);
                    Repaint();
                }
                    

                break;
            case EventType.KeyDown:
                //Debug.Log(e);
                break;
        }
    }

    private void ConnectKeyPress()
    {
        if(selectedTree == null || FocusID == -1)
        {
            Debug.Log("Cant connect when there is no node/tree selected");
            connectPress = false;
            return;
        }
                        
        if(!connectPress)
        {
            connectPress = true;
            parentIndex = FocusID;
            Repaint();
        }
        else
        {
            if(FocusID == parentIndex)
            {
                Debug.Log("Select a child first");
                return;
            }
            connectPress = false;
            childIndex = FocusID;
            selectedTree.Connect(parentIndex, childIndex);
            Repaint();
        }
    }

    #endregion

    protected override void DrawButtons()
    {
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Create new Tree"))
            {
                selectedTree = BT_Tree.CreateObjAndAsset("Assets/TestTree.asset");
                Selection.objects = new Object[] { selectedTree };
            }

            if (SelectedTree == null)
            {
                GUILayout.Label("Select a tree");
                // Enum Popup
                EditorGUILayout.EndHorizontal();
                return;
            }

            GUILayout.Label("FocusID:" + FocusID + " | Connect nodes info Parent: " + parentIndex.ToString() 
                            + " Child:" + childIndex.ToString());
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Create Selector"))
            {
                BT_TreeNode node = selectedTree.CreateNode(BT_TreeConstructor.Create<BT_Selector>(HideFlags.DontSave));
                Selection.objects = new Object[] { node };
            }
            if (GUILayout.Button("Create Sequencer"))
            {
                BT_TreeNode node = selectedTree.CreateNode(BT_TreeConstructor.Create<BT_Sequencer>(HideFlags.NotEditable));
                Selection.objects = new Object[] { node };
            }
            if (GUILayout.Button("Create Inverter"))
            {
                BT_TreeNode node = selectedTree.CreateNode(BT_TreeConstructor.Create<BT_Inverter>());
                Selection.objects = new Object[] { node };
            }
            if (GUILayout.Button("Create Negator"))
            {
                BT_TreeNode node = selectedTree.CreateNode(BT_TreeConstructor.Create<BT_AlwayFail>());
                Selection.objects = new Object[] { node };
            }
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        {
            CreateNodeOfChoice();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Delete Node"))
                selectedTree.DestroyNode(FocusID);
            if (GUILayout.Button("Print childCount"))
                Debug.Log(windows[FocusID].Children.Count);
            if (GUILayout.Button("Print behaviorType"))
                Debug.Log(selectedTree[FocusID].Behavior.GetType());
        }
        EditorGUILayout.EndHorizontal();

        // Temp move buttons
        // Move to base
        NavigationArrows(90.0f);
    }

    private void NavigationArrows(float top)
    {
        if (GUI.RepeatButton(new Rect(20, top + 40, 20, 20), "<"))
        {
            panX++;
            Repaint();
        }

        if (GUI.RepeatButton(new Rect(40, top + 40, 20, 20), ">"))
        {
            panX--;
            Repaint();
        }

        if (GUI.RepeatButton(new Rect(30, top + 20, 20, 20), "^"))
        {
            panY++;
            Repaint();
        }

        if (GUI.RepeatButton(new Rect(30, top + 60, 20, 20), "v"))
        {
            panY--;
            Repaint();
        }
    }

    private void CreateNodeOfChoice()
    {
        // Enum popup of selectable types
        curType = (nodeType)EditorGUILayout.Popup((int)curType, Enum.GetNames(typeof(nodeType)));

        Type type = GetType(curType);

        // Reset if changed
        if (lastType != curType)
            selectedClass = 0;

        lastType = curType;

        // Get all the classes from the assembly that inherent from the selected BT node type
        var q1 = from t in Assembly.GetAssembly(typeof(BT_Behavior)).GetTypes()
                 where t.IsClass && (t.IsSubclassOf(type))// && !t.GetInterfaces().Contains(typeof(IReflectionIgnore)))//t == type) // No more equal types
                 select t;

        var q2 = from t in q1
                 select t.Name.ToString();

        List<string> classList = q2.ToList<string>();

        selectedClass = EditorGUILayout.Popup(selectedClass, classList.ToArray());

        // Create the node
        var l1 = q1.ToList();
        if (GUILayout.Button("Create node"))
        {
            selectedTree.CreateNode((BT_BBParameters)System.Activator.CreateInstance(l1[selectedClass]));
            Repaint();
        }
            
    }

    private Type GetType(nodeType bT_NodeType)
    {
        switch (bT_NodeType)
        {
            case nodeType.Action:
                return typeof(BT_Action);
            case nodeType.Condition:
                return typeof(BT_Condition);
            case nodeType.Decorator:
                return typeof(BT_Decorator);
            case nodeType.Composite:
                return typeof(BT_Composite);
        }
        return typeof(BT_Behavior);
    }
}
