using System.Collections;
using System.Collections.Generic;
using BE;
using JunkyardDogs;
using JunkyardDogs.Components;
using PandeaGames;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class JunkyardMonoView : MonoBehaviour
{
    private enum DataDebugType
    {
        Fog,
        Threshold,
        SpecialChance
    }
    
    public event JunkyardJunkMonoView.JunkInteractionDelegate OnJunkCleared;
    public event JunkyardJunkMonoView.JunkInteractionDelegate OnJunkPointerDown;
    
    public JunkyardConfig config;
    public JunkyardRenderConfig renderConfig;
    private GameObject _renderingPlane;

    [SerializeField] private JunkyardFogOfWar _fogOfWarView;
    [SerializeField] private JunkyardFogOfWar _fogOfWarViewBottom;
    [SerializeField] private MobileRTSCam _camera;
    [SerializeField] private CameraAgent _camAgent;
    [SerializeField] private JunkyardBorder _border;
    [SerializeField] private BotAnimation _botAnimation;
    [SerializeField] private JunkyardJunkMonoView _junkyardJunkMonoView;
    [SerializeField] private float _distanceToActivateSnapping = 2;
    [SerializeField] private Vector3 _camOffsetWhenSnapping = new Vector3(0, 0, -2);
    [SerializeField] private bool _displayDebug;
    [SerializeField] private DataDebugType _displayDebugType;
    
    private JunkyardJunk[,] _junk;
    private Vector3 _cameraSnapPosition;
    private bool _isSnapping;
    private float _distanceWhileSnapping;
    
    [SerializeField]
    private float _scale = 1;
    public float Scale
    {
        get { return _scale; }
    }
    
    
    private void OnDrawGizmos()
    {
        if (_displayDebug && _viewModel != null)
        {
#if UNITY_EDITOR
            switch (_displayDebugType)
            {
                case DataDebugType.Fog:
                {
                    foreach (FogDataPoint dataPoint in _viewModel.Fog.AllData())
                    {
                        Handles.Label(new Vector3(dataPoint.Vector.X, 0, dataPoint.Vector.Y), dataPoint.Data.ToString());
                    }

                    break;
                }
                case DataDebugType.Threshold:
                {
                    foreach (JunkyardThresholdDataPoint dataPoint in _viewModel.Thresholds.AllData())
                    {
                        Handles.Label(new Vector3(dataPoint.Vector.X, 0, dataPoint.Vector.Y), dataPoint.Data.ToString());
                    }

                    break;
                }
                
                case DataDebugType.SpecialChance:
                {
                    foreach (SpecialChanceDataPoint dataPoint in _viewModel.SpecialChanceDataModel.AllData())
                    {
                        Handles.Label(new Vector3(dataPoint.Vector.X, 0, dataPoint.Vector.Y), dataPoint.Data.ToString());
                    }

                    break;
                }
            }
#endif
        }
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return null;
            
            if (_isSnapping)
            {
                float d = Vector3.Distance(_cameraSnapPosition, _camAgent.transform.position);

                bool shouldStopSnapping = d > _distanceWhileSnapping;
                shouldStopSnapping |= d < 0.1;

                _distanceWhileSnapping = d;
                
                if (shouldStopSnapping)
                {
                    _isSnapping = false;
                }
                else
                {
                    _camAgent.transform.position =
                        _camAgent.transform.position + (_cameraSnapPosition - _camAgent.transform.position) / 10;
                }
            }
        }
    }

    private JunkyardViewModel _viewModel;
    private Junkyard _junkyard;
    
    public void Render(JunkyardViewModel viewModel, Bot bot)
    {
        _viewModel = viewModel;
        _junkyard = viewModel.junkyard;
        
        
        if (!JunkyardUtils.HideGround)
        {
            RenderGround(_junkyard);
        }
       
        _fogOfWarView.Render(viewModel);
        _junkyardJunkMonoView.Render(viewModel, renderConfig);
        _fogOfWarViewBottom.Render(viewModel);
        _camera.XMax = _junkyard.Width;
        _camera.ZMax = _junkyard.Height;
        _camera.XMin = 0;
        _camera.ZMin = 0;
        _camAgent.transform.position = new Vector3(_junkyard.X, _camAgent.transform.position.y, _junkyard.Y);
        Game.Instance.GetViewModel<CameraViewModel>(0).Focus(_camAgent);
        _border.Render(renderConfig, _junkyard);
        _botAnimation.Setup(renderConfig, _junkyard, bot, this);
        
        _junkyardJunkMonoView.OnJunkCleared += HandleJunkCleared;
        _junkyardJunkMonoView.OnJunkPointerDown += HandleJunkPointerDown;
    }

    private void HandleJunkPointerDown(int x, int y, JunkyardJunk junkyardjunk)
    {
        OnJunkPointerDown?.Invoke(x, y, junkyardjunk);
    }

    private void HandleJunkCleared(int x, int y, JunkyardJunk junkyardjunk)
    {
        ActivateSnappingConditional(x, y);
        OnJunkCleared?.Invoke(x, y, junkyardjunk);
    }

    private void RenderGround(Junkyard junkyard)
    {
        MeshFilter meshFilter;
        if(_renderingPlane == null)
        {
            _renderingPlane = Instantiate(new GameObject("Ground"), transform);
            _renderingPlane.transform.position = Vector3.zero;
            _renderingPlane.AddComponent<MeshRenderer>();
            _renderingPlane.AddComponent<MeshFilter>();
            meshFilter = _renderingPlane.GetComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            //DestroyImmediate(planePrimitive);
        }

        GameObject plane = _renderingPlane;
        plane.GetComponent<Renderer>().material = renderConfig.GroundMaterial;

        meshFilter = plane.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.sharedMesh;

        int verticiesLength = (junkyard.Width + 1) * (junkyard.Height + 1);

        Vector3[] vertices = new Vector3[verticiesLength];
        Color[] colors = new Color[vertices.Length];
        Vector2[] uvs = new Vector2[vertices.Length];
        Vector3[] normals = new Vector3[vertices.Length];
        float[] grass = new float[vertices.Length];
        // Vector2[] triangles = new Vector2[(int)(dimensions.Area * 2)];

        //for every point, there is 2 triangles, equaling 6 total vertices
        int[] triangles = new int[(int)((junkyard.Width * junkyard.Height) * 6)];

        float totalWidth = junkyard.Width * _scale;
        float totalHeight = junkyard.Height * _scale;

        //Create Vertices
        for (int x = 0; x < junkyard.Width + 1; x++)
        {
            for (int y = 0; y < junkyard.Height + 1; y++)
            {
                int position = (x * (junkyard.Width + 1)) + y;
                
                vertices[position] = new Vector3(x * _scale, junkyard.GetNormalizedHeight(x, y), y * _scale);
                
                colors[position] = new Color(0.5f, 0.5f, 0.5f);
                //uvs[position] = new Vector2((float)x / (float)junkyard.Width, (float)y / (float)junkyard.Height);
                uvs[position] = new Vector2((float)x, (float)y);
                normals[position] = Vector3.up;
                grass[position] = 0.5f;
            }
        }

        List<Vector3> vectorTriangles = new List<Vector3>();

        //Create Triangles
        for (int x = 0; x < junkyard.Width; x++)
        {
            for (int y = 0; y < junkyard.Height; y++)
            {
                SetTriangles(junkyard, x, y, triangles, vectorTriangles);
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.normals = normals;

        plane.SetActive(true);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
    }
    
    private void SetTriangles(Junkyard junkyard, int x, int y, int[] triangles, List<Vector3> vectorTriangles)
    {
        //we are making 2 triangles per loop. so offset goes up by 6 each time
        int triangleOffset = (x * junkyard.Height + y) * 6;
        int verticeX = junkyard.Width + 1;
        int verticeY = junkyard.Height + 1;
                
        //triangle 1
        triangles[triangleOffset] = x * verticeY + y;
        triangles[1 + triangleOffset] = x * verticeY + y + 1;
        triangles[2 + triangleOffset] = x * verticeY + y + verticeY;

        vectorTriangles.Add(new Vector3(triangles[triangleOffset], triangles[1 + triangleOffset], triangles[2 + triangleOffset]));

        //triangle 2
        triangles[3 + triangleOffset] = x * verticeY + y + verticeY;
        triangles[4 + triangleOffset] = x * verticeY + y + 1;
        triangles[5 + triangleOffset] = x * verticeY + y + verticeY + 1;

        vectorTriangles.Add(new Vector3(triangles[3 + triangleOffset], triangles[4 + triangleOffset], triangles[5 + triangleOffset]));
    }
    
    private void ActivateSnappingConditional(int x, int y)
    {
        Vector3 newPosition = new Vector3(x, _camAgent.transform.position.y, y) + _camOffsetWhenSnapping;
        float d = Vector2.Distance(_cameraSnapPosition, newPosition);
        _cameraSnapPosition = newPosition;

        if (d < _distanceToActivateSnapping)
        {
            _isSnapping = true;
            _distanceWhileSnapping = float.MaxValue;
        }
    }
}
