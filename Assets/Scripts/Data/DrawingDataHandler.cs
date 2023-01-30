using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DrawingDataHandler : MonoBehaviour
{
    private GameData GameData;
    

    public void SendData()
    {
        GameData = CreateGameData();
        SendDataManager.Instance.SendJsonData(GameData);
    }

    private GameData CreateGameData()
    {
        var gameData = new GameData();

        gameData.DataName = "lines";
        var lines = GameObject.FindObjectsOfType<Line>();
        var draw = new Draw();
        draw.lines = new SingleLine[lines.Length];
        for (int i=0;i< lines.Length;i++)
        {
            draw.lines[i]= new SingleLine
            {
                points = lines[i].getPoints().ToArray(),
                times = lines[i].getTimes().ToArray()
            };
        }
        gameData.Data = JsonUtility.ToJson(draw,true);

        return gameData;


    }
    
    private class Draw
    {
        public SingleLine[] lines;
    }
    
    [Serializable]
    private class SingleLine
    {
        public Vector2[] points;
        public long[] times;
    }
}
