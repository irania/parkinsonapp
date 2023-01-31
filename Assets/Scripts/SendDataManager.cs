using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using Entities;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Send Data to server and save data if server was unavailable
/// </summary>
public class SendDataManager : Singleton<SendDataManager>
{
    /// <summary>
    /// Server URL
    /// </summary>
    public string Url = "http://gamesdata.cognitivetests.ir/";
    public const string AppId = "9A6E5919-7EED-4A2E-8887-C34E02949274";
    public Text debugText;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        #if UNITY_ANDROID
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/");

            //every 120 second send files to server
            InvokeRepeating("SendSavedFilesAndDelete", 0, 120.0f);
        #endif
    }
    
    public void SendJsonUser(string json,Func<string,int> afterCall=null)
    {
        StartCoroutine(PostRequestCoroutine(Url+"users",json,afterCall));
    }
    
    public void SendJsonData(GameData gameData)
    {
        //scene name
        gameData.SceneName = SceneManager.GetActiveScene().name;

        //time
        gameData.Time = DateTime.Now.ToString();

        //user name
        gameData.UserID = DataManager.Instance.GetCurrentUser().Id;
        string fileName = gameData.DataName+"_"+DateTime.Now.Ticks+".txt";
        Log(gameData.Data);    
        StartCoroutine(UploadFile(Url + "data/apps/" + AppId + "/users/" + gameData.UserID,
                gameData.Data,
                fileName,
                gameData.SceneName,
                gameData.DataName));
            
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
            Log(string.Format("{0}: {1} json is: {2}", www.url, www.error, json));
            #if UNITY_EDITOR
            ;
            #elif UNITY_ANDROID
                if (www.isNetworkError || www.downloadHandler.text.IndexOf("parsing")==-1)
                    SaveData(www.url,json);
            #endif
        }
        else
        {
            Log(string.Format("Response: {0}", www.downloadHandler.text));
            if(afterCall!=null)
                afterCall(json);

        }
    }

    IEnumerator UploadFile(string apiUrl, string data, string fileName, string location, string rawData)
    {
        Log(data+" , "+fileName+", "+location+" , "+rawData);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("rawdata",rawData));
        formData.Add(new MultipartFormDataSection("location", location));
        formData.Add(new MultipartFormFileSection("file", System.Text.Encoding.UTF8.GetBytes(data), fileName, "application/octet-stream"));
        UnityWebRequest www = UnityWebRequest.Post(apiUrl, formData);
        
        // Send the request
        yield return www.SendWebRequest();
            
        // Check for errors
        if (www.isNetworkError || www.isHttpError)
        {
            Log(string.Format("{0}: {1} json is: {2}", www.url, www.error, www.uploadedBytes));
        }
        else
        {
            //Log(formData.ToString());
            Log("Upload complete!");
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
            Log(ex.ToString());
        }
    }

    private void Log(string message)
    {
        Debug.Log(message);
    }
}
