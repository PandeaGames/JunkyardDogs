using System;
using System.Collections;
using System.Collections.Generic;
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
    
    [SerializeField]
    private float _scale = 1;
    public float Scale
    {
        get { return _scale; }
    }

    private Junkyard _junkyard;
    
    public void Render(Junkyard junkyard)
    {
        _junkyard = junkyard;
        RenderGround(junkyard);
        junkyard.Update += JunkyardOnUpdate;
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

        float totalWidth = junkyard.Width * _scale;
        float totalHeight = junkyard.Height * _scale;

        //Create Vertices
        for (int x = 0; x < junkyard.Width + 1; x++)
        {
            for (int y = 0; y < junkyard.Height + 1; y++)
            {
                Color color = GetColor(x, y, junkyard);
                
                int position = (x * (junkyard.Width + 1)) + y;
                
                vertices[position] = new Vector3(x * _scale,junkyard.GetNormalizedHeight(x, y), y * _scale);
                
                colors[position] = color;
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
    
    
    private Color GetColor(int x, int y, Junkyard junkyard)
    {
        bool hasCleared = false;
        if (x < junkyard.Width && y < junkyard.Height)
        {
            hasCleared = junkyard.serializedJunkyard.Cleared[x, y];
        }

        Color color = !hasCleared ? _fogColor : _clearedColor;

        if (!hasCleared)
        {
            int totalSightDistance = _farSightDistance + _fullSightDistance;
            bool hasClearedAdjacent = false;
            bool hasClearedCloseAdjacent = false;
            for (int dx = x - totalSightDistance; dx <= x + totalSightDistance; dx++)
            {
                for (int dy = y - totalSightDistance; dy <= y + totalSightDistance; dy++)
                {
                    if (dx > 0 && dx < junkyard.Width && dy > 0 && dy < junkyard.Height)
                    {
                        bool isAdjacentCleared = junkyard.serializedJunkyard.Cleared[dx, dy];
                        
                        hasClearedAdjacent |= isAdjacentCleared;
    
                        if (hasClearedAdjacent && isAdjacentCleared)
                        {
                            hasClearedCloseAdjacent |= Math.Abs(dx - x) <= _fullSightDistance &&
                                                       Math.Abs(dy - y) <= _fullSightDistance;
                        }
                    }
                }
            }

            if (hasClearedAdjacent)
            {
                if (hasClearedCloseAdjacent)
                {
                    return _fullSightColor;
                }
                else
                {
                    return _farSightColor;
                }
            }
        }
        
        return color;
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
