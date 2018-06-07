using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWindow : ScreenController {

    [SerializeField]
    private WorldSO _world;

    [SerializeField]
    private AreaSO _areaSO;

    [SerializeField]
    private GameObject _plane;

    [SerializeField]
    private ServiceManager _serviceManager;

    private ElementiaUserService _userService;
    private ElementiaUser _user;



    public override void Setup(WindowController window, Config config)
    {
        base.Setup(window, config);

        _userService = _serviceManager.GetService<ElementiaUserService>();
        _user = _userService.Load();

        if (_user.TestArea == null)
        {
            _user.TestArea = WorldSO.GenerateArea(_areaSO, _world);
        }

        //GameObject testPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        float scale = 0.1f;
        Area dimensions = _user.TestArea;
        foreach (Layer layer in _user.TestArea.Layers)
        {
            LayerSO layerSO = layer.LayerReference.Load<LayerSO>();
            GameObject plane = Instantiate(_plane);

            MeshFilter meshFilter = plane.GetComponent<MeshFilter>();
            Mesh mesh = meshFilter.sharedMesh;
            mesh.Clear();

            Vector3[] vertices = new Vector3[(dimensions.x + 1) * (dimensions.y + 1)];
            Color[] colors = new Color[vertices.Length];
            // Vector2[] triangles = new Vector2[(int)(dimensions.Area * 2)];

            //for every point, there is 2 triangles, equaling 6 total vertices
            int[] triangles = new int[(int)(dimensions.GetArea * 6)];

            //Create Vertices
            for (int x = 0; x < dimensions.x + 1; x++ )
            {
                for (int y = 0; y < dimensions.y + 1; y++)
                {
                    int position = (x * (dimensions.y + 1)) + y;
                    vertices[position] = new Vector3(x * scale, y * scale, 0);
                    colors[position] = new Color((float)x / (dimensions.x + 1), (float)y / dimensions.y + 1, 1f);
                }
            }

            //Create Triangles
            for (int x = 0; x < dimensions.x; x++)
            {
                for (int y = 0; y < dimensions.y; y++)
                {
                    //we are making 2 triangles per loop. so offset goes up by 6 each time
                    int triangleOffset = (x * dimensions.y + y) * 6;
                    int verticeX = dimensions.x + 1;
                    int verticeY = dimensions.y + 1;

                    //triangle 1
                    triangles[triangleOffset] = x * verticeY + y;
                    triangles[1 + triangleOffset] = x * verticeY + y + 1;
                    triangles[2 + triangleOffset] = x * verticeY + y + verticeY;

                    //triangle 2
                    triangles[3 + triangleOffset] = x * verticeY + y + verticeY;
                    triangles[4 + triangleOffset] = x * verticeY + y + 1;
                    triangles[5 + triangleOffset] = x * verticeY + y + verticeY + 1;

                   // break;
                }

                //break;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.colors = colors;

            //mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0),new Vector3(1, 1, 0) };
            //mesh.triangles = new int[] { 0, 1, 2 };
            //plane.GetComponent<MeshCollider>().sharedMesh
            //meshFilter.
        }
    }

    public void SaveUser()
    {
        _userService.Save(_user);
    }

    public void StepWorld()
    {
        AreaSO.StepArea(_user.TestArea, _areaSO, new AreaSteper());
    }
}
