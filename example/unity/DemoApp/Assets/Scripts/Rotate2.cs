using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotate2 : MonoBehaviour, IEventSystemHandler
{   
    [SerializeField] private GameObject Devices;
    [SerializeField] private float[] DeviceValues;

    [SerializeField] private DataManager DataI;

    void Awake()
    {
        SetUpScene();
    }

    private void SetUpScene()
    {
        Devices = GameObject.FindWithTag("Devices");
        DeviceValues = new float[Devices.transform.childCount - 1];

        //for(int i=0; i<_deviceList.Count; i++)
        for(int i=3; i<5; i++)
        {   
            Devices.transform.GetChild(i).name = DataI.DeviceList[i]["bridge_name"];
            DeviceValues[i] = DataI.GetValueFromSensor(i);
        }
    }

    private void SetDeviceValue(int _id)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        RaycastObjects();

        Devices.transform.GetChild(4).transform.Rotate(new Vector3(0,DeviceValues[4],0) * Time.deltaTime * 1000);   

        if(DeviceValues[3] == 1f)
        {
            Devices.transform.GetChild(3).GetComponent<Light>().range = DeviceValues[4]*80f;
        }
        else
        {
            Devices.transform.GetChild(3).GetComponent<Light>().range = 0f;
        }
    }


    private void RaycastObjects()
    {   
        
        if(Input.GetMouseButtonDown(0))
        {
            var hit = new RaycastHit();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {  
                // This method is used to send data to Flutter
                UnityMessageManager.Instance.SendMessageToFlutter(DataI.DeviceList);
                
                Debug.Log("ping " + hit.transform.name);
            }
        }

        for (int i = 0; i < Input.touchCount; ++i)
        {   
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                var hit = new RaycastHit();

                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                if (Physics.Raycast(ray, out hit))
                {  
                    // This method is used to send data to Flutter
                    UnityMessageManager.Instance.SendMessageToFlutter("The cube feels touched.");
                    Debug.Log(hit);
                }
            }
        }
    }

    // This method is called from Flutter
    public void SetRotationSpeed(String message)
    {
        float value = float.Parse(message);
        //RotateValue = value;
    }

    // This method is called from Unity
    public void SwitchLightOn(String message)
    {
        bool lightOn = ( message == "true");

        DeviceValues[3] = lightOn==true?1:0;
    }

    // This method is called from Unity
    public void SetRotationVelocity(float message)
    {
        DeviceValues[4] = message;
        DataI.DeviceList[4]["device_value"] = DeviceValues[4].ToString();

        UnityMessageManager.Instance.SendMessageToFlutter(DeviceValues[4].ToString());
    }

    // This method is called from Unity
    public void TurnLightOn(bool message)
    {
        DeviceValues[3] = message==true?1f:0f;
        DataI.DeviceList[3]["device_value"] = DeviceValues[3].ToString();  

        UnityMessageManager.Instance.SendMessageToFlutter(DeviceValues[3].ToString());
    }

    private void OnApplicationExit()
    {
        //Debug log datai jsondata
    }

}
