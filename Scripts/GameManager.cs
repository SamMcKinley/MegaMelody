using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool songCompleted = false;
    public bool fadeStarted = false;
    public Animator fadeAnimator;
    public AudioSource odeToJoy;
    public AudioSource odeToJoyComplete;
    public GameObject layoutGroup;
    public List<MeasureData> correctMeasures = new List<MeasureData>();
    public List<MeasureData> incorrectMeasures = new List<MeasureData>();
    public int currentIndex;
    public List<MeasureData> currentMeasures = new List<MeasureData>();
    public GameObject measurePrefab;
    public List<Sprite> musicSheets = new List<Sprite>();
    public Image sheetMusic;
   

    public void GetMeasures()
    {
        if(layoutGroup.transform.childCount > 0)
        {
            Destroy(layoutGroup.transform.GetChild(0).gameObject);
            Destroy(layoutGroup.transform.GetChild(1).gameObject);
            Destroy(layoutGroup.transform.GetChild(2).gameObject);
        }
        
        
        
        //Clear the existing measures
        currentMeasures.Clear();
        //Get the next correct measure
        if(currentIndex < correctMeasures.Count)
        {
            currentMeasures.Add(correctMeasures[currentIndex]);
        }
        
        //Get two incorrect measures
        if(incorrectMeasures.Count > 1)
        {
            int firstIncorrectMeasure = 0;
            int secondIncorrectMeasure = 0;
            while(firstIncorrectMeasure == secondIncorrectMeasure)
            {
                firstIncorrectMeasure = Random.Range(0, incorrectMeasures.Count);
                secondIncorrectMeasure = Random.Range(0, incorrectMeasures.Count);
            }
            currentMeasures.Add(incorrectMeasures[firstIncorrectMeasure]);
            currentMeasures.Add(incorrectMeasures[secondIncorrectMeasure]);
        }
        //Randomize the order of the three measures
        List<MeasureData> shuffledList = currentMeasures.OrderBy(x => Random.value).ToList();
        currentMeasures = shuffledList;
        //Create the measures as a child of the game object that does the horizontal layout
        foreach(MeasureData measureData in currentMeasures)
        {
            GameObject newMeasure = Instantiate(measurePrefab, layoutGroup.transform);
            newMeasure.GetComponent<Measure>().Data = measureData;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        odeToJoy.Play();
        GetMeasures();
    }

    // Update is called once per frame
    void Update()
    {
        if(!odeToJoy.isPlaying && !fadeStarted)
        {
            fadeAnimator.SetTrigger("Fade");
            fadeStarted = true;
        }
    }

    public void winGame()
    {
        Destroy(layoutGroup.transform.GetChild(0).gameObject);
        Destroy(layoutGroup.transform.GetChild(1).gameObject);
        Destroy(layoutGroup.transform.GetChild(2).gameObject);
        //Change the conductor
        //play the full orchestra version of ode to joy
        odeToJoyComplete.Play();
    }
}
