using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;


/**
 * Uses perlin noise to generate a mesh for the terrain. Must have a mesh renderer and mesh filter on the attached object.
 */
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class TerrainGenerator : MonoBehaviour
{
    public int size = 50; //size in squares of the mesh
    public Gradient gradient;
    public float scale = 0.3f;
    public float magnitude = 3f;
    
    public Mesh mesh;
    public Vector3[] vertices;
    private int[] triangles;
    private Color[] vertexColors;

    private float minTerrainHeight;
    private float maxTerrainHeight;

    private void Start()
    {
        mesh = new Mesh();
        mesh.name = "Terrain";
        
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        createMesh();
        updateMesh();
       
        // Create our mesh simplifier and setup our entire mesh in it
        UnityMeshSimplifier.MeshSimplifier meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
        //meshSimplifier.Initialize(mesh);

        //This is where the magic happens, lets simplify!
        //meshSimplifier.SimplifyMesh(0.2f);
        //mesh = meshSimplifier.ToMesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        FindObjectOfType<GridManager>().Test();
       
        //16641
        

        //collider.convex = false;

    }

    private void Update()
    {
        
       
    }

    void createMesh()
    {
       
        vertices = new Vector3[(size+1) * (size+1)];
        Vector2[] uv = new Vector2[vertices.Length];
        //verticies
        for (int z = 0, i = 0; z < size + 1; z++)
        {
            for (int x = 0; x < size + 1; x++)
            {
                float y = Mathf.PerlinNoise(x * scale, z * scale) * magnitude;
                y = (int)(y);
                vertices[i] = new Vector3(x,y,z);
                uv[i] = new Vector2(x / (float)size, z / (float)size);
                if (y > maxTerrainHeight)
                {
                    maxTerrainHeight = y;
                }
                if (y < minTerrainHeight)
                {
                    minTerrainHeight = y;
                }
                i++;
            }
        }

        triangles = new int[6 * size * size];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + size + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + size + 1;
                triangles[tris + 5] = vert + size + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        vertexColors = new Color[vertices.Length];
        for (int z = 0, i = 0; z < size + 1; z++)
        {
            for (int x = 0; x < size + 1; x++)
            {
                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
                vertexColors[i] = gradient.Evaluate(height);
                i++;
            }
        }
        mesh.uv = uv;

    }

    void updateMesh()
    {
        
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = vertexColors;
        mesh.RecalculateNormals();
        
    }
    
}
