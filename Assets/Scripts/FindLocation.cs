using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindLocation : MonoBehaviour
{
    public Text GPSStatus;
    public Text Latitude;
    public Text Longitude;


    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLocation());
    }

    
    IEnumerator GPSLocation()
    {
        //check if user has enabled location service
        Debug.Log("I");
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("ii");
            yield break;
        }

        Debug.Log("iii");
        //Start service by querying location  
        Input.location.Start();
        //wait untill service initialize
        int MaxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && MaxWait > 0)
        {

            Debug.Log("while");
            yield return new WaitForSeconds(1);
            MaxWait--;  
        }
        //Service didn't start in 20 Sec
        if (MaxWait < 1)
        {
            Debug.Log("maxwait");
            GPSStatus.text = "Time Out";
            yield break;

        }
        //Connection Failed 
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
            InvokeRepeating(nameof(UpdateGPSData),0.5f,1.0f);
        }
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
