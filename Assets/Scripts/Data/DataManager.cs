using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DataManager: Singleton<DataManager>
    {
        public int CurrentUser;
        public static List<int> DoneTasks;
        public static GameData UnSendData;
        private List<User> Users;
        private const string AppId = "9A6E5919-7EED-4A2E-8887-C34E02949274";
        private const string PlayerPrefabKey = "Users";

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            Users = LoadFromFile();
            if (Users.Count > 0)
                CurrentUser = Users.Count - 1;
        }

        public User CreateNewUser(string name)
        {
            User newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                AppId = AppId,
                UserName = name,
                IsUploaded = false
            };
            Users.Add(newUser);
            UploadUser(newUser);
            SaveUsers(Users);
            return newUser;
        }

        public void UploadUser(User u)
        {
            SendDataManager.Instance.SendJsonUser(JsonUtility.ToJson(u),AfterUploadUser);
        }

        public int AfterUploadUser(string json)
        {
            User user = JsonUtility.FromJson<User>(json);
            foreach (var u in Users.Where(u => u.Id.Equals(user.Id)))
            {
                u.IsUploaded = true;
            }
            return 0;
        }


        public List<string> GetUserNames()
        {
            List<string> us = new List<string>();
            if(Users!=null)
                foreach (var user in Users)
                {
                    us.Add(user.UserName);
                }
            return us;
        }

        public void SelectLastUser()
        {
            CurrentUser = Users.Count - 1;
        }
        public void SetCurrentUser(int num)
        {
            CurrentUser = num;
        }

        public User GetCurrentUser()
        {
            return Users[CurrentUser];
        }

        private List<User> LoadFromFile()
        {
            if (PlayerPrefs.HasKey(PlayerPrefabKey))
            {
                // Load the JSON string from PlayerPrefs
                string json = PlayerPrefs.GetString(PlayerPrefabKey);
                // Convert the JSON string to a list of objects
                var objects = JsonHelper.FromJson<User>(json);
                return objects!=null && objects.Length>0?objects.ToList(): new List<User>();
                
            }
            else
            {
                return new List<User>();
            }
        }
        private void SaveUsers(List<User> users)
        {
            // Convert the list of objects to a JSON string
            string json = JsonHelper.ToJson<User>(users.ToArray());
            // Save the JSON string to PlayerPrefs
            PlayerPrefs.SetString(PlayerPrefabKey, json);
            PlayerPrefs.Save();
        }
    }
}