using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light lightObject;
    void Start()
    {
        
    }

    void Update()
    {
        //turn on if player click "F" button
        if (Input.GetKeyDown(KeyCode.F))
        {
            TurnFlashLight();
        }
    }

    private void TurnFlashLight()
    {
        //turn on when is off and turn off when is on
        lightObject.enabled = !lightObject.enabled;


    }
}
