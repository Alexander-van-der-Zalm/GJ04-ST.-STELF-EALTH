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
    #region Enums
    public enum nodeType
    {
        Action,
        Composite,
        Decorator,
        Condition
    }

    #endregion

    #region Fields

    [SerializeField]
    private string path = "Assets/TestNode.asset";

    [SerializeField]
    private int childIndex = 0;

    [SerializeField]
    private int parentIndex = 0;

    [SerializeField]
    private nodeType curType = nodeType.Action;

    [SerializeField]
    private BT_Tree selectedTree;

    private bool connectPress = false;
    private int selectedClass;
    private nodeType lastType;

    #endregion

    #region Properties

    public BT_Tree SelectedTree
    {
        get { return selectedTree; }
        private set
        {
            selectedTree = value;
            drawWindow = selectedTree != null;
            Repaint();
            if (value != null)
                windows = selectedTree.NodeWindows.Cast<NodeWindow>().ToList();
        }
    }

    #endregion

    #region Create

    // Constructor
    [MenuItem("CustomTools/BehaviorTree viewer")]
    public static void ShowWindow()
    {
        Instance = EditorWindow.GetWindow<BTNodeWindowEditor>();
        Instance.Init();
    }

    #endregion

    #region Selection related

    void OnSelectionChange()
    {
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
    }

    protected override void ChangedFocus()
    {
        if (FocusID >= 0 && FocusID < SelectedTree.TreeNodes.Count)
            Selection.objects = new Object[] { SelectedTree.TreeNodes[FocusID] };
    }

    #endregion

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
            case EventType.KeyDown:
                // Connect parent child
                if (e.keyCode == KeyCode.C)
                    ConnectKeyPress(); 

                // Cancel action
                if (e.keyCode == KeyCode.Escape)
                {
                    connectPress = false; // Cancel parent child connecting
                    Repaint();
                }
                    
                // Delete Node
                if(e.keyCode == KeyCode.Delete)
                {
                    DeleteFocus();
                    
                }

                // Disconnect Node
                if(e.keyCode == KeyCode.D)
                {
                    DisconnectFocus();
                }
                
                if(e.keyCode == KeyCode.Alpha1)
                {
                    createNode<BT_Selector>(e.mousePosition);
                }
                if (e.keyCode == KeyCode.Alpha2)
                {
                    createNode<BT_Sequencer>(e.mousePosition);
                }
                if (e.keyCode == KeyCode.Alpha3)
                {
                    createNode<BT_Inverter>(e.mousePosition);
                }
                if (e.keyCode == KeyCode.Alpha4)
                {
                    createNode<BT_AlwayFail>(e.mousePosition);
                }

                break;
            //case EventType.KeyDown:
            //    //Debug.Log(e);
            //    break;
        }
    }

    private void DisconnectFocus()
    {
        if (SelectedTree == null || FocusID == -1)
        {
            Debug.Log("Cant disconnect when there is no node/tree selected");
            return;
        }

        SelectedTree.TreeNodes[FocusID].DisconnectAll();
        SelectedTree.NodeWindows[FocusID].DisconnectAll();
        Repaint();
    }

    private void ConnectKeyPress()
    {
        if(SelectedTree == null || FocusID == -1)
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
            SelectedTree.Connect(parentIndex, childIndex);
            Repaint();
        }
    }

    #endregion

    #region GUI Related

    protected override void DrawButtons()
    {
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Create new Tree"))
            {
                SelectedTree = BT_Tree.CreateObjAndAsset("Assets/TestTree.asset");
                Selection.objects = new Object[] { SelectedTree };
            }

            TreeAssetSelector();
            
            if (SelectedTree == null)
            {
                EditorGUILayout.EndHorizontal();
                GUILayout.Label("Select a tree in hierarchy or from the menu");
                // Enum Popup
                
                return;
            }

            GUILayout.Label("FocusID:" + FocusID + " | Connect nodes info Parent: " + parentIndex.ToString() 
                            + " Child:" + childIndex.ToString());
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Create Selector (1)"))
            {
                createNode<BT_Selector>();
            }
            if (GUILayout.Button("Create Sequencer (2)"))
            {
                createNode<BT_Sequencer>();
            }
            if (GUILayout.Button("Create Inverter (3)"))
            {
                createNode<BT_Inverter>();
            }
            if (GUILayout.Button("Create Negator (4)"))
            {
                createNode<BT_AlwayFail>();
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
            string buttonText = "Connect - Select parent (C)";
            if (connectPress)
                buttonText = "Connect - Select child (C)";

            if (GUILayout.Button(buttonText))
                ConnectKeyPress();
            if (GUILayout.Button("Disconnect Node (D)"))
                DisconnectFocus();
            if (GUILayout.Button("Delete Node (Del)"))
                DeleteFocus();
                
        }
        EditorGUILayout.EndHorizontal();

        // Temp move buttons
        // Move to base
        NavigationArrows(90.0f);
    }

    private void DeleteFocus()
    {
        SelectedTree.DestroyNode(FocusID);

        if (FocusID >= SelectedTree.TreeNodes.Count)
        {
            FocusID = SelectedTree.NodeWindows.Count - 1;
            Debug.Log("DeleteFocus focusID: " + FocusID + " Count: " + (SelectedTree.NodeWindows.Count - 1).ToString());
        }

        Repaint();
    }

    private BT_TreeNode createNode<T>(Vector2 pos) where T : BT_BBParameters
    {
        BT_TreeNode node = createNode<T>();
        selectedTree.NodeWindows[node.ID].Position = pos - selectedTree.NodeWindows[node.ID].Mid*0.5f;
        return node;
    }

    private BT_TreeNode createNode<T>()where T:BT_BBParameters
    {
        BT_TreeNode node = SelectedTree.CreateNode(BT_TreeConstructor.Create<T>(HideFlags.DontSave));
        Selection.objects = new Object[] { node };
        return node;
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

    #endregion

    #region Cool GUI stuff (select tree/node creation)

    private void TreeAssetSelector()
    {
        SelectedTree = (BT_Tree)EditorGUILayout.ObjectField(SelectedTree, typeof(BT_Tree), false);
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
            SelectedTree.CreateNode((BT_BBParameters)System.Activator.CreateInstance(l1[selectedClass]));
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

    #endregion

    #region NodeWindow helper

    internal bool IsPressedParent(int ID)
    {
        return connectPress && parentIndex == ID;
    }

    #endregion
}
