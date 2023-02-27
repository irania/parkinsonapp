using System;

[Serializable]
public class User
{
    public string Id;

    public string UserName;

    public string FirstName;

    public string LastName;

    public string Password;

    public DateTime LastActivity;

    public DateTime FirstActivity;

    public string Email;

    public string AppId;

    public bool IsUploaded;

    public bool[] LevelsDone;
}

