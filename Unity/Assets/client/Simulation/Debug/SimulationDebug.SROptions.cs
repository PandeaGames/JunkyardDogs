using System.ComponentModel;
using JunkyardDogs.Simulation;
using UnityEngine;

public partial class SROptions
{
    private const string SIMULATION_DATA_EXPORT_CATEGORY = "SIMULATION DATA EXPORT";
    
    // Options will be grouped by category
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public bool GenerateSimulationDebugData {
        get { return SimulationDebugUtils.GenerateSimulationDebugData; }
        set { SimulationDebugUtils.GenerateSimulationDebugData = value; }
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public Initiator InitiatorToDebug {
        get { return SimulationDebugUtils.InitiatorToDebug; }
        set { SimulationDebugUtils.InitiatorToDebug = value; }
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionWeights()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionWeights.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyBodyState()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("BodyStateExporter.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionPriorities()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionPriorities.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyTopDecisions()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("Top Decisions.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionSimBotStatus()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionSimBotStatus.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionMoveBackwards()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionMoveBackwards.data.playerpre");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionMoveRight()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionMoveRight.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionMoveLeft()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionMoveLeft.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionMoveForward()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionMoveForward.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionStartWeaponLeftCharge()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionStartWeaponLeftCharge.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionStartWeaponRightCharge()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionStartWeaponRightCharge.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionStartWeaponTopCharge()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionStartWeaponTopCharge.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionStartWeaponFrontCharge()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionStartWeaponFrontCharge.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionStunned()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionStunned.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionWeaponLeft()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionWeaponLeft.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionWeaponRight()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionWeaponRight.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionWeaponTop()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionWeaponTop.data.playerpref");
    }
    
    [Category(SIMULATION_DATA_EXPORT_CATEGORY)] 
    public void CopyDecisionWeaponFront()
    {
        GUIUtility.systemCopyBuffer = SimulationDebugUtils.GetSimulationDebugData("DecisionWeaponFront.data.playerpref");
    }
}