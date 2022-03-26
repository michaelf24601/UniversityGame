using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeManager : MonoBehaviour
{
    public GameObject buildModeUI;

    private CameraController cameraController;

    private void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) //b for build mode. probably want to use a different input system later
        {
            if (cameraController.currentState == CameraController.State.Transition) return; 
            if (cameraController.currentState == CameraController.State.BuildMode)
            {
                //set camera controller state to appropriate input setting and move the camera
                cameraController.currentState = CameraController.State.Normal; //skip transition for now
                transitionCameraToNormalMode();

                //de-activate the build mode UI
                buildModeUI.SetActive(false);
            } else if (cameraController.currentState == CameraController.State.Normal)
            {
                //change camera position and camera controller input settings
                cameraController.currentState = CameraController.State.BuildMode; //skip transition for now
                transitionCameraToBuildMode();

                //activate the input manager
                buildModeUI.gameObject.SetActive(true);
            }
        }
    }

    private void transitionCameraToBuildMode()
    {
        cameraController.transform.rotation = Quaternion.Euler(90, 0, 0);
        cameraController.gameObject.GetComponent<Camera>().orthographic = true;
    }

    private void transitionCameraToNormalMode()
    {
        cameraController.transform.rotation = Quaternion.Euler(30, 0, 0);
        Camera cam = cameraController.gameObject.GetComponent<Camera>();
        cam.orthographic = false;
        cam.orthographicSize = 15;
    }
}
