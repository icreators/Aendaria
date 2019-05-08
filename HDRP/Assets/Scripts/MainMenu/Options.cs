using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public bool fpsShow = false;

    [SerializeField] private Toggle FpsOn;

    private float deltaTime = 0.0f;


    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }


    public void FpsButton()
    {
        fpsShow = !fpsShow;

        if (fpsShow) 
        {
            FpsOn.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "ON";

            FpsOn.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0, 0, 0);
        }
        else
        {
            FpsOn.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "OFF";

            FpsOn.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(255, 255, 255);
        }
           
    }

    // SHADOWN QUALITY

    [SerializeField] private Text QLText;

    private int QLV = 2;

    public void SQUP()
    {
        QLV ++;

        if (QLV > 2) QLV = 2;

        ChangeQLV();
    }

    public void SQDWN()
    {
        QLV --;

        if (QLV < 0) QLV = 0;

        ChangeQLV();
    }

    private void ChangeQLV()
    {
        switch(QLV)
        {
            case 0: QLText.text = "None"; break;
            case 1: QLText.text = "Low"; break;
            case 2: QLText.text = "Hight"; break;
            default: QLText.text = "Error no quality level find!"; break;
        }

        QualitySettings.shadows = (ShadowQuality)QLV;
    }

    // SHADOWS RESOLUTION

    [SerializeField] private Text QSRText;

    private int QSR = 3;

    public void QSRUP()
    {
        QSR++;

        if (QSR > 3) QSR = 3;

        ChangeSRes();
    }

    public void QSRDWN()
    {
        QSR--;

        if (QSR < 0) QSR = 0;

        ChangeSRes();
    }

    private void ChangeSRes()
    {
        switch (QSR)
        {
            case 0: QSRText.text = "Low"; break;
            case 1: QSRText.text = "Medium"; break;
            case 2: QSRText.text = "Hight"; break;
            case 3: QSRText.text = "Very Hight"; break;
            default: QSRText.text = "Error no quality level find!"; break;
        }

        QualitySettings.shadowResolution = (ShadowResolution)QSR;
    }

    // SHADOW DISTANCE

    [SerializeField] private Text QSDText;

    private int QSD = 3;

    private int QSDDistance = 256;

    public void QSDUP()
    {
        QSD++;

        if (QSD > 4) QSD = 4;

        ChangeSDis();
    }

    public void QSDDWN()
    {
        QSD--;

        if (QSD < 0) QSD = 0;

        ChangeSDis();
    }

    private void ChangeSDis()
    {

        switch (QSD)
        {
            case 0: QSDText.text = "Ur D1cK"; QSDDistance = 12;  break;
            case 1: QSDText.text = "Low"; QSDDistance = 64; break;
            case 2: QSDText.text = "Medium"; QSDDistance = 112; break;
            case 3: QSDText.text = "Hight"; QSDDistance = 256; break;
            case 4: QSDText.text = "Very Hight"; QSDDistance = 512; break;
            default: QSDText.text = "Error no quality level find!"; break;
        }

        QualitySettings.shadowDistance = QSDDistance;
    }

    // ANTYALIASING

    [SerializeField] private Text AntyText;

    private int Anty = 3;
    private int AntyA = 8;

    public void AntyUP()
    {
        Anty++;

        if (Anty > 4) Anty = 4;

        ChangeAnty();
    }

    public void AntyDWN()
    {
        Anty--;

        if (Anty < 0) Anty = 0;

        ChangeAnty();
    }

    private void ChangeAnty()
    {

        switch (Anty)
        {
            case 0: AntyText.text = "Disabled"; AntyA = 0; break;
            case 1: AntyText.text = "X2"; AntyA = 2; break;
            case 2: AntyText.text = "X4"; AntyA = 4; break;
            case 3: AntyText.text = "X8"; AntyA = 8; break;
            case 4: AntyText.text = "X16"; AntyA = 16; break;
            default: AntyText.text = "Error no quality level find!"; break;
        }

        QualitySettings.antiAliasing = AntyA;
    }





    void OnGUI()
    {
        if (fpsShow)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}
