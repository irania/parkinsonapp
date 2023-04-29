using System;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraHandler : Singleton<CameraHandler>
{
    WebCamTexture webcamTexture;
    public Text debug;
    private Quaternion rotation;
    void Start()
    {
        
        // Request permission to use the camera
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        var size = gameObject.GetComponent<RectTransform>().sizeDelta;
        rotation = Quaternion.Euler(0, 0, -90);
        // Check if permission was granted
        if (Permission.HasUserAuthorizedPermission(Permission.Camera) && WebCamTexture.devices.Length>0 )
        {
	        // Create a new WebCamTexture & Start the camera
	        webcamTexture = new WebCamTexture(WebCamTexture.devices[WebCamTexture.devices.Length-1].name);
	        webcamTexture.Play();
 
	        // Assign the WebCamTexture to a Material on a Quad object
	        GetComponent<Image>().sprite = null;
	        GetComponent<Image>().material.mainTexture = webcamTexture;
	        rotation = Quaternion.Euler(0, 0, -webcamTexture.videoRotationAngle);
        }

        
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(size.y, size.x, 1);
        transform.localScale = (Screen.orientation == ScreenOrientation.Portrait)?new Vector3(1f, -1.4f, 1): new Vector3(-1f, 1.4f, 1);

    }
    private void Update()
    {
	    rotation = Quaternion.Euler(0, 0, -webcamTexture.videoRotationAngle);
	    transform.rotation = rotation;
	    transform.localScale = (Screen.orientation == ScreenOrientation.Portrait)?new Vector3(1f, -1.4f, 1): new Vector3(-1f, 1.4f, 1);
	    if (Keyboard.current.escapeKey.isPressed)
        {
           TakePicture();
        }
    }

    public void TakePicture()
    {
	    webcamTexture.Stop();
	    // Restart the camera
	    webcamTexture.Play();
            
    }

}