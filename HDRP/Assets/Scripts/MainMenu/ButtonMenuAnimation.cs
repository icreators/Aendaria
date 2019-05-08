using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ButtonMenuAnimation : MonoBehaviour {

    /* Variables */

    public int animationSlide;
    public float animationAlphaSpeed;
    public float maxTransition;

    public Vector3 animationSlideSpeed;


    [SerializeField] private Image Background;


    private int optionsPosX = 0;
    private float alphaLevel;
    private bool slideOn = false;


    void Start()
    {
        Background.enabled = true;
        alphaLevel = 0;

        Background.GetComponent<Image>().color = new Color(255, 255, 255, alphaLevel); // Set invisibility on start
    }

    private void FixedUpdate()
    {
        if (!slideOn) // If cursor is avoid button
        {
            if (optionsPosX > 0)
            {
                //Debug.Log("SlideOFF");
                optionsPosX--;
                this.transform.GetChild(0).Translate(-animationSlideSpeed);
            }

            //Background.enabled = false;

            if (alphaLevel > 0)
            {
                alphaLevel -= animationAlphaSpeed;

                Background.GetComponent<Image>().color = new Color(255, 255, 255, alphaLevel);
            }

        }

        else if (slideOn) // If cursor is on the button
        {
            if (optionsPosX != animationSlide)
            {
                optionsPosX++;
                this.transform.GetChild(0).Translate(animationSlideSpeed);
            }

            //Debug.Log("SlideON");

            //Background.enabled = true;

            if (alphaLevel < maxTransition)
            {
                alphaLevel += animationAlphaSpeed;

                Background.GetComponent<Image>().color = new Color(255, 255, 255, alphaLevel);
            }


        }


    }




    public void AnimationSlideStart()
    {
        //Debug.Log("Slideeee!");
        slideOn = true;
    }

    public void AnimationSlideOut()
    {
        //Debug.Log("Noslide:(");
        slideOn = false;
    }
}
