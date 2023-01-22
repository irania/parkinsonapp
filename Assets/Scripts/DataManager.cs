using System;
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
        public List<User> Users;
        private const string AppId = "9A6E5919-7EED-4A2E-8887-C34E02949274";

        private void Start()
        {
            Users = new List<User>();
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
            return newUser;
        }

        public void UploadUser(User u)
        {
            Debug.Log(u.UserName.ToString());
            Debug.Log(JsonUtility.ToJson(u));
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
    }
}