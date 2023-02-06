using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Entities;
using UnityEngine;
using UnityEngine.UI;

public class UserPanelHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject SelectPanel;
    [SerializeField]
    private GameObject NewPanel;
    [SerializeField]
    private InputField UserNameField;
    [SerializeField] 
    private Dropdown UsersDropDown;

    private void Awake()
    {
        SetDropDown();
    }
    

    // Start is called before the first frame update
    void Start()
    {
       SetSelectPanel();
    }

    public void OnNewButtonClick()
    {
        SelectPanel.SetActive(false); 
        NewPanel.SetActive(true); 
    }

    public void OnSelectButtonClick()
    {
        SelectUser(UsersDropDown.value);
        gameObject.SetActive(false);
        
    }

    private void SelectUser(int value)
    {
        DataManager.Instance.CurrentUser = UsersDropDown.value;
    }

    public void OnCreateButtonClick()
    {
        DataManager.Instance.CreateNewUser(UserNameField.text);
        SetDropDown();
        SetSelectPanel();
        DataManager.Instance.SelectLastUser();
        UsersDropDown.value = DataManager.Instance.CurrentUser;
        SelectUser(DataManager.Instance.CurrentUser);
        UserNameField.text = "";
        gameObject.SetActive(false);

    }

    private void SetDropDown()
    {
        UsersDropDown.ClearOptions();
        UsersDropDown.AddOptions(DataManager.Instance.GetUserNames());
        UsersDropDown.value = DataManager.Instance.CurrentUser;
    }

    private void SetSelectPanel()
    {
        SelectPanel.SetActive(true); 
        NewPanel.SetActive(false); 
    }

}
