using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            StartRecord = true;
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

    private void OnDestroy()
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

        //data
        gameData.DataName = "AccelerationSensor";
        var angularV = JsonHelper.ToJson(angularVelocities.ToArray());
        var acceleration = JsonHelper.ToJson(accelerations.ToArray());
        var attitude = JsonHelper.ToJson(attitudes.ToArray());
        var gravity = JsonHelper.ToJson(gravities.ToArray());
        var time = JsonHelper.ToJson(times.ToArray());
        gameData.Data =
            String.Format(
                "\"angularVelocities\": {0}, \"accelerations\": {1}, \"attitudes\": {2}, \"gravities\": {3}, \"times\": {4} ",
                angularV, acceleration, attitude, gravity, time);
        gameData.Data = "{" + gameData.Data + "}";

        return gameData;


    }
}