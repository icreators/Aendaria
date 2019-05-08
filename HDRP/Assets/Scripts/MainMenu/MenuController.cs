using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    [SerializeField] private Canvas MainMenu;
    [SerializeField] private Canvas Options;
    [SerializeField] private Canvas Credits;

    private void Start()
    {
        MainMenu.gameObject.SetActive(true);
        Options.gameObject.SetActive(false);
        Credits.gameObject.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        Options.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
    }

    public void CloseSettings()
    {
        Options.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }

    public void OpenCredits()
    {
        MainMenu.gameObject.SetActive(false);
        Credits.gameObject.SetActive(true);
    }

    public void CloseCredits()
    {
        MainMenu.gameObject.SetActive(true);
        Credits.gameObject.SetActive(false);
    }

    public void AppQuit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
