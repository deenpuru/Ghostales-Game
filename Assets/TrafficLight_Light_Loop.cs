using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TrafficLight_Light_Loop : MonoBehaviour
{
    [SerializeField] Light2D _redLight;
    [SerializeField] Light2D _yellowLight;
    [SerializeField] Light2D _blueLight;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightSwitching());
    }

    private IEnumerator LightSwitching()
    {
        while (true)
        {
            _yellowLight.enabled = false;
            _blueLight.enabled = false;
            _redLight.enabled = true;
            Debug.Log("on");
            yield return new WaitForSeconds(4);
            Debug.Log("off");
            _redLight.enabled = false;

            _yellowLight.enabled = true;
            yield return new WaitForSeconds(3);
            _yellowLight.enabled = false;

            _blueLight.enabled = true;
            yield return new WaitForSeconds(5);
            _blueLight.enabled = false;
        }
            
    }
}

