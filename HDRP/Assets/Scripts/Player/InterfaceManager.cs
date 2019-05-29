using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    #region Singleton

    public static InterfaceManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public bool isAnyActiveInterface;
    public bool DrawSword;

    private void Start()
    {
        isAnyActiveInterface = false;
        DrawSword = false;
    }
}
