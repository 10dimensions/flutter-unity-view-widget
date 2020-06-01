using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class DataManager: MonoBehaviour
{   
    public JSONNode JsonData;
    public JSONNode DeviceList;

    [SerializeField] private GameObject IotI;

    void Awake()
    {
        JsonData = JSON.Parse( LoadResourceTextfile("state.json") );
        DeviceList = JsonData["response"]["device_groups"][0]["device_sensors"];

        IotI.SetActive(true);

    }

    public string LoadResourceTextfile(string path)
    {
        string filePath = "" + path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        return targetFile.text;
    }

    public float GetValueFromSensor(int _idx)
    {   
        string _type = DeviceList[_idx]["device_type"];
        return float.Parse(DeviceList[_idx]["device_value"]);
    }

    public string GetTypeOfSensor(int _idx)
    {
        return DeviceList[_idx]["device_type"];
    }

    public void SetValueFromSensor()
    {

    }

}