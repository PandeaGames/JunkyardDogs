
    using JunkyardDogs;
    using JunkyardDogs.Components;
    using JunkyardDogs.Data;
    using PandeaGames;
    using UnityEditor;
    using UnityEngine;

[CustomEditor(typeof(JunkyardTesting))]
public class JunkyardTestingEditor : Editor
{
    [SerializeField]
    private JunkyardData _junkyardData;

    private Junkyard _junkyard;
    private SerializedProperty _junkyardViewSerializedProperty;
    private SerializedProperty _junkyardConfigProperty;
    private SerializedProperty _junkyardDataProperty;

    private void OnEnable()
    {
        _junkyardViewSerializedProperty = serializedObject.FindProperty("_junkyardMonoView");
        _junkyardConfigProperty = serializedObject.FindProperty("_junkyardConfig");
        _junkyardDataProperty = serializedObject.FindProperty("_junkyardData");
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _junkyardViewSerializedProperty = serializedObject.FindProperty("_junkyardMonoView");
        _junkyardData = _junkyardDataProperty.objectReferenceValue as JunkyardData;
        EditorGUI.BeginDisabledGroup(_junkyardData == null);
        if (GUILayout.Button("Generate Preview"))
        {
            GeneratePreview(_junkyardData);
        }
        
        if (GUILayout.Button("Save"))
        {
            Save(_junkyard);
        }
        EditorGUI.EndDisabledGroup();
    }
    
    private void GeneratePreview(JunkyardData junkyardData)
    {
        Game.Instance.GetService<JunkyardUserService>().Load();
        Bot bot = Game.Instance.GetService<JunkyardUserService>().User.Competitor.Inventory.Bots[0];
        JunkyardViewModel junkyardViewModel = Game.Instance.GetViewModel<JunkyardViewModel>(0);
        _junkyard = JunkyardService.Instance.GetJunkyard(junkyardData);
        
        junkyardViewModel.SetJunkyard(_junkyard, _junkyardConfigProperty.objectReferenceValue as JunkyardConfig, Game.Instance.GetService<JunkyardUserService>().User);
        
        JunkyardMonoView junkyardMonoTesting = _junkyardViewSerializedProperty.objectReferenceValue as JunkyardMonoView;
        
        junkyardMonoTesting.Render(junkyardViewModel, bot);
    }
    
    private void Save(Junkyard junkyard)
    {
        JunkyardService.Instance.SaveJunkyard(junkyard);
    }
}
