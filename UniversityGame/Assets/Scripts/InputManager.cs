using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private int selectedObject = -1;
    private BuildManager buildManager; //reference to build manager

    private enum Instruction
    {
        BuildFoundation,
        BuildWall,
        Delete
    }
    private Instruction currentInstruction; //the current thing that the player has selected that they want to do
    private int currentlySelectedObject = -1;

    private void Start()
    {
        buildManager = FindObjectOfType<BuildManager>();
    }

    //onclick functions
    public void foundation()
    {
        print("player selected foundation");
    }
    public void wall()
    {
        print("player selected wall");
    }
    public void delete()
    {
        print("player selected delete");
    }

    //listen for mouse input

    //onclick, check current instruction and then query the build manager accordingly 

    //in update, preview object placement if the current instruction calls for it
}
