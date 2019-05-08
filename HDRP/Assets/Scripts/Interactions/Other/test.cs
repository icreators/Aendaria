using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public CollisionChecker colChecker;

    public void AttackEvent()
    {
        colChecker.Check();
    }
}
