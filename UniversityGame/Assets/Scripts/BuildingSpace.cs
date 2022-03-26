using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * represents a space in the world that can be built on. each building space can have have multiple "floors" on which
 * tiles can be placed
 */
public class BuildingSpace
{
    public string buildingName; //building this space is a part of
    /**
     * list of tiles that could exist in any space. essentially represents the different "floors".
     */
    public List<Tile> tiles = new List<Tile>();
    /**
     * only four sprites need to be rendered for building spot (floor, base, wall, ceiling). These four sprites are 
     * updated when the floor being viewed changes or when the tile data changes. 
     */
    public GameObject[] sprites = new GameObject[4]; 

    public BuildingSpace(Tile tile)
    {
        this.tiles.Add(tile);
        buildingName = "Unnamed Building";
    }
}

public class Tile
{ 
    public int[] objects; //array of object ids
    /**
     * having an array of gameObject references for each tile is probably not a good idea. later we might want to look
     * into storing this geometry holistically as opposed to on a per-tile basis and, as such, the process for 
     * generating, storing, and modifying this data will undoutably get more complex especially when factoring in 
     * other optimizations that might get made. right now i'm behind schedule though so i'm going to do things this 
     * way and then fix it later when it inevitably causes performance problems. 
     */
    //TODO: change this
    public GameObject[] geometry;
    public Tile(int floor)
    {
        objects = new int[4]; //4 "layers" (floor, base, wall, ceiling) 
        this.objects[0] = floor;
        this.geometry = new GameObject[4];
    }
}