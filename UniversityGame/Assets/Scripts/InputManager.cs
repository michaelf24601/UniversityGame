using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private int selectedObject = -1;
    private BuildManager buildManager; //reference to build manager

    private enum Instruction
    {
        None,
        BuildFoundation,
        BuildWall,
        Delete
    }
    private Instruction currentInstruction = Instruction.None; //the current thing that the player has selected that they want to do
    private int currentlySelectedObject = -1;

    private Vector3 startDragPos = Vector3.zero;
    private bool isDragging = false;

    private void Start()
    {
        buildManager = FindObjectOfType<BuildManager>();
    }

    //onclick functions
    public void foundation()
    {
        currentInstruction = Instruction.BuildFoundation;
    }
    public void wall()
    {
        currentInstruction = Instruction.BuildWall;
    }
    public void delete()
    {
        currentInstruction = Instruction.Delete;
    }

    private void Update()
    {
        Debug.Log(buildManager.getTileCoordFromMousePos(Input.mousePosition));
        if (Input.GetMouseButtonDown(0))
        {
            //check if over ui element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
                return;
            }
            //if execution has gotten here then the player has clicked somewhere where an object could be placed
            if (currentInstruction == Instruction.BuildWall)
            {
                Debug.Log("player is trying to place a wall");
                Vector3 tilePos = buildManager.getTileCoordFromMousePos(Input.mousePosition);
                if (buildManager.isPlacableLocation(tilePos))
                {
                    buildManager.placeObject(tilePos);
                } 
                else
                {
                    Debug.Log("Can't place tile there");
                }
            }
            if (currentInstruction == Instruction.BuildFoundation)
            {
                if (!isDragging)
                {
                    isDragging = true;
                    startDragPos = buildManager.getTileCoordFromMousePos(Input.mousePosition);
                }
            }
            if (currentInstruction == Instruction.Delete)
            {
                Debug.Log("player is trying to delete an object");
                Vector3 tilePos = buildManager.getTileCoordFromMousePos(Input.mousePosition);
                buildManager.deleteObject(tilePos);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                Debug.Log("draggable area selected");
                Vector3 endDragPos = buildManager.getTileCoordFromMousePos(Input.mousePosition);
                buildManager.buildFoundation(startDragPos, endDragPos);
                isDragging = false;
            }
        }
        if (isDragging)
        {
            //preview
        }
    }
}
