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
    public int size = 3; //size in squares of the mesh
    public Gradient gradient;

    //terrain generation variables
    public float scale = 0.3f;
    public float magnitude = 3f;
    
    //mesh data variables
    private Mesh mesh;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();
    private List<Color> vertexColors = new List<Color>();

    private float minTerrainHeight;
    private float maxTerrainHeight;

    private void Start()
    {
        mesh = new Mesh();
        mesh.name = "Terrain";
        
        createMesh(); //calculate values of above mesh data variables
        updateMesh(); //assign mesh data variables to mesh
       
        GetComponent<MeshFilter>().mesh = mesh; 
        GetComponent<MeshCollider>().sharedMesh = mesh;

        //run the slope detection. maybe in the future we might want to run other stuff during the "generation" stage of the game and maybe, at that point, we should make a different system for that
        FindObjectOfType<GridManager>().SlopeDetection();
    }

    void createMesh()
    {

        //verticies
        int vert = 0; //keeps track of the number of verticies
        for (int z = 0; z < size + 1; z++)
        {
            for (int x = 0; x < size + 1; x++)
            {
                //vertex and uv data
                float y = terrainHeightGeneration(x, z);
                vertices.Add(new Vector3(x,y,z));
                uvs.Add(new Vector2(x, z));

                //record min and max terrain hight b/c idk, maybe it'll come in handy later
                if (y > maxTerrainHeight)
                {
                    maxTerrainHeight = y;
                }
                if (y < minTerrainHeight)
                {
                    minTerrainHeight = y;
                }

                //Debug.LogFormat("Vert: ({0},{1})", x, z);

                //triangles 
                if (x < size && z < size) //don't overflow stuff
                {
                    triangles.Add(vert);
                    triangles.Add(vert + size + 1);
                    triangles.Add(vert + 1);
                    triangles.Add(vert + 1);
                    triangles.Add(vert + size + 1);
                    triangles.Add(vert + size + 2);
                    //Debug.LogFormat("Tris: ({0},{1},{2},{3},{4},{5})", vert, vert + size + 1, vert + 1, vert + 1, vert + size + 1, vert + size + 2);
                }
                vert++;

                //vertex coloring
                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, y); //basically Unity's way of mapping values
                vertexColors.Add(gradient.Evaluate(height));
            }
        }

        /*
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
        */

    }

    float terrainHeightGeneration(int x, int z)
    {
        float y = Mathf.PerlinNoise(x * scale, z * scale) * magnitude;
        y *= 1.5f;
        y = Mathf.Round(y);
        y /= 1.5f;
        return y;
    }

    void updateMesh()
    { 
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = vertexColors.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
    }
}
