using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //for now the only object that will be placed is a cube 
    public void placeObject(Vector3 tilePos)
    {
        Vector3 objSpawnPoint = new Vector3(tilePos.x + 0.5f, tilePos.y + 0.5f, tilePos.z + 0.5f);
        GameObject theCuuuuube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        theCuuuuube.transform.position = objSpawnPoint;
    }

    /**
     * tilePos should be the bottom left corner of the tile. The same as the return value from 
     * getTileCoordFromMousePos().
     */
    public bool isPlacableLocation(Vector3 tilePos)
    {
        /**
         * +-0.1 because sometimes half of a tile can be flat and half can be sloped so doing it this way means that
         * the function will return false if any part of the tile is sloped.
         */
        Vector3[] castPositions =
        {
            new Vector3(tilePos.x + 0.5f, 10f, tilePos.z + 0.6f),
            new Vector3(tilePos.x + 0.5f, 10f, tilePos.z + 0.4f),
        };
        foreach(Vector3 pos in castPositions)
        {
            //check for slope
            RaycastHit hit;
            Physics.Raycast(pos, Vector3.down, out hit, 12);
            float dot = Vector3.Dot(Vector3.down, hit.normal);
            if (dot != -1) return false;

            //check for foundation (cannot place on anything other than a foundation)
            if (!hit.collider.gameObject.CompareTag("Foundation")) return false;
        }
        return true;
    }

    /**
     * Deletes an object at the tile pos if it is present. Cannot be used to delete foundations.
     */
    public void deleteObject(Vector3 tilePos)
    {
        Vector3 castPosition = new Vector3(tilePos.x + 0.5f, 10f, tilePos.z + 0.5f);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Terrain");
        mask = ~mask; //collide with everything except the terrain layer b/c don't want to delete the terrain
        if (Physics.Raycast(castPosition, Vector3.down, out hit, 12, mask)) //if you hit something
        {
            if (hit.transform.gameObject.tag != "Foundation")
            {
                Destroy(hit.transform.gameObject);
                /**
                 * note that this is potentially problamatic because references to this gameobject will not be null
                 * just the gameobject will no longer exist at that location in memory so if there are any references
                 * to this gameobject then those references will now point to some random data in memory which could
                 * cause problems
                 */
            }
        }
    }

    public void buildFoundation(Vector3 startPos, Vector3 endPos)
    {
        //resize cube to be foundation
        Vector3 spawnPoint = (startPos + endPos) / 2;
        spawnPoint.y = startPos.y;
        float xlen = Mathf.Abs(startPos.x - endPos.x);
        float zlen = Mathf.Abs(startPos.z - endPos.z);
        GameObject foundation = GameObject.CreatePrimitive(PrimitiveType.Cube);
        foundation.transform.localScale = new Vector3(xlen, 0.01f, zlen);
        foundation.transform.position = spawnPoint;
        foundation.tag = "Foundation";

        //auto generate walls for foundation
        Vector3 bottomLeftCorner = new Vector3(spawnPoint.x - xlen / 2, startPos.y, spawnPoint.z - zlen / 2);
        for (int i = 0; i < xlen; i++)
        {
            Vector3 bottomWallPos = new Vector3(bottomLeftCorner.x + i, bottomLeftCorner.y, bottomLeftCorner.z);
            Vector3 topWallPos = new Vector3(bottomLeftCorner.x + i, bottomLeftCorner.y, bottomLeftCorner.z + zlen  -1);
            placeObject(bottomWallPos);
            placeObject(topWallPos);
        }
        
        for (int i = 1; i < zlen; i++)
        {
            Vector3 leftWallPos = new Vector3(bottomLeftCorner.x, bottomLeftCorner.y, bottomLeftCorner.z + i);
            Vector3 rightWallPos = new Vector3(bottomLeftCorner.x + xlen - 1, bottomLeftCorner.y, bottomLeftCorner.z + i);
            placeObject(leftWallPos);
            placeObject(rightWallPos);
        }
        
    }

    /**
     * Will return the bottom corner of the terrain "tile" at that point. The y component of the vector will be the 
     * height of the terrain regardless of what's placed on top of it.
     */
    public Vector3 getTileCoordFromMousePos(Vector2 mousePos)
    {
        Vector3 castPos = Camera.main.ScreenToWorldPoint(mousePos); //y is height of camera
        //note that the int 3 (00000101) is not the same as the layermask 3 (00000100) so that's why you can't just do LayerMask mask = 3;
        LayerMask mask = LayerMask.GetMask("Terrain");
        RaycastHit hit;
        Physics.Raycast(castPos, Vector3.down, out hit, castPos.y + 1f, mask);
        Vector3 tileCoord = new Vector3(Mathf.Floor(castPos.x), hit.point.y, Mathf.Floor(castPos.z));
        return tileCoord;
    }

}