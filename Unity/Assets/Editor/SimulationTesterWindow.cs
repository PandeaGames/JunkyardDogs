using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class SimulationTesterWindow : EditorWindow, ISimulatedEngagementListener
{
    private const string PARTICIPANT_01_EDITOR_PREF = "PARTICIPANT_01_EDITOR_PREF";
    private const string PARTICIPANT_02_EDITOR_PREF = "PARTICIPANT_02_EDITOR_PREF";
    private const string SEED_EDITOR_PREF = "SIMULATION_TESTER_SEED_PREF";
    private static SimulationTesterWindow Window;
        
    [MenuItem("Junkyard Dogs/Simulation Tester")]
    public static void OpenBalanceEditorWindow()
    {
        if (Window == null)
        {
            Window = (SimulationTesterWindow)EditorWindow.GetWindow(typeof(SimulationTesterWindow));
        }
        Window.Show();
    }

    private string Participant01
    {
        get { return EditorPrefs.GetString(PARTICIPANT_01_EDITOR_PREF, string.Empty); }
        set { EditorPrefs.SetString(PARTICIPANT_01_EDITOR_PREF, value); }
    }
    
    private string Participant02
    {
        get { return EditorPrefs.GetString(PARTICIPANT_02_EDITOR_PREF, string.Empty); }
        set { EditorPrefs.SetString(PARTICIPANT_02_EDITOR_PREF, value); }
    }
    
    private int SeedPref
    {
        get { return EditorPrefs.GetInt(SEED_EDITOR_PREF, 0); }
        set { EditorPrefs.SetInt(SEED_EDITOR_PREF, value); }
    }
    
    [SerializeField, ParticipantStaticDataReference]
    private ParticipantStaticDataReference participantRed01;
    [SerializeField, ParticipantStaticDataReference]
    private ParticipantStaticDataReference participantBlue02;
    [SerializeField] 
    private RulesOfEngagement RulesOfEngagement;
    [SerializeField] 
    private int Seed;
    [SerializeField] 
    private Initiator InitiatorToInspectForData;
    [SerializeField] private string exportPath;

    private Editor testDataEditor;
    private List<ISimulationTestExporter> dataExporters;

    private bool showExporters
    {
        set { EditorPrefs.SetBool("simulationTesterWindow_showExporters", value); }
        get
        {
            return EditorPrefs.GetBool("simulationTesterWindow_showExporters");
        }
    }
    
    private bool inspectInitiator
    {
        set { EditorPrefs.SetBool("simulationTesterWindow_inspectInitiator", value); }
        get
        {
            return EditorPrefs.GetBool("simulationTesterWindow_inspectInitiator");
        }
    }

    private void OnEnable()
    {
        participantRed01 = new ParticipantStaticDataReference();
        participantBlue02 = new ParticipantStaticDataReference();
        dataExporters = AssemblyUtils.GetInstances<ISimulationTestExporter>();
        
        List<ISimulationTestGroupExporter> groupExporters = AssemblyUtils.GetInstances<ISimulationTestGroupExporter>();

        foreach (ISimulationTestGroupExporter simulationTestGroupExporter in groupExporters)
        {
            dataExporters.AddRange(simulationTestGroupExporter.GetDataExporters());
        }
    }

    private void OnGUI()
    {
        try
        {
            participantRed01.ID = Participant01;
            participantBlue02.ID = Participant02;
            UnityEditor.Editor.CreateCachedEditor(this, null, ref testDataEditor);
            testDataEditor.OnInspectorGUI();
            Participant01 = participantRed01.ID;
            Participant02 = participantBlue02.ID;

            showExporters = EditorGUILayout.Foldout(showExporters, "Data Exporters");
            
            if (showExporters)
            {
                EditorGUI.indentLevel++;


                bool areAllSelected = true;
                    
                foreach (ISimulationTestExporter exporter in dataExporters)
                {
                    bool currentValue = EditorPrefs.GetBool(exporter.GetDataName() + "_export_pref");
                    if (!currentValue)
                    {
                        areAllSelected = false;
                        break;
                    }
                }

                EditorGUILayout.Space();
                bool toggleSelectAll = EditorGUILayout.Toggle("Select All", areAllSelected);
                EditorGUILayout.Space();

                bool doSelectAll = !areAllSelected && toggleSelectAll;
                bool doUnSelectAll = areAllSelected && !toggleSelectAll;

                if (doSelectAll)
                {
                    foreach (ISimulationTestExporter exporter in dataExporters)
                    {
                        EditorPrefs.SetBool(exporter.GetDataName() + "_export_pref", true);
                    }
                }
                
                if (doUnSelectAll)
                {
                    foreach (ISimulationTestExporter exporter in dataExporters)
                    {
                        EditorPrefs.SetBool(exporter.GetDataName() + "_export_pref", false);
                    }
                }
                
                foreach (ISimulationTestExporter exporter in dataExporters)
                {
                    bool currentValue = EditorPrefs.GetBool(exporter.GetDataName() + "_export_pref");
                    currentValue = EditorGUILayout.Toggle(exporter.GetDataName(), currentValue);
                    EditorPrefs.SetBool(exporter.GetDataName() + "_export_pref", currentValue);
                }
                
                EditorGUI.indentLevel--;
            }

            if (GUILayout.Button("Run Test"))
            {

                OnTest();
            }
        }
        catch (Exception e)
        {
            EditorUtility.DisplayDialog("Error", e.ToString(), "ok");
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    private void OnTest()
    {
        EditorUtility.DisplayProgressBar("Running Simulation", "Starting", 0);
        Engagement engagement = new Engagement();
        JunkyardUser user = new JunkyardUser();

        engagement.RedCombatent = participantRed01.Data.GetParticipant().GetTeam(user).Bot;
        engagement.BlueCombatent = participantBlue02.Data.GetParticipant().GetTeam(user).Bot;
        engagement.Rules = RulesOfEngagement;
        engagement.Seed = Seed;

        SimulatedEngagement SimulatedEngagement = new SimulatedEngagement(engagement, this);

        int step = 0;
        
        while (!SimulatedEngagement.Step())
        {
            EditorUtility.DisplayProgressBar("Running Simulation", "Running... (" + (++step)+")", 0.5f);
        }

         ExportSimulation(SimulatedEngagement);
         string winner = engagement.RedCombatent == engagement.Outcome.Winner ? participantRed01.ID : participantBlue02.ID;
         EditorUtility.DisplayDialog("Complete", "Winner "+winner, "ok");
    }

    public void StepStart()
    {
        
    }

    public void StepComplete()
    {
        
    }

    public void OnEvent(SimulatedEngagement simulatedEngagement, SimEvent e)
    {
        if (e is SimInstantiationEvent)
        {
            SimInstantiationEvent simInstantiationEvent = e as SimInstantiationEvent;
        }
    }

    private void ExportSimulation(SimulatedEngagement simulatedEngagement)
    {
        List<ISimulationTestExporter> activeExporters =
            dataExporters.FindAll(exporter => EditorPrefs.GetBool(exporter.GetDataName() + "_export_pref"));

        for (int i = 0; i < activeExporters.Count; i++)
        {
            ISimulationTestExporter exporter = activeExporters[i];
            Type exporterType = exporter.GetType();
            EditorUtility.DisplayProgressBar("Exporting Results", exporter.GetDataName(), (float)i/activeExporters.Count);
            
            SimulationTestExportData inputData = new SimulationTestExportData(simulatedEngagement, InitiatorToInspectForData);
            
            StringBuilder data = exporter.GetData(inputData);
            string filePath = exportPath + exporter.GetDataName()+".csv";

            StreamWriter outStream = System.IO.File.CreateText(filePath);
            outStream.WriteLine(data);
            outStream.Close();
        }
    }
}
