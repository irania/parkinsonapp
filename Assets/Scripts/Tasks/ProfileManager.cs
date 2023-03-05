
using System;
using DefaultNamespace;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ProfileManager: MonoBehaviour
{
    private const int GameId = 5;
    [SerializeField]
    private Toggle GenderMan;
    
    [SerializeField]
    private InputField Name;
    
    [SerializeField]
    private InputField Age;
    
    [SerializeField]
    private InputField YearDisease;
    
    [SerializeField]
    private InputField Drugs;
    
    [SerializeField]
    private InputField LastDrug;

    [SerializeField]
    private Toggle ParkFamily;
    
    [SerializeField]
    private Toggle LungDisease;
    
    [SerializeField]
    private Toggle RightHand;
    

    private void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.LoadLevel(0);
        }
    }
    public void OnSubmit()
    {
        
        var profile = new Profile()
        {
            Name = Name.text,
            Age = Int32.TryParse(Age.text, out int result) ? result : 0,
            IsMan = GenderMan.isOn,
            Drugs = Drugs.text,
            Id = DataManager.Instance.GetCurrentUser().Id,
            LastMedicine = Int32.TryParse(LastDrug.text, out int resultDrug) ? resultDrug : 0,
            LungDisease = LungDisease.isOn,
            ParkFamily = ParkFamily.isOn,
            YearDisease = Int32.TryParse(LastDrug.text, out int resultYear) ? resultYear : 0,
            RightHand = RightHand.isOn
        };
        GameData gd = new GameData()
        {
            DataName = "profile",
            Data = JsonUtility.ToJson(profile)
        };
        SendDataManager.Instance.SendJsonData(gd);
        DataManager.Instance.GetCurrentUser().LevelsDone[GameId] = true;
        Application.LoadLevel(0);
    }

    public void BackButton()
    {
        Application.LoadLevel(0);
    }
    
}
