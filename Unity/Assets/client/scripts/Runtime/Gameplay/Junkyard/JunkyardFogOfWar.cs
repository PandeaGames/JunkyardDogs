using System;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class JunkyardFogOfWar : MonoBehaviour
{
    
    public JunkyardConfig config;
    public JunkyardRenderConfig renderConfig;
    private GameObject _renderingPlane;

    [SerializeField] private int _farSightDistance = 1;
    [SerializeField] private int _fullSightDistance = 1;
    [SerializeField] private Vector3 _planeOffset;
    [SerializeField] private Color _fogColor;
    [SerializeField] private Color _clearedColor;
    [SerializeField] private Color _farSightColor;
    [SerializeField] private Color _fullSightColor;
    [SerializeField] private bool _displayDebug;

    [Serializable]
    private struct FogLayerConfig
    {
        public Color color;
    }

    private void OnDrawGizmos()
    {
        if (_displayDebug && _viewModel != null)
        {
            foreach (FogDataPoint dataPoint in _viewModel.Fog.AllData())
            {
                Handles.Label(new Vector3(dataPoint.Vector.X, 0, dataPoint.Vector.Y), dataPoint.Data.ToString());
            }
        }
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
        
        if (!JunkyardUtils.HideFog)
        {
            _junkyard.Update += JunkyardOnUpdate;
            RenderGround(_junkyard); 
        }
    }

    private void JunkyardOnUpdate(int x, int y, Junkyard junkyard)
    {
        RenderGround(junkyard);
    }

    private void RenderGround(Junkyard junkyard)
    {
        MeshFilter meshFilter;
        if(_renderingPlane == null)
        {
            _renderingPlane = Instantiate(new GameObject(), transform);
            _renderingPlane.transform.position = _planeOffset;
            _renderingPlane.AddComponent<MeshRenderer>();
            _renderingPlane.AddComponent<MeshFilter>();
            meshFilter = _renderingPlane.GetComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            //DestroyImmediate(planePrimitive);
        }

        GameObject plane = _renderingPlane;
        plane.GetComponent<Renderer>().material = renderConfig.FogOfWar;

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

        //Create Vertices
        for (int x = 0; x < junkyard.Width + 1; x++)
        {
            for (int y = 0; y < junkyard.Height + 1; y++)
            {
                Color color = _fogConfig[Math.Min(
                    _fogConfig.Length - 1, 
                    _viewModel.Fog[Math.Min(x, junkyard.Width - 1), Math.Min(y, junkyard.Height - 1)])].color;
                
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
