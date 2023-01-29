using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class SensorsReader : MonoBehaviour
{
    private Text text;
    private IList<Vector3> angularVelocities;
    private IList<Vector3> accelerations;
    private IList<Vector3> attitudes;
    private IList<Vector3> gravities;
    private IList<string> times;
    public GameData GameData;
    private Boolean StartRecord = false;

    void Start()
    {
        #if !UNITY_EDITOR && UNITY_ANDROID
            StartRecord = true
        #endif
        if (StartRecord)
        {
            InputSystem.EnableDevice(Gyroscope.current);
            InputSystem.EnableDevice(Accelerometer.current);
            InputSystem.EnableDevice(AttitudeSensor.current);
            InputSystem.EnableDevice(GravitySensor.current);

            angularVelocities = new List<Vector3>();
            accelerations = new List<Vector3>();
            attitudes = new List<Vector3>();
            gravities = new List<Vector3>();
            times = new List<string>();
        }
    }

    void Update()
    {
        if (StartRecord)
        {
            Vector3 angularVelocity = Gyroscope.current.angularVelocity.ReadValue();
            Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();
            Vector3 attitude =
                AttitudeSensor.current.attitude.ReadValue().eulerAngles; // ReadValue() returns a Quaternion
            Vector3 gravity = GravitySensor.current.gravity.ReadValue();

            angularVelocities.Add(angularVelocity);
            accelerations.Add(acceleration);
            attitudes.Add(attitude);
            gravities.Add(gravity);
            times.Add(DateTime.Now.ToString());
        }

    }

    public void OnClick()
    {
        if (StartRecord)
        {
            GameData = CreateGameData();
            SendDataManager.Instance.SendJsonData(GameData);
        }
    }

    private GameData CreateGameData()
    {
        var gameData = new GameData();
        //scene name
        gameData.SceneName = SceneManager.GetActiveScene().name;

        //time
        gameData.Time = DateTime.Now.ToString();

        //user name
        //gameData.UserID = DataManager.Instance.GetCurrentUser().Id;
        gameData.UserID = "E5E15C8E-3708-44AC-B195-B4E9B1080E8F";
        
        //data
        gameData.DataName = "AccelerationSensor";
        var lists = new {angularVelocities, accelerations, attitudes, gravities, times};
        gameData.Data = JsonUtility.ToJson(lists);

        return gameData;


    }
}