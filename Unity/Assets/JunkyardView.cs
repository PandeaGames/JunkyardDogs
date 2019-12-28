using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkyardView : MonoBehaviour
{
    public JunkyardConfig config;
    public JunkyardRenderConfig renderConfig;
    private GameObject _renderingPlane;
    
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
        RenderJunk(junkyard);
    }
    
    private void RenderGround(Junkyard junkyard)
    {
        if(_renderingPlane == null)
        {
            GameObject planePrimitive = GameObject.CreatePrimitive(PrimitiveType.Plane);
            _renderingPlane = Instantiate(planePrimitive, transform);
            DestroyImmediate(planePrimitive);
        }

        GameObject plane = _renderingPlane;
        plane.GetComponent<Renderer>().material = renderConfig.GroundMaterial;

        MeshFilter meshFilter = plane.GetComponent<MeshFilter>();
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
                
                vertices[position] = new Vector3(x * _scale,0, y * _scale);
                
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

    private void RenderJunk(Junkyard junkyard)
    {
        GameObject junk = new GameObject("Junk");
        junk.transform.parent = transform;
        
        for (int x = 0; x < junkyard.Width; x++)
        {
            for (int y = 0; y < junkyard.Height; y++)
            {
                bool hasCleared = junkyard.serializedJunkyard.Cleared[x, y];
                Debug.LogFormat("Rendering [hasCleared:{0]}])");
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
                            Debug.Log("True at " + x+":" +y);
                            prefab = renderLayerConfig.clearedPrefab;
                        }

                        if (prefab != null)
                        {
                            GameObject instance = Instantiate(prefab, new Vector3(x * _scale, 0, y * _scale), prefab.transform.rotation, junk.transform);
                            JunkyardJunk junkyardJunk = instance.GetComponent<JunkyardJunk>();
                            junkyardJunk.Setup(x, y);
                            junkyardJunk.OnClicked += JunkyardJunkOnOnClicked;
                        }
                        
                        break;
                    }
                }
            }
        }
    }

    private void JunkyardJunkOnOnClicked(int x, int y)
    {
        Debug.Log("Clicked at " + x+":" +y);
        _junkyard.serializedJunkyard.Cleared[x, y] = true;
        JunkyardService.Instance.SaveJunkyard(_junkyard);
    }
}
