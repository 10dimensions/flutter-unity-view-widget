using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotate2 : MonoBehaviour, IEventSystemHandler
{   
    [SerializeField] Vector3 RotateAmount;
    [SerializeField] float RotateValue;
    [SerializeField] bool LightOn;


    [SerializeField] private Light Light1;
    [SerializeField] private GameObject Fan;

    // Start is called before the first frame update
    void Start()
    {
        RotateAmount = new Vector3(0, 0, 0);
        RotateValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Fan.transform.Rotate(new Vector3(0,RotateValue,0) * Time.deltaTime * 1000);   

        if(LightOn)
            Light1.range = RotateValue*80f;
        else
            Light1.range = 0f;
    }

    // This method is called from Flutter
    public void SetRotationSpeed(String message)
    {
        float value = float.Parse(message);
        RotateValue = value;
        //RotateAmount = new Vector3(value, value, value);
    }

    // This method is called from Unity
    public void SwitchLightOn(String message)
    {
        LightOn = ( message == "true");
    }

    // This method is called from Unity
    public void SetRotationVelocity(float message)
    {
        RotateValue = message;
    }

    // This method is called from Unity
    public void TurnLightOn(bool message)
    {
        LightOn = message;
        
    }

}
