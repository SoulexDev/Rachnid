using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            return _current == null ? _current = new SaveData() : _current;
        }
        set
        {
            _current = value;
        }
    }

    public PlayerData playerData = new PlayerData();
    public SceneData sceneData = new SceneData();
    public EntityData entityData = new EntityData();
    public QuestData questData = new QuestData();
    //{ 
    //    get 
    //    {
    //        if(playerData == null)
    //        {
    //            playerData = new PlayerData();
    //        }
    //        return playerData;
    //        //return playerData == null ? playerData = new PlayerData() : playerData;
    //    }
    //    set
    //    {
    //        playerData = value;
    //    }
    //}
}