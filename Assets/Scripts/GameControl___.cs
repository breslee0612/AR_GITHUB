using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControl___ : MonoBehaviour
{
    bool start;
    bool start_timer = false;
    public Button button;
    public TMP_Text countdown;
    float timer = 3.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void start_game()
    {
        BotController BotStart = BotController.FindObjectOfType<BotController>();
        start = BotStart.CanStart;
        if (BotStart.CanStart == false)
        {
            start_timer = true;

        }
        else
        {
            BotStart.CanStart = !BotStart.CanStart;
            button.GetComponentInChildren<TextMeshProUGUI>().text = start ?
                "START" : "PAUSE";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        BotController BotStart = BotController.FindObjectOfType<BotController>();
        CarController CarStart = CarController.FindObjectOfType<CarController>();
        if (BotStart.laps >= 3)
        {
            countdown.text = "You Lose";
            BotStart.CanStart = false;
        }
        if (CarStart.laps >= 3)
        {
            countdown.text = "You Win!";
            BotStart.CanStart = false;
        }
        if (start_timer)
        {
            
            timer = timer - Time.deltaTime;
            if (timer > 2.5f)
            {
                countdown.text = "3";
            }
            else if (timer > 1.5f)
            {
                countdown.text = "2";
            }
            else if (timer > 0.5f)
            {
                countdown.text = "1";
            }
            else if (timer > 0)
            {
                countdown.text = "GO!";
            }
            else
            {
                countdown.text = " ";
                timer = 3.5f;
                start_timer = false;
                BotStart.CanStart = !BotStart.CanStart;
                button.GetComponentInChildren<TextMeshProUGUI>().text = start ?
                    "START" : "PAUSE";
            }
        }
    }
}