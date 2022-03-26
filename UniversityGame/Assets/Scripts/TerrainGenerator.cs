using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;


/**
 * Uses perlin noise to generate a mesh for the terrain. Must have a mesh renderer and mesh filter on the attached object.
 */
public class TerrainGenerator : MonoBehaviour
{
    public int size; //length of the terrain in chunks
    public Gradient gradient;

    public Material terrainShader;

    //terrain generation variables
    public float scale = 0.3f;
    public float magnitude = 3f;
    
    //mesh data variables
    public int meshLength;

    private float terrainSeed;

    public void generateTerrain()
    {
        terrainSeed = Random.Range(0,1000000);

        //holder parent game object for all the meshes
        GameObject bigDaddy = new GameObject();
        Transform daddy = bigDaddy.transform;
        bigDaddy.name = "Terrain";

        int chunkNum = 1;
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                string chunkName = "Chunk_" + chunkNum;
                GameObject childObj = new GameObject();
                Transform child = childObj.transform;
                child.name = chunkName;
                Mesh chunk = createMesh(x, z);
                chunk.name = chunkName;
                MeshFilter meshFilter = childObj.AddComponent<MeshFilter>();
                MeshRenderer meshRenderer = childObj.AddComponent<MeshRenderer>();
                MeshCollider meshCollider = childObj.AddComponent<MeshCollider>();
                meshRenderer.sharedMaterial = terrainShader;
                meshFilter.mesh = chunk;
                meshCollider.sharedMesh = chunk;
                child.parent = daddy;
                chunkNum++;
            }
        }

        //run the slope detection. maybe in the future we might want to run other stuff during the "generation" stage of the game and maybe, at that point, we should make a different system for that
        //FindObjectOfType<GridManager>().SlopeDetection();
    }

    /**
     * Populates the values of the vertecies, triangles, and other data used in making the meshes.
     */
    Mesh createMesh(int xOffset, int zOffset)
    {
        xOffset *= meshLength;
        zOffset *= meshLength;

        //variables for mesh data
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Color> vertexColors = new List<Color>();

        float minTerrainHeight = float.MaxValue;
        float maxTerrainHeight = float.MinValue;

        Mesh mesh = new Mesh();

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
                if (x < meshLength + xOffset && z < meshLength + zOffset) //don't overflow stuff
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

        //update mesh
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = vertexColors.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }

    float terrainHeightGeneration(int x, int z)
    {
        float y = Mathf.PerlinNoise((x + terrainSeed) * scale, (z + terrainSeed) * scale) * magnitude;
        y *= 1.5f;
        y = Mathf.Round(y);
        y /= 1.5f;
        return y;
    }
}
