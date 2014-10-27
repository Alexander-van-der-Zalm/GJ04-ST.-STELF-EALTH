using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class BTNodeWindowEditor : NodeEditorWindow
{
    private BT_Tree selectedTree;

    [SerializeField]
    private string path = "Assets/TestNode.asset";

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
        if (FocusID > 0 && FocusID < selectedTree.TreeNodes.Count)
            Selection.objects = new Object[] { selectedTree.TreeNodes[FocusID] };
    }

    //void Update()
    //{
        
        
            
    //}

    protected override void DrawButtons()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create new Tree"))
        {
            selectedTree = BT_Tree.CreateObjAndAsset("Assets/TestTree.asset");
            Selection.objects = new Object[] { selectedTree };
        }
        
        if (SelectedTree == null)
        {
            GUILayout.Label("Select a tree");
            EditorGUILayout.EndHorizontal();
            return;
        }
            
        
        // Temp test buttons for functionality
        //EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create new Selector"))
        {
            BT_TreeNode node = selectedTree.CreateNode(new BT_Selector());
            Selection.objects = new Object[] { node };
        }
            
        if (GUILayout.Button("Connect 1 to 0"))
            ConnectChild(1, 0);
        if (GUILayout.Button("Connect 0 to 1"))
            ConnectChild(0, 1);
        if (GUILayout.Button("Delete Node"))
            selectedTree.DestroyNode(FocusID);
        if (GUILayout.Button("Print 0 childCount"))
            Debug.Log(windows[0].Children.Count);

        GUILayout.Label("FocusID:" + FocusID);
        EditorGUILayout.EndHorizontal();

        // Temp move buttons
        // Move to base
        if (GUI.RepeatButton(new Rect(20, 40, 20, 20), "<"))
        {
            panX++;
            Repaint();
        }

        if (GUI.RepeatButton(new Rect(40, 40, 20, 20), ">"))
        {
            panX--;
            Repaint();
        }

        if (GUI.RepeatButton(new Rect(30, 20, 20, 20), "^"))
        {
            panY++;
            Repaint();
        }

        if (GUI.RepeatButton(new Rect(30, 60, 20, 20), "v"))
        {
            panY--;
            Repaint();
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("CreateTree"))
        {
            SelectedTree = BT_Tree.CreateObjAndAsset(path);
            Selection.activeObject = selectedTree;
        }
            
        path = EditorGUILayout.TextField("Path", path);
        if (GUILayout.Button("CreateNewNode"))
        {
            //selectedTree.
        }
        
        EditorGUILayout.EndHorizontal();
    }
}
