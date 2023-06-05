using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindLocation2 : MonoBehaviour
{
    public Text GPSStatus;
    public Text Latitude;
    public Text Longitude;


    void Start()
    {
        StartCoroutine(GetLocation());
    }
    private IEnumerator GetLocation()
    {
        int MaxWait = 0;
        Input.location.Start();
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);

        }
        Latitude.text = Input.location.lastData.latitude.ToString();
        Longitude.text = Input.location.lastData.longitude.ToString();
        //yield break;

        if (MaxWait < 1)
        {
            Debug.Log("maxwait");
            GPSStatus.text = "Time Out";
            yield break;

        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("connection failed");
            GPSStatus.text = "Unable To Determine Device Location";
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("connection failed");
            GPSStatus.text = "Unable To Determine Device Location";
            yield break;
        }
        else
        {
            Debug.Log("else else");
            GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1.0f);
        }

    }
    // Update is called once per frame
    void Update()
    {
        Latitude.text = Input.location.lastData.latitude.ToString();
        Longitude.text = Input.location.lastData.longitude.ToString();
    }
    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            //Access granted to GPS values and it has been initialized
            Debug.Log("InUpdate");
            GPSStatus.text = "Running";
            Latitude.text = Input.location.lastData.latitude.ToString();
            Longitude.text = Input.location.lastData.longitude.ToString();
        }
        else
        {
            //Service is Stopped
            Debug.Log("else");
            GPSStatus.text = "Stop";
        }
    }
}
