using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeManager : MonoBehaviour
{
    public bool buildModeOn;
    public GameObject cameraa;
    public CameraController controller;
    public Material skyboxNormal;
    public Material skyboxBuild;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if (buildModeOn == false)
            {
                if(cameraa.transform.localPosition != new Vector3(0, 5, -10))
                {
                    cameraa.transform.localPosition -= new Vector3(0, 0, Time.deltaTime*20);
                }
                if (cameraa.transform.localEulerAngles != new Vector3(30, 0, 0))
                {
                    cameraa.transform.localEulerAngles -= new Vector3(Time.deltaTime * 120, 0, 0);
                }
                cameraa.GetComponent<Camera>().fieldOfView += Time.deltaTime * 40;
            }
            if (buildModeOn == true)
            {
                if (cameraa.transform.localPosition != new Vector3(0, 5, 0))
                {
                    cameraa.transform.localPosition += new Vector3(0, 0, Time.deltaTime * 20);
                }
                if (cameraa.transform.localEulerAngles != new Vector3(90, 0, 0))
                {
                    cameraa.transform.localEulerAngles += new Vector3(Time.deltaTime * 120, 0, 0);
                }
                cameraa.GetComponent<Camera>().fieldOfView -= Time.deltaTime * 40;

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                buildModeOn = !buildModeOn;
                timer = 0.5f;
            }
            else
            {
                if (buildModeOn == true)
                {
                    cameraa.transform.localPosition = new Vector3(0, 5, 0);
                    cameraa.transform.localEulerAngles = new Vector3(90, 0, 0);
                    controller.buildmode = true;
                    RenderSettings.skybox = skyboxBuild;
                    cameraa.GetComponent<Camera>().fieldOfView = 60;
                }
                if (buildModeOn == false)
                {
                    cameraa.transform.localPosition = new Vector3(0, 5, -10);
                    cameraa.transform.localEulerAngles = new Vector3(30, 0, 0);
                    controller.buildmode = false;
                    RenderSettings.skybox = skyboxNormal;
                    cameraa.GetComponent<Camera>().fieldOfView = 80;

                }
            }
            

        }
        
    }
}
