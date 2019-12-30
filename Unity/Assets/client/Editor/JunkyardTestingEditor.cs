
    using JunkyardDogs.Components;
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

    private void OnEnable()
    {
        _junkyardViewSerializedProperty = serializedObject.FindProperty("_junkyardView");
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _junkyardViewSerializedProperty = serializedObject.FindProperty("_junkyardView");
        _junkyardData = (JunkyardData) EditorGUILayout.ObjectField(_junkyardData, typeof(JunkyardData), false);
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
        
        _junkyard = JunkyardService.Instance.GetJunkyard(junkyardData);

        JunkyardMonoView junkyardMonoTesting = _junkyardViewSerializedProperty.objectReferenceValue as JunkyardMonoView;
        
        junkyardMonoTesting.Render(_junkyard, bot);
    }
    
    private void Save(Junkyard junkyard)
    {
        JunkyardService.Instance.SaveJunkyard(junkyard);
    }
}
