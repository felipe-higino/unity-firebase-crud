using System;

[Serializable]
public class PlayerData
{
    public string Name;// { get; }
    public string CreationDate;// { get; }

    public PlayerData(string name)
    {
        Name = name;
        CreationDate = DateTime.Now.ToString();
    }
}
