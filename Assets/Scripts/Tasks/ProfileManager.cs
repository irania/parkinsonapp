
using System;
using DefaultNamespace;
using Entities;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager: MonoBehaviour
{
    [SerializeField]
    private Toggle GenderMan;

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

    public void OnSubmit()
    {
        var profile = new Profile()
        {
            Age = Int32.Parse(Age.text),
            IsMan = GenderMan.isOn,
            Drugs = Drugs.text,
            Id = DataManager.Instance.GetCurrentUser().Id,
            LastMedicine = Int32.Parse(LastDrug.text),
            LungDisease = LungDisease.isOn,
            ParkFamily = ParkFamily.isOn,
            YearDisease = Int32.Parse(YearDisease.text)
        };
        GameData gd = new GameData()
        {
            DataName = "profile",
            Data = JsonUtility.ToJson(profile)
        };
        SendDataManager.Instance.SendJsonData(gd);
        Application.LoadLevel(0);
    }
}
