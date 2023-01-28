using System;
using UnityEngine.Serialization;

namespace Entities
{
    [Serializable]
    public class GameData
    {
        public string UserID;
        public string SceneName;
        public string DataName;
        public string Time;
        public string Data;

    }
}