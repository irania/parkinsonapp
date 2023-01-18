using System;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class CameraHandler : MonoBehaviour
{
    WebCamTexture webcamTexture;
    public Text debug;
    void Start()
    {
        
        // Request permission to use the camera
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        var size = gameObject.GetComponent<RectTransform>().sizeDelta;
        var rotation = Quaternion.Euler(0, 0, -90);
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

        transform.rotation = rotation;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(size.y, size.x, 1);
        transform.localScale = new Vector3(1f, -1f, 1);
        debug.text = "size: " + size.x + " " + size.y + "\n" + Screen.height + ".";

    }
    private void Update()
    {
	    
	    
	    if (Input.GetKeyDown(KeyCode.Space))
        {
           TakePicture();
        }
    }

    public void TakePicture()
    {
        // Stop the camera
                webcamTexture.Stop();

                // Create a new Texture2D with the same dimensions as the WebCamTexture
                Texture2D picture = new Texture2D(webcamTexture.width, webcamTexture.height);

                // Copy the pixels from the WebCamTexture to the new Texture2D
                picture.SetPixels(webcamTexture.GetPixels());
                picture.Apply();

                // Encode the Texture2D as a PNG
                byte[] bytes = picture.EncodeToPNG();

                // Save the PNG to the device's photo gallery
                File.WriteAllBytes(Application.persistentDataPath + "/photo.png", bytes);

                // Restart the camera
                webcamTexture.Play();
            
    }

}