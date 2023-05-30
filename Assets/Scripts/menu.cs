using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    public bool SoundOn = true;
    public GameObject Audio;
    public GameObject button;
    public Sprite SoundON;
    public Sprite SoundOFF;
    public void Play_()
    {

        SceneManager.LoadScene("MainGameAR_ARDK");
    }

    public void Sound_()
    {
        SoundOn = !SoundOn;
        Audio.GetComponent<AudioSource>().enabled = SoundOn;
        button.GetComponent<Image>().sprite = SoundOn ?SoundON:SoundOFF;
        
    }
    public void Quit_()
    {
        Application.Quit();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
