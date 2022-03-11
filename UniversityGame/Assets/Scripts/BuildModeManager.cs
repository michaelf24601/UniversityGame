using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeManager : MonoBehaviour
{
    private CameraController cameraController;
    bool buildMode = false;

    private void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) //lets say b for build mode. probably want to use a different input system later
        {
            if (cameraController.currentState == CameraController.State.Transition) return;
            //change camera controler to transitionary state to disable movement input
            //lerp the camera rotation to vertical
            //lerp the fustrum to orthographic 
            //change camera controler to build mode to use build mode movement
            if (buildMode)
            {
                cameraController.currentState = CameraController.State.Normal;
                cameraController.transform.rotation = Quaternion.Euler(30, 0, 0);
                cameraController.gameObject.GetComponent<Camera>().orthographic = false;
                Camera.main.orthographicSize = 5;
                buildMode = false;
            } else
            {
                cameraController.currentState = CameraController.State.BuildMode;
                cameraController.transform.rotation = Quaternion.Euler(90, 0, 0);
                cameraController.gameObject.GetComponent<Camera>().orthographic = true;
                buildMode = true;
            }
            
        }
    }
}
