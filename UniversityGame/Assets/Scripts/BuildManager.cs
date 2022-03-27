using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //for now the only object that will be placed is a cube 
    public void placeObject(Vector3 tilePos)
    {
        Vector3 objSpawnPoint = new Vector3(tilePos.x, tilePos.y + 0.5f, tilePos.z);
        GameObject theCuuuuube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        theCuuuuube.transform.position = objSpawnPoint;
    }
    public bool isPlacableLocation(Vector3 tilePos)
    {
        Vector3 castPos = new Vector3(tilePos.x, 10, tilePos.z);
        RaycastHit hit;
        Physics.Raycast(castPos, Vector3.down, out hit, 12);
        float dot = Vector3.Dot(Vector3.down, hit.normal);
        return (dot == -1);
    }

    public Vector3 getTileCoordFromMousePos(Vector2 mousePos)
    {
        //Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 tileCoord = hit.point;
        tileCoord.x = Mathf.Round(tileCoord.x);
        tileCoord.y = Mathf.Round(tileCoord.y);
        tileCoord.z = Mathf.Round(tileCoord.z);
        return tileCoord;
    }
}
