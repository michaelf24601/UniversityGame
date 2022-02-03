using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public TerrainGenerator terrain;
    public GameObject scanner;
    public GameObject cube;
    private int x;
    private int z;
    
    private bool go2;
    public int flats;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        go2 = true;
        terrain = gameObject.GetComponent<TerrainGenerator>();
        scanner = GameObject.FindGameObjectWithTag("Scanner");
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.SyncTransforms();
       
        if (go2 == true)
        {
            terrain.collider.cookingOptions = MeshColliderCookingOptions.None;
            terrain.collider.cookingOptions = MeshColliderCookingOptions.EnableMeshCleaning;
            Physics.SyncTransforms();

            Physics.autoSimulation = false;
            Physics.Simulate(Time.fixedDeltaTime);
            Physics.autoSimulation = true;
            for (x = 0; x <= terrain.size; x++)
            {
                for (z = 0; z <= terrain.size-3; z++)
                {
                    Vector3 newPos = new Vector3(x + 0.5f, 10, z + 0.5f);


                    //GameObject a = GameObject.Instantiate(cube);
                    //a.name = "X: " + x + " Z: " + z;
                    scanner.transform.position = newPos;
                    //a.transform.position = new Vector3(x,10-terrain.vertices[go2].y,z);
                    RaycastHit hit;


                    Ray ray = new Ray(scanner.transform.position, transform.TransformDirection(Vector3.down));
                    if (Physics.Raycast(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
                    {
                        //Physics.Simulate(Time.fixedDeltaTime);
                        //a.transform.position = new Vector3(x + 0.5f, 10 - hit.distance, z+0.5f);
                        Debug.Log("X: " + x + " Z: " + z + " Magnitude:" + (10 - hit.distance) + " Angle:" + Mathf.RoundToInt(Vector3.Angle(hit.normal, scanner.transform.TransformDirection(Vector3.down))));
                        if (Mathf.RoundToInt(Vector3.Angle(hit.normal, scanner.transform.TransformDirection(Vector3.down))) == 180)
                        {
                            flats++;
                            Debug.DrawRay(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down) * hit.distance, Color.red, 1000f);
                        }
                        else
                        {
                            Debug.DrawRay(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow, 1000f);
                        }
                    }
                    else
                    {
                        Debug.DrawRay(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down) * 1, Color.blue, 1000f);
                    }
                   
                }
            }
            go2 = false;
        }
        /*
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = 0.1f;
            if (z <= terrain.size)
            {
                x = 0;
                for(x = 0; x <= terrain.size - 3;x++)
                {
                    Vector3 newPos = new Vector3(x + 0.5f, 10, z + 0.5f);
                   
                   
                    //GameObject a = GameObject.Instantiate(cube);
                    //a.name = "X: " + x + " Z: " + z;
                    scanner.transform.position = newPos;
                    //a.transform.position = new Vector3(x,10-terrain.vertices[go2].y,z);
                    RaycastHit hit;

                   
                    Ray ray = new Ray(scanner.transform.position, transform.TransformDirection(Vector3.down));
                    if (Physics.Raycast(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
                    {
                       
                        //a.transform.position = new Vector3(x + 0.5f, 10 - hit.distance, z+0.5f);
                        Debug.Log("X: " + x + " Z: " + z + " Magnitude:" + (10 - hit.distance) + " Angle:" + Mathf.RoundToInt(Vector3.Angle(hit.normal, scanner.transform.TransformDirection(Vector3.down))));
                        if(Mathf.RoundToInt(Vector3.Angle(hit.normal, scanner.transform.TransformDirection(Vector3.down))) == 180)
                        {
                            flats++; 
                            Debug.DrawRay(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down) * hit.distance, Color.red, 1000f);
                        }
                        else
                        {
                            Debug.DrawRay(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow, 1000f);
                        }
                    }
                    else
                    {
                        Debug.DrawRay(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down) * 1, Color.blue, 1000f);
                    }
                    go2++;
                   
                }
                z++;
            }
        }*/
    }
}
