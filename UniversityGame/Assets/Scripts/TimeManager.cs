using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public int timeState;
    public Light sun;

    [Header("Time")]
    public int days;
    public int hours;
    public int minutes;

    [Header("Skybox Materials")]
    public Material dayNoon;
    public Material dayMorning;
    public Material dayEvening;
    public Material Night;

    [Header("UI Elements")]
    public Button fast;
    public Button superFast;
    public Button pause;
    public Button play;
    public Text clock;
    public Text calender;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        sun = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            gameUpdate();
            
            timer = 10;
        }
        else
        {
            switch (timeState)
            {
                case 0: timer -= 0; 
                    break;
                case 1: timer -= 50 * Time.deltaTime;
                    break;
                case 2: timer -= 200 * Time.deltaTime;
                    break;
                case 3:
                    timer -= 2000 * Time.deltaTime;
                    break;
            }
        }
    }
    void gameUpdate()   //When Timer == 0 this activates
    {
        clockChecker();
        if(hours > 1 && hours < 10) //updates skybox and sun
        {
            RenderSettings.skybox = dayNoon;
            sun.enabled = true;
        }
        else if (hours < 1 || hours > 23)
        {
            RenderSettings.skybox = dayMorning;
            sun.enabled = true;
        }
        else if (hours < 11 && hours > 10)
        {
            RenderSettings.skybox = dayEvening;
            sun.enabled = true;
        }
        else if (hours > 11 && hours < 22)
        {
            RenderSettings.skybox = Night;
            sun.enabled = false;
        }
        if (minutes == 60)
        {
            hours++;
            minutes = 0;
        }
        if (hours == 24)
        {
            days++;
            hours = 0;
        }
        minutes++;
        transform.Rotate(0.25f, 0, 0);
    }
    //UI Button Methods
    public void pressPlay()
    {
        timeState = 1;
        pause.interactable = true;
        fast.interactable = true;
        play.interactable = false;
        superFast.interactable = true;
    }
    public void pressPause()
    {
        timeState = 0;
        pause.interactable = false;
        fast.interactable = true;
        play.interactable = true;
        superFast.interactable = true;
    }
    public void pressFast()
    {
        timeState = 2;
        pause.interactable = true;
        fast.interactable = false;
        play.interactable = true;
        superFast.interactable = true;
    }
    public void pressSuperFast()
    {
        timeState = 3;
        pause.interactable = true;
        fast.interactable = true;
        play.interactable = true;
        superFast.interactable = false;
    }

    //Updates Clock and Calender UI Elements
    public void clockChecker()
    {
        if (minutes < 10)
        {
          
            if (hours < 10)
            {
                clock.text = "0" + hours + ":0" + minutes;
            }
            else
            {
                clock.text = hours + ":0" + minutes;
            }
        }
        else
        {
            if (hours < 10)
            {
                clock.text = "0" + hours + ":" + minutes;
            }
            else
            {
                clock.text = hours + ":" + minutes;
            }
        }
        calender.text = "Day " + days;
    }
}
