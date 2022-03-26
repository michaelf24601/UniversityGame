using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class in charge of building things. Has methods for placing objects, deleting objects, building and buldozing 
 * foundations, and just generally editing the building data. The public methods of this class will get called by the 
 * input sustem. 
 */
public class BuildManager : MonoBehaviour
{

    // the tilespace coordinate of a tile
    public struct Coord
    {

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }
    }
    
    //TODO: move this and the building data to some other serializable class and make a loading system
    public static GameObject[] objectTable;

    /**
     * Array of building space data which has tile data for multiple floors. This is what gets written to and loaded 
     * from a file to save and generate the buildings for the game. this data only needs to exist while in build view.
     */
    public static BuildingSpace[,] buildingData;

    private void Start()
    {
        TerrainGenerator tg = FindObjectOfType<TerrainGenerator>();
        int buildSpaceLength = tg.size * tg.meshLength; //chunk length * chunk size
        buildingData = new BuildingSpace[buildSpaceLength, buildSpaceLength];
    }

    /**
     * updates the tiles data to reflect changes
     */
    public int placeObject(int objectID, Coord coord, int floor, int layer)
    {
        //check build data to see if it is possible to place an object
        //update buildData to add object
        //update sprite layer to render new object
        //generate 3d representation of object at calculated location
        return 0;
    }
    public void generateObjectGeometry(int objectID, Coord coord, int floor, int layer)
    {
        //generates 3d geometry for an object
    }
    public bool isPlacableTile(Coord coord, int floor, int layer)
    {
        //  is object id valid, is floor valid, is layer already occupied
        return true;
    }
    /**
     * translates the worldspace coordinates of, presumably, the mouse pointer, to the tilespace coordinates of the 
     * tile at that point.
     */
    public Coord worldToTilespace(float x, float y)
    {
        return new Coord((int)x, (int)y); //i think this is good for now
    }
}