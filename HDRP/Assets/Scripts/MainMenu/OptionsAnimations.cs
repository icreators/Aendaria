using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsAnimations : MonoBehaviour {

    public int AnimationSlideSpeed = 10;

    [SerializeField] private Image GameCanvas;
    [SerializeField] private Image GraphicsCanvas;
    [SerializeField] private Image SoundCanvas;
    [SerializeField] private Image ControlsCanvas;

    private bool GameCanvasAnimation = false;
    private bool GameCanvasActive = false;

    private bool GraphicsCanvasAnimation = false;
    private bool GraphicsCanvasActive = false;

    private bool SoundCanvasAnimation = false;
    private bool SoundCanvasActive = false;

    private bool ControlsCanvasAnimation = false;
    private bool ControlsCanvasActive = false;

    private int GameCanvasPosition = -100;
    private int GraphicsCanvasPosition = -100;
    private int SoundCanvasPosition = -100;
    private int ControlsCanvasPosition = -100;

    public void Start()
    {
       GameCanvas.gameObject.SetActive(false);
       GraphicsCanvas.gameObject.SetActive(false);
       SoundCanvas.gameObject.SetActive(false);
       ControlsCanvas.gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (GameCanvasAnimation && GameCanvasPosition < Screen.width / 1.4)
        {
            GameCanvas.rectTransform.position = new Vector3(GameCanvasPosition, Screen.height / 2);

            GameCanvasPosition += AnimationSlideSpeed;
        }
        else if (GameCanvasPosition >= Screen.width / 1.4)
        {
            GameCanvasAnimation = false;
        }

        if (GraphicsCanvasAnimation && GraphicsCanvasPosition < Screen.width / 1.4)
        {
            GraphicsCanvas.rectTransform.position = new Vector3(GraphicsCanvasPosition, Screen.height / 2);

            GraphicsCanvasPosition += AnimationSlideSpeed;
        }
        else if (GraphicsCanvasPosition >= Screen.width / 1.4)
        {
            GraphicsCanvasAnimation = false;
        }

        if (SoundCanvasAnimation && SoundCanvasPosition < Screen.width / 1.4)
        {
            SoundCanvas.rectTransform.position = new Vector3(SoundCanvasPosition, Screen.height / 2);

            SoundCanvasPosition += AnimationSlideSpeed;
        }
        else if (SoundCanvasPosition >= Screen.width / 1.4)
        {
            SoundCanvasAnimation = false;
        }

        if (ControlsCanvasAnimation && ControlsCanvasPosition < Screen.width / 1.4)
        {
            ControlsCanvas.rectTransform.position = new Vector3(ControlsCanvasPosition, Screen.height / 2);

            ControlsCanvasPosition += AnimationSlideSpeed;
        }
        else if (ControlsCanvasPosition >= Screen.width / 1.4)
        {
            ControlsCanvasAnimation = false;
        }


    }

    public void CanvasGameAnimation()
    {
        if (!GameCanvasActive)
        {
            GraphicsCanvasActive = false;
            GraphicsCanvas.gameObject.SetActive(false);
            GraphicsCanvasAnimation = false;

            SoundCanvasActive = false;
            SoundCanvas.gameObject.SetActive(false);
            SoundCanvasAnimation = false;

            ControlsCanvasActive = false;
            ControlsCanvas.gameObject.SetActive(false);
            ControlsCanvasAnimation = false;

            GameCanvasPosition = -100;

            GameCanvasActive = true;

            GameCanvas.gameObject.SetActive(true);

            GameCanvas.rectTransform.position = new Vector3(GameCanvasPosition, Screen.height / 2);

            GameCanvasAnimation = true;
        }

    }

    public void CanvasGraphicsAnimation()
    {
        if (!GraphicsCanvasActive)
        {
            GameCanvasActive = false;
            GameCanvas.gameObject.SetActive(false);
            GameCanvasAnimation = false;

            SoundCanvasActive = false;
            SoundCanvas.gameObject.SetActive(false);
            SoundCanvasAnimation = false;

            ControlsCanvasActive = false;
            ControlsCanvas.gameObject.SetActive(false);
            ControlsCanvasAnimation = false;

            GraphicsCanvasPosition = -100;

            GraphicsCanvasActive = true;

            GraphicsCanvas.gameObject.SetActive(true);

            GraphicsCanvas.rectTransform.position = new Vector3(GraphicsCanvasPosition, Screen.height / 2);

            GraphicsCanvasAnimation = true;
        }

    }

    public void CanvasSoundAnimation()
    {
        if (!SoundCanvasActive)
        {
            GameCanvasActive = false;
            GameCanvas.gameObject.SetActive(false);
            GameCanvasAnimation = false;

            GraphicsCanvasActive = false;
            GraphicsCanvas.gameObject.SetActive(false);
            GraphicsCanvasAnimation = false;

            ControlsCanvasActive = false;
            ControlsCanvas.gameObject.SetActive(false);
            ControlsCanvasAnimation = false;

            SoundCanvasPosition = -100;

            SoundCanvasActive = true;

            SoundCanvas.gameObject.SetActive(true);

            SoundCanvas.rectTransform.position = new Vector3(SoundCanvasPosition, Screen.height / 2);

            SoundCanvasAnimation = true;
        }

    }

    public void CanvasControlsAnimation()
    {
        if (!ControlsCanvasActive)
        {
            GameCanvasActive = false;
            GameCanvas.gameObject.SetActive(false);
            GameCanvasAnimation = false;

            GraphicsCanvasActive = false;
            GraphicsCanvas.gameObject.SetActive(false);
            GraphicsCanvasAnimation = false;

            SoundCanvasActive = false;
            SoundCanvas.gameObject.SetActive(false);
            SoundCanvasAnimation = false;

            ControlsCanvasPosition = -100;

            ControlsCanvasActive = true;

            ControlsCanvas.gameObject.SetActive(true);

            ControlsCanvas.rectTransform.position = new Vector3(ControlsCanvasPosition, Screen.height / 2);

            ControlsCanvasAnimation = true;
        }

    }

}
