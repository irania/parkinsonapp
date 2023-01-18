using System;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class CameraHandler : MonoBehaviour
{
    WebCamTexture webcamTexture;
    private Quaternion baseRotation;
    void Start()
    {
        
        // Request permission to use the camera
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        // Check if permission was granted
        if (Permission.HasUserAuthorizedPermission(Permission.Camera) && WebCamTexture.devices.Length>0 )
        {
	        // Create a new WebCamTexture & Start the camera
	        webcamTexture = new WebCamTexture(WebCamTexture.devices[WebCamTexture.devices.Length-1].name,Screen.width,Screen.height);
	        webcamTexture.Play();

	        // Assign the WebCamTexture to a Material on a Quad object
	        GetComponent<Image>().material.mainTexture = webcamTexture;
	        transform.rotation = Quaternion.Euler(0, 0, -webcamTexture.videoRotationAngle);
	        //baseRotation = transform.rotation;
	        // float aspectRatio = Screen.width / Screen.height;
	        // float width = transform.localScale.x;
	        // float height = width / aspectRatio;
	        // transform.localScale = new Vector3(-width, height, 1);
	        //Vector3 scale = new Vector3(1, 1, 1);
	        //transform.localScale = scale;
	        //baseRotation = transform.rotation;
	        // float size = gameObject.GetComponent<Renderer>().bounds.size.y;
	        //
	        // Vector3 rescale = gameObject.transform.localScale;
	        //
	        // rescale.y = Screen.height * rescale.y / size;
	        //
	        // gameObject.transform.localScale = rescale;
	        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(Screen.height, Screen.width, 1);
	        transform.localScale = new Vector3(1, -1, 1);
        }

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