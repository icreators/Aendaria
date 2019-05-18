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

    private void Start()
    {
        isAnyActiveInterface = false;
    }
}
