using System;
using System.Collections.Generic;
using JunkyardDogs;
using UnityEngine;
public class JunkyardFogOfWar : MonoBehaviour
{
    public JunkyardRenderConfig renderConfig;
    private GameObject _renderingPlane;

    [SerializeField] private Vector3 _planeOffset;
    [SerializeField] private string _generatedGameObjectName;

    [Serializable]
    private struct FogLayerConfig
    {
        public Color color;
    }

    [SerializeField] private FogLayerConfig[] _fogConfig;
    
    [SerializeField]
    private float _scale = 1;
    public float Scale
    {
        get { return _scale; }
    }

    private Junkyard _junkyard;
    private JunkyardJunk[,] _junk;
    private JunkyardViewModel _viewModel;
    
    public void Render(JunkyardViewModel viewModel)
    {
        _viewModel = viewModel;
        _junkyard = viewModel.junkyard;
        viewModel.Fog.OnDataHasChanged += OnDataHasChanged;
        RenderGround(_junkyard);
    }
    
    
    private Mesh mesh;
    private MeshFilter meshFilter = null;

    private void OnDataHasChanged(IEnumerable<FogDataPoint> data)
    {
        Color[] colors = meshFilter.sharedMesh.colors;
        
        foreach (FogDataPoint dataPoint in data)
        {
            UpdateData(dataPoint.Vector, colors);
        }

        meshFilter.sharedMesh.colors = colors;
        meshFilter.sharedMesh.RecalculateNormals();
        meshFilter.sharedMesh.RecalculateBounds();
        meshFilter.sharedMesh.RecalculateTangents();
    }

    
    private void UpdateData(INTVector vector, Color[] colors)
    {
        int vertPosition = (vector.X * (_viewModel.Width + 1)) + vector.Y;
        colors[vertPosition] = GetColor(vector.X, vector.Y);
    }

    private void RenderGround(Junkyard junkyard)
    {
        if(_renderingPlane == null)
        {
            _renderingPlane = Instantiate(new GameObject(string.IsNullOrEmpty(_generatedGameObjectName) ? "FogOfWar":_generatedGameObjectName), transform);
            _renderingPlane.transform.position = _planeOffset;
            _renderingPlane.AddComponent<MeshRenderer>();
            _renderingPlane.AddComponent<MeshFilter>();
            meshFilter = _renderingPlane.GetComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
        }

        GameObject plane = _renderingPlane;
        plane.GetComponent<Renderer>().material = renderConfig.FogOfWar;

        meshFilter = plane.GetComponent<MeshFilter>();
        mesh = meshFilter.sharedMesh;

        int verticiesLength = (junkyard.Width + 1) * (junkyard.Height + 1);

        Vector3[] vertices = new Vector3[verticiesLength];
        Color[] colors = new Color[vertices.Length];
        Vector2[] uvs = new Vector2[vertices.Length];
        Vector3[] normals = new Vector3[vertices.Length];
        float[] grass = new float[vertices.Length];
        // Vector2[] triangles = new Vector2[(int)(dimensions.Area * 2)];

        //for every point, there is 2 triangles, equaling 6 total vertices
        int[] triangles = new int[(int)((junkyard.Width * junkyard.Height) * 6)];

        //Create Vertices
        for (int x = 0; x < junkyard.Width + 1; x++)
        {
            for (int y = 0; y < junkyard.Height + 1; y++)
            {
                Color color = GetColor(x, y);
                
                int position = (x * (junkyard.Width + 1)) + y;
                
                vertices[position] = new Vector3(x * _scale,junkyard.GetNormalizedHeight(x, y), y * _scale);
                colors[position] = color;
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

    private Color GetColor(int x, int y)
    {
        return _fogConfig[Math.Min(
            _fogConfig.Length - 1,
            _viewModel.Fog[Math.Min(x, _junkyard.Width - 1), Math.Min(y, _junkyard.Height - 1)])].color;
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
}
