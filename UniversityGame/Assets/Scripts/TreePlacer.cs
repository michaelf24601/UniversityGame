using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    public GameObject[] trees;
    public float treePercentage;
    private bool generate;
    // Start is called before the first frame update

    // Update is called once per frame
    public void placeTrees(int meshSize)
    {
        for (int x = 0; x < meshSize; x++)
        {
            for (int z = 0; z < meshSize; z++)
            {
                float ran1 = Random.Range(0, 100);
                if (ran1 > 100 - treePercentage)
                {
                    int ran2 = (int)Mathf.Round(Random.Range(0, trees.Length));
                   
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(x, 10, z), Vector3.down, out hit);
                    GameObject spawnedTree = GameObject.Instantiate(trees[ran2], new Vector3(x, 10 - hit.distance, z), Quaternion.Euler(Vector3.zero), null);
                    spawnedTree.name = "Tree Type: " + ran2 + " X: " + x + " Z: " +z;
                    spawnedTree.transform.Rotate(0, Random.Range(0, 180), 0);
                    spawnedTree.transform.parent = gameObject.transform;
                }
            }
        }
        generate = false;
    }
}
