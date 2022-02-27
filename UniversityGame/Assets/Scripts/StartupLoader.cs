using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Central script for managing all the different functions that might need to happen at the start of the game such as generating terrain, loading savedata, placing trees, generating navmesh, and stuff like that. 
 */
public class StartupLoader : MonoBehaviour
{
    TerrainGenerator terrainGenerator;

    void Start()
    {
        terrainGenerator = FindObjectOfType<TerrainGenerator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
