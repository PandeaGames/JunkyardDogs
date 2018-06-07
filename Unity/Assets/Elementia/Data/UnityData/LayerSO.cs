using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class LayerSO : ScriptableObject
{
    [SerializeField]
    private List<LayerEffect> _layerEffects;

    [MenuItem("Elementia Data/World/Layer")]
    static void DoIt()
    {
        LayerSO asset = ScriptableObject.CreateInstance<LayerSO>();
        AssetDatabase.CreateAsset(asset, "Assets/Layer.asset");
        AssetDatabase.SaveAssets();
    }

    public void StepLayerPoint(Dictionary<Layer, LayerSO> _layerTable)
    {

    }
}



[Serializable]
public class LayerEffect
{
    [SerializeField]
    private LayerSO _layer;

    [SerializeField]
    private Effect _effect;

    public LayerSO Layer { get { return _layer; } }
    public Effect Effect { get { return _effect; } }
}

public abstract class Effect : ScriptableObject
{
    public abstract void ApplyEffect(Layer sourceLayer, Layer effectLayer );
    public abstract void ApplyEffect(Layer sourceLayer);
}

[Serializable]
public enum Operand
{
    Plus,
    Minus,
    Devide
}

public class SimpleEffect : Effect
{
    [SerializeField]
    private Operand _operand;

    [SerializeField]
    private float _value;

    public Operand Operand { get { return _operand; } }
    public float Value { get { return _value; } }

    [MenuItem("Elementia Data/Effects/Simple Effect")]
    static void DoIt()
    {
        SimpleEffect asset = ScriptableObject.CreateInstance<SimpleEffect>();
        AssetDatabase.CreateAsset(asset, "Assets/SimpleEffect.asset");
        AssetDatabase.SaveAssets();
    }

    public override void ApplyEffect(Layer sourceLayer)
    {

    }
    public override void ApplyEffect(Layer sourceLayer, Layer effectLayer)
    {

    }
}