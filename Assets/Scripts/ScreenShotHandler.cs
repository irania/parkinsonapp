using System.Collections;
using UnityEngine;

    public class ScreenShotHandler: MonoBehaviour
    {
        public IEnumerator TakeScreenShotAndSave(string filename)
        {
            yield return new WaitForEndOfFrame();
            Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenshot.Apply();
        
            string path = Application.persistentDataPath + "/" + filename;
            System.IO.File.WriteAllBytes(path, screenshot.EncodeToPNG());

            // Save the screenshot to the device's photo gallery
            AndroidJavaClass classUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = classUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass classMedia = new AndroidJavaClass("android.media.MediaScannerConnection");
            classMedia.CallStatic("scanFile", objActivity, new string[] { path }, null, new AndroidJavaProxy("android.media.MediaScannerConnection.OnScanCompletedListener"));
        
        }
        
    }
