using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Send Data to server and save data if server was unavailable
/// </summary>
public class SendDataManager : Singleton<SendDataManager>
{
    /// <summary>
    /// Server URL
    /// </summary>
    public string Url = "http://gamesdata.cognitivetests.ir/";
    void Start()
    {
        #if UNITY_ANDROID
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/");

            //every 120 second send files to server
            InvokeRepeating("SendSavedFilesAndDelete", 0, 120.0f);
        #endif
    }

    /// <summary>
    /// Send Json to server
    /// </summary>
    /// <param name="json">Json format of game results object</param>
    public void SendJsonUser(string json,Func<string,int> afterCall=null)
    {
        StartCoroutine(PostRequestCoroutine(Url+"users",json,afterCall));
    }
    /// <summary>
    /// Send Json to server
    /// </summary>
    /// <param name="json">Json format of game results object</param>
    private void SendJson(string url, string json,Func<string,int> afterCall=null)
    {
        StartCoroutine(PostRequestCoroutine(url,json,afterCall));
    }

    /// <summary>
    /// Post request coroutine
    /// </summary>
    /// <param name="url">URL address</param>
    /// <param name="json">Json format of game results object</param>
    private IEnumerator PostRequestCoroutine(string url, string json,Func<string,int> afterCall)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";


        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.downloadHandler.text.IndexOf("Success") == -1)
        {
            Debug.LogError(string.Format("{0}: {1} json is: {2}", www.url, www.error, json));
            #if UNITY_EDITOR
            ;
            #elif UNITY_ANDROID
                if (www.isNetworkError || www.downloadHandler.text.IndexOf("parsing")==-1)
                    SaveData(json);
            #endif
        }
        else
        {
            Debug.Log(string.Format("Response: {0}", www.downloadHandler.text));
            if(afterCall!=null)
                afterCall(json);

        }
    }

    /// <summary>
    /// Save data in a temp file
    /// </summary>
    private void SaveData(string url, string json)
    {
        string folderPath = Application.persistentDataPath + "/";
        string fileName = $@"{url}-{DateTime.Now.Ticks}.txt";
        System.IO.File.WriteAllText(folderPath + fileName, json);
    }

    /// <summary>
    /// Send saved files to server
    /// </summary>
    private void SendSavedFilesAndDelete()
    {
        try
        {
            foreach (string file in Directory.GetFiles(Application.persistentDataPath + "/"))
            {
                String contents = System.IO.File.ReadAllText(file);
                SendJson(file.Split('-')[0],contents);
                File.Delete(file);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }
}
