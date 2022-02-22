using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    public TerrainGenerator terrain;
    public GameObject[] trees;
    public float treePercentage;
    private bool generate;
    // Start is called before the first frame update
    void Start()
    {
        generate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (generate == true)
        {
            for(int x = 0; x < terrain.size;x++)
            {
                for (int z = 0; z < terrain.size; z++)
                {
                    float ran1 = Random.Range(0, 10);
                    if(10-ran1 > treePercentage)
                    {
                        float ran2 = Random.Range(0, trees.Length - 1);
                        int ranInt = (int)ran2;
                        RaycastHit hit;
                        Physics.Raycast(new Vector3(x, 10, z), Vector3.down, out hit);
                        GameObject spawnedTree = GameObject.Instantiate(trees[ranInt], new Vector3(x, 10 - hit.distance, z),Quaternion.Euler(Vector3.zero),null);
                    }
                }
            }
            generate = false;
        }
    }
}
