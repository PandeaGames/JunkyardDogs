using System;
using System.Collections.Generic;
using JunkyardDogs;
using UnityEngine;

public class JunkyardJunkMonoView : MonoBehaviour
{
    public delegate void JunkInteractionDelegate(int x, int y, JunkyardJunk junkyardJunk);
    public event JunkInteractionDelegate OnJunkCleared;
    public event JunkInteractionDelegate OnJunkPointerDown;
    
    [SerializeField] private float _randomJunkScale;
    
    private JunkyardJunk[,] _junk;
    private JunkyardViewModel _viewModel;
    private JunkyardRenderConfig _renderConfig;

    public void Render(JunkyardViewModel viewModel, JunkyardRenderConfig renderConfig)
    {
        _viewModel = viewModel;
        _renderConfig = renderConfig;
        viewModel.VisibleDataModel.OnDataHasChanged += VisibleDataModelOnOnDataHasChanged;
        viewModel.Interactible.OnDataHasChanged += InteractibleOnOnDataHasChanged;
        RenderJunk(viewModel, renderConfig);
    }

    private void InteractibleOnOnDataHasChanged(IEnumerable<InteractibleGridDataPoint> data)
    {
        foreach (InteractibleGridDataPoint dataPoint in data)
        {
            _junk[dataPoint.Vector.X, dataPoint.Vector.Y]?.SetAvailableForCollection(dataPoint.Data);
        }
    }

    private void VisibleDataModelOnOnDataHasChanged(IEnumerable<VisibleGridDataPoint> data)
    {
        foreach (VisibleGridDataPoint dataPoint in data)
        {
            _junk[dataPoint.Vector.X, dataPoint.Vector.Y]?.SetIsVisible(dataPoint.Data);
        }
    }

    private void RenderJunk(JunkyardViewModel viewModel, JunkyardRenderConfig renderConfig)
    {
        Junkyard junkyard = viewModel.junkyard;
        JunkyardConfig config = viewModel.Config;
        GameObject junk = new GameObject("Junk");
        junk.transform.parent = transform;
        _junk = new JunkyardJunk[junkyard.Width,junkyard.Height];
        
        for (int x = 0; x < junkyard.Width; x++)
        {
            for (int y = 0; y < junkyard.Height; y++)
            {
                bool hasCleared = junkyard.serializedJunkyard.Cleared[x, y];
                for (int i = 0; i < config.Layers.Length; i++)
                {
                    JunkyardConfig.JunkyardLayerConfig layerConfig = config.Layers[i];
                    JunkyardRenderConfig.JunkyardLayerRenderConfig renderLayerConfig =
                        renderConfig.Configs[Math.Min(i, renderConfig.Configs.Length - 1)];

                    if (layerConfig.threshold > junkyard.serializedJunkyard.Data[x, y])
                    {
                        GameObject prefab = renderLayerConfig.prefab;

                        if (!hasCleared)
                        {
                            prefab = renderLayerConfig.prefab;
                        }else if(renderLayerConfig.clearedPrefab != null)
                        {
                            prefab = renderLayerConfig.clearedPrefab;
                        }

                        if (prefab != null)
                        {
                            GameObject instance = Instantiate(prefab, new Vector3(x, junkyard.GetNormalizedHeight(x, y), y), 
                                Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0)
                                , junk.transform);
                            JunkyardJunk junkyardJunk = instance.GetComponent<JunkyardJunk>();
                            junkyardJunk.Setup(x, y);
                            junkyardJunk.SetAvailableForCollection(viewModel.Interactible[x, y]);
                            junkyardJunk.SetIsVisible(viewModel.VisibleDataModel[x, y]);
                            junkyardJunk.OnClicked += JunkyardJunkOnOnClicked;
                            junkyardJunk.OnPointerDown += JunkyardJunkOnOnPointerDown;
                            float randomScaleFactor = UnityEngine.Random.Range(1, 1 + _randomJunkScale);
                            junkyardJunk.transform.localScale = new Vector3(randomScaleFactor, randomScaleFactor ,randomScaleFactor);
                            _junk[x, y] = junkyardJunk;
                        }
                        
                        break;
                    }
                }
            }
        }
    }
    
    private void JunkyardJunkOnOnClicked(int x, int y, JunkyardJunk junkyardJunk)
    {
        if (_viewModel.Interactible[x, y])
        {
            _viewModel.ClearSpace(x, y);
            _viewModel.junkyard.X = x;
            _viewModel.junkyard.Y = y;
            JunkyardService.Instance.SaveJunkyard(_viewModel.junkyard);
            Destroy(junkyardJunk.gameObject);
            _junk[x, y] = null;
            
            Instantiate(_renderConfig.JunkClearedAnimation, junkyardJunk.transform.position,
                junkyardJunk.transform.rotation, transform);
            
            OnJunkCleared?.Invoke(x, y, junkyardJunk);
        }
    }
      
    private void JunkyardJunkOnOnPointerDown(int x, int y, JunkyardJunk JunkyardJunk)
    {
        OnJunkPointerDown?.Invoke(x, y, JunkyardJunk); 
    }
}
