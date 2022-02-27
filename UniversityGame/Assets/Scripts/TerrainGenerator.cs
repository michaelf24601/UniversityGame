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
    public int size = 3; //length of the terrain in chunks
    public Gradient gradient;

    //terrain generation variables
    public float scale = 0.3f;
    public float magnitude = 3f;
    
    //mesh data variables
    int meshLength = 240;

    private float minTerrainHeight;
    private float maxTerrainHeight;

    private float terrainSeed;

    private void Start()
    {
        terrainSeed = Random.Range(0,1000000);

        Mesh[] chunks = new Mesh[size * size];

        //holder parent game object for all the meshes
        GameObject terrain = new GameObject();
        terrain.name = "Terrain";

       for (int x = 0; x < size; x++)
       {
            for (int z = 0; z < size; z++)
            {
                Mesh chunk = new Mesh();
                chunk.name = "Chunk_" + (size * x) + z;

                createMesh(x,z); //calculate values of above mesh data variables
            }
       }

        updateMesh(); //assign mesh data variables to mesh
       
        GetComponent<MeshFilter>().mesh = mesh; 
        GetComponent<MeshCollider>().sharedMesh = mesh;

        //run the slope detection. maybe in the future we might want to run other stuff during the "generation" stage of the game and maybe, at that point, we should make a different system for that
        FindObjectOfType<GridManager>().SlopeDetection();
    }

    /**
     * Populates the values of the vertecies, triangles, and other data used in making the meshes.
     */
    Mesh createMesh(int xOffset, int zOffset)
    {
        //variables for mesh data
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Color> vertexColors = new List<Color>();

        Mesh mesh = new Mesh();

        xOffset *= meshLength;
        zOffset *= meshLength;

        //verticies
        int vert = 0; //keeps track of the number of verticies
        for (int z = zOffset; z < meshLength + 1 + zOffset; z++)
        {
            for (int x = xOffset; x < meshLength + 1 + xOffset; x++)
            {
                //vertex and uv data
                float y = terrainHeightGeneration(x, z);
                vertices.Add(new Vector3(x,y,z));

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
                if (x < meshLength && z < meshLength) //don't overflow stuff
                {
                    triangles.Add(vert);
                    triangles.Add(vert + meshLength + 1);
                    triangles.Add(vert + 1);
                    triangles.Add(vert + 1);
                    triangles.Add(vert + meshLength + 1);
                    triangles.Add(vert + meshLength + 2);
                    //Debug.LogFormat("Tris: ({0},{1},{2},{3},{4},{5})", vert, vert + size + 1, vert + 1, vert + 1, vert + size + 1, vert + size + 2);
                }
                vert++;

                //vertex coloring
                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, y); //basically Unity's way of mapping values
                vertexColors.Add(gradient.Evaluate(height));
            }
        }


    }

    float terrainHeightGeneration(int x, int z)
    {
        float y = Mathf.PerlinNoise((x + terrainSeed) * scale, (z + terrainSeed) * scale) * magnitude;
        y *= 1.5f;
        y = Mathf.Round(y);
        y /= 1.5f;
        return y;
    }

    void updateMesh(Mesh mesh)
    { 
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = vertexColors.ToArray();
        mesh.RecalculateNormals();
    }
}
