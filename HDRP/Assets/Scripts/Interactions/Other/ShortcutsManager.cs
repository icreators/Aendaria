using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WindowsInput;


public class ShortcutsManager : MonoBehaviour
{
    enum ShortcutChoise { Nothing, Inventory, Skills, Quests, WorldMap, GetSword, PutDownSword };

    ShortcutChoise shortcutChoise = ShortcutChoise.Nothing;

    Image[] shortcutsBackgrounds = new Image[4];

    Canvas shortcutInterface;

    InputSimulator IS;

    private void Start()
    {
        IS = new InputSimulator();

        GameObject.Find("Interfaces/Shortcuts").SetActive(true);
        shortcutInterface = GameObject.Find("Interfaces/Shortcuts").GetComponent<Canvas>();

        shortcutInterface.enabled = false;

        shortcutsBackgrounds[0] = GameObject.Find("Inventory/TBackground").GetComponent<Image>();
        shortcutsBackgrounds[1] = GameObject.Find("Skills/RBackground").GetComponent<Image>();
        shortcutsBackgrounds[2] = GameObject.Find("Quests/BBackground").GetComponent<Image>();
        shortcutsBackgrounds[3] = GameObject.Find("WorldMap/LBackground").GetComponent<Image>();

        foreach (Image img in shortcutsBackgrounds)
        {
            img.color = new Color(0, 0, 0, 0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !InterfaceManager.instance.isAnyActiveInterface)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;

            shortcutInterface.enabled = true;

            InterfaceManager.instance.isAnyActiveInterface = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab) && shortcutInterface.isActiveAndEnabled)
        {
            shortcutInterface.enabled = false;

            InterfaceManager.instance.isAnyActiveInterface = false;

            Debug.Log("Wybrałeś: " + shortcutChoise);

            switch ((int)shortcutChoise)
            {
                case 1: transform.GetComponent<InventoryUI>().OpenInventory();
                    break;
                // Dodac reszte interfejsow
            }
        }
    }

    public void execute(int a)
    {
        if(a==2)
            IS.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_P);
        if(a==3)
            IS.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_Q);
        if(a==4)
            IS.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_M);  
        
    }
    public void OnBackgroundEnter(int scChouise)
    {
        shortcutChoise = (ShortcutChoise)scChouise;

        shortcutsBackgrounds[scChouise-1].color = new Color(255, 255, 255, .3f);

        execute(scChouise);
    }
    public void OnBackgroundExit(int scChouise)
    {
        shortcutsBackgrounds[scChouise - 1].color = new Color(255, 255, 255, 0);

        shortcutChoise = ShortcutChoise.Nothing;
        
        execute(scChouise);
    }

    public void GetSword(bool putDown = false)
    {
        if (putDown) shortcutChoise = ShortcutChoise.PutDownSword;
        else shortcutChoise = ShortcutChoise.GetSword;
    }

    public void PutDownSword()
    {
        shortcutChoise = ShortcutChoise.Nothing;
    }
}
