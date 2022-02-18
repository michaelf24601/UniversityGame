using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementPrototype : MonoBehaviour
{
    public GameObject obj;

    private GameObject preview = null;

    private void Start()
    {

    }

    private void Update()
    {
        //move preivew to point on mesh
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) //if you hit the mesh
        {
            //move preview to position on mesh
            if (preview == null)
            {
                preview = Instantiate(obj);
                Destroy(preview.GetComponent<Collider>());
            }
            float yOffset = 0.5f;
            Vector3 previewPos = new Vector3(Mathf.RoundToInt(hit.point.x), hit.point.y + yOffset, Mathf.RoundToInt(hit.point.z));
            preview.transform.position = previewPos;

            //check if point on mesh is flatw
            Color col = Color.red;
            if (isFlat(hit.point))
            {
                col = Color.green;
                //if mouse down then instantiate object
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject newObj = Instantiate(obj);
                    newObj.transform.position = previewPos;
                }
            }
            preview.GetComponent<Renderer>().material.color = col;
        }
        else
        {
            if (preview != null)
            {
                Destroy(preview);
                preview = null;
            }
        }
    }

    private bool isFlat(Vector3 pointOnMesh)
    {
        RaycastHit hit;
        Vector3 castPos = new Vector3(pointOnMesh.x, pointOnMesh.y + 10, pointOnMesh.z);
        Physics.Raycast(castPos, Vector3.down, out hit, 11f);
        if (Vector3.Dot(hit.normal, Vector3.down) == -1)
        {
            return true;
        }
        return false;
    }
}
