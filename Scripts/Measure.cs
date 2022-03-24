using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Measure : MonoBehaviour
{
    public Image measureImage;
    private MeasureData measure;
    public AudioSource Source;
    public bool wasClicked;
    private ConductorImages conductorReference;
    public MeasureData Data
    {
        get
        {
            return measure;
        }

        set
        {
            measure = value;
            Source.clip = measure.music;
            measureImage.sprite = measure.sprite;
        }
    }



    // Start is called before the first frame update
    void Awake()
    {
        measureImage = GetComponent<Image>();
        Source = GetComponent<AudioSource>();
        conductorReference = FindObjectOfType<ConductorImages>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void PlayMusic()
    {
        Source.Play();
    }

    public void handleButtonClicked()
    {
        if (wasClicked)
        {
            //This handles the second time the button was clicked
            if (measure.isCorrect)
            {
                conductorReference.gotRightAnswer();
                GameManager.instance.currentIndex++;
                GameManager.instance.sheetMusic.sprite = GameManager.instance.musicSheets[GameManager.instance.currentIndex];
                if(GameManager.instance.currentIndex < GameManager.instance.correctMeasures.Count)
                {
                    GameManager.instance.GetMeasures();
                }
                else
                {
                    GameManager.instance.winGame();
                }
                
            }
            else
            {
                conductorReference.gotWrongAnswer();
            }
        }
        else
        {
            //This handles the first time the button was clicked
            wasClicked = true;
            PlayMusic();
        }
    }
}
