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
        go2 = false;
        terrain = gameObject.GetComponent<TerrainGenerator>();
        scanner = GameObject.FindGameObjectWithTag("Scanner");
        timer = 1;
        
        
    }

    // Update is called once per frame
    /*
    void Update()
    {
        timer -= Time.deltaTime;
        //Physics.SyncTransforms();

       
        if (go2 == true && timer <0)
        {
            //terrain.transform.position = new Vector3(0, 3, 0);
            //terrain.collider.cookingOptions = MeshColliderCookingOptions.None;
            //terrain.collider.cookingOptions = MeshColliderCookingOptions.EnableMeshCleaning;

            for (z = 0; z < terrain.size; z++)
            {
              
                for (x = 0; x < terrain.size; x++)
                {
                    Vector3 newPos = new Vector3(x, 10,z);
                    

                    //GameObject a = GameObject.Instantiate(cube);
                    //a.name = "X: " + x + " Z: " + z;
                    scanner.transform.position = newPos;
                    //a.transform.position = new Vector3(x,10-terrain.vertices[go2].y,z);
                    RaycastHit hit;


                    GameObject a = GameObject.Instantiate(cube);
                    a.name = "X: " + x + " Z: " + z;
                    if (Physics.Raycast(newPos,Vector3.down, out hit, 10000f))
                    {
                        //Physics.Simulate(Time.fixedDeltaTime);
                        //a.transform.position = new Vector3(x + 0.5f, 10 - hit.distance, z+0.5f);
                       
                        Debug.Log("X: " + x + " Z: " + z + " Magnitude:" + (10 - hit.distance) + " Angle:" + Mathf.RoundToInt(Vector3.Angle(hit.normal, scanner.transform.TransformDirection(Vector3.down))));
                        if (Mathf.Round(Vector3.Angle(hit.normal, scanner.transform.TransformDirection(Vector3.down))) == 180)
                        {
                            flats++;
                            //Debug.DrawRay(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down) * hit.distance, new Color((float)(0.2 *hit.distance), 0.2f,0.2f), 1000f);
                            Debug.DrawLine(scanner.transform.position, hit.point,Color.green,10000);
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
                    a.transform.position = new Vector3(x + 0.5f, 10 - hit.distance, z + 0.5f);
                    //Mathf.Round(Vector3.Angle(hit.normal, scanner.transform.TransformDirection(Vector3.down)))
                }
            }
            go2 = false;
        }
        
    }
    */
    public void Test()
    {
        terrain = gameObject.GetComponent<TerrainGenerator>();
        scanner = GameObject.FindGameObjectWithTag("Scanner");
        //terrain.collider.cookingOptions = MeshColliderCookingOptions.None;
        //terrain.collider.cookingOptions = MeshColliderCookingOptions.EnableMeshCleaning;

        for (x = 0; x < terrain.size; x++)
        {

            for (z = 0; z < terrain.size; z++)
            {
               
                Vector3 newPos = new Vector3(x, 10, z);


                //GameObject a = GameObject.Instantiate(cube);
                //a.name = "X: " + x + " Z: " + z;
                scanner.transform.position = newPos;
                //a.transform.position = new Vector3(x,10-terrain.vertices[go2].y,z);
                RaycastHit hit;


                GameObject a = GameObject.Instantiate(cube);
                a.name = "X: " + x + " Z: " + z;
                Physics.Raycast(newPos, Vector3.down, out hit, 100);
                Debug.Log("X: " + x + " Z: " + z + " Magnitude:" + (10 - hit.distance) + " Angle:" + Mathf.RoundToInt(Vector3.Angle(hit.normal, scanner.transform.TransformDirection(Vector3.down))));
                if (Mathf.Round(Vector3.Angle(hit.normal, scanner.transform.TransformDirection(Vector3.down))) == 180)
                {
                    flats++;
                    Debug.DrawRay(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down) * hit.distance,Color.red, 1000f);
                    //Debug.DrawLine(scanner.transform.position, hit.point, Color.green, 10000);
                }
                else
                {
                    Debug.DrawRay(scanner.transform.position, scanner.transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow, 1000f);
                }
                float dot = Vector3.Dot(Vector3.down, hit.normal);
               // Debug.DrawRay(hit.point, hit.normal, Color.blue, 30);

                Color col = Color.red;
                if (dot == -1)
                {
                    col = Color.green;
                }
                //Debug.DrawRay(hit.point, Vector3.up * 3, col, 30);
                a.transform.position = new Vector3(x + 0.5f, 10 - hit.distance, z + 0.5f);
                //Mathf.Round(Vector3.Angle(hit.normal, scanner.transform.TransformDirection(Vector3.down)))*/
            }
        }
    }
}
