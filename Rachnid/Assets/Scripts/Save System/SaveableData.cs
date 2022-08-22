using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    #region Player Inventory
    public List<int> itemIndex = new List<int>();
    public List<int> noteIndex = new List<int>();
    public int handItemIndex;
    public float batteryPercentage;
    public bool on;
    public bool canDeplete;
    public float fillAmount;
    #endregion
    #region Player Stats
    public float health;
    public Vector3 velocity;
    public Vector3 position;
    public float camX;
    public float camY;
    #endregion
}
[System.Serializable]
public class SceneData
{
    #region Scene Info
    public int sceneIndex = 0;
    public List<int> objectState = new List<int>();
    #endregion
    #region States
    public List<bool> doorStates = new List<bool>();
    #endregion
}
[System.Serializable]
public class EntityData
{
    #region Spiders
    public List<Entity> entities = new List<Entity>();
    #endregion
}
[System.Serializable]
public class Entity
{
    public int id;
    public Vector3 position;
    public Quaternion rotation;
    public EnemyAI.States state;
    public float health;
}
[System.Serializable]
public class QuestData
{
    public List<QuestSave> quests = new List<QuestSave>();
}
[System.Serializable]
public class QuestSave
{
    public List<QuestGoalSave> questGoals = new List<QuestGoalSave>();
}
[System.Serializable]
public class QuestGoalSave
{
    public int currentAmount;
}