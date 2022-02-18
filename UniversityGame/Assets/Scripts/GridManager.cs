 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public TerrainGenerator terrain;
    private int x;
    private int z;
    
    private bool go2;
    public int flats;

    void Start()
    {
        go2 = false;
        terrain = gameObject.GetComponent<TerrainGenerator>();
    }

    public void SlopeDetection()
    {
        terrain = gameObject.GetComponent<TerrainGenerator>();

        Vector3 scanner = new Vector3();

        for (x = 0; x < terrain.size; x++)
        {
            for (z = 0; z < terrain.size; z++)
            {
                Vector3 newPos = new Vector3(x, 10, z);

                scanner = newPos;
                RaycastHit hit;

                Physics.Raycast(newPos, Vector3.down, out hit, 100);
                float dot = Vector3.Dot(Vector3.down, hit.normal);

                Color col = Color.red;
                if (dot == -1)
                {
                    col = Color.green;
                }
                Debug.DrawRay(hit.point, Vector3.up * 3, col, 30);
            }
        }
    }
}
