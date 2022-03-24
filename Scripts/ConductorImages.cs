using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConductorImages : MonoBehaviour
{
    public Image RenderingImage;
    public Sprite RightAnswer;
    public Sprite WrongAnswer;
    public Sprite Undecided;
    
    public void gotRightAnswer()
    {
        RenderingImage.sprite = RightAnswer;
        StartCoroutine(ResetSprite());
    }

    public void gotWrongAnswer()
    {
        RenderingImage.sprite = WrongAnswer;
        StartCoroutine(ResetSprite());
    }

    IEnumerator ResetSprite()
    {
        yield return new WaitForSeconds(5);
        RenderingImage.sprite = Undecided;
    }
    // Start is called before the first frame update
    void Start()
    {
        RenderingImage = GetComponent<Image>();
        RenderingImage.sprite = Undecided;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
