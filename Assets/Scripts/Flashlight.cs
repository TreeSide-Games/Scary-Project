using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private const int maxEnergyLevel = 20;
    [SerializeField] Light lightObject;
    [SerializeField] private int energyLevel;
    private bool isBinking = false;
    void Start()
    {
        energyLevel = maxEnergyLevel;
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
        //if flashlight is out of energy then light is off
        if(energyLevel<=0)
        {
            lightObject.enabled = false;
            return;
        }

        //turn off light when is on elsewere turn it on
        if (lightObject.enabled)
        {
            lightObject.enabled = false;

        }
        else
        {
            lightObject.enabled = true;

            //take energy level down
            StartCoroutine(EnergyDrainCoroutine());
        }


    }

    IEnumerator EnergyDrainCoroutine()
    {
        //drain energy when light is on
        while (lightObject.enabled)
        {
            //if energy in flashlights is not 0 then drain that energy elsewere turn off light
            if(energyLevel > 0)
            {
                energyLevel--;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                lightObject.enabled = false;
                break;
            }

            //if flashlight is not blinking and energy is low then start blinking
            if(!isBinking && energyLevel <= 10)
            {
                StartCoroutine(LightBlinkingEffect());
            }
        }
    }

    IEnumerator LightBlinkingEffect()
    {
        isBinking = true;
        var basicLightIntensity = lightObject.intensity;
        //blink with random intensity of light when light is on
        while (lightObject.enabled)
        {
            var intensity = UnityEngine.Random.Range(0,basicLightIntensity);
            lightObject.intensity = intensity;
            yield return new WaitForSeconds(intensity/5);
        }
        lightObject.intensity = basicLightIntensity;
        isBinking = false;

    }
}
