using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<Quest> questQueue = new List<Quest>();
    public GameObject questGoalUI;
    public Transform questGoalUIContainer;
    public Quest activeQuest => questQueue.Count == 0 ? null : questQueue[0];
    private void Awake()
    {
        Instance = this;
        SaveManager.OnSave += SaveManager_OnSave;
        SaveManager.OnLoad += SaveManager_OnLoad;
    }
    private void SaveManager_OnSave()
    {
        SaveData.current.questData.quests.Clear();
        foreach (var quest in questQueue)
        {
            QuestSave questSave = new QuestSave();
            List<QuestGoalSave> goalSaves = new List<QuestGoalSave>();
            //goalSaves.Capacity = quest.questGoals.Capacity;
            for (int i = 0; i < quest.questGoals.Count; i++)
            {
                QuestGoalSave goalSave = new QuestGoalSave();
                goalSave.currentAmount = quest.questGoals[i].currentAmount;
                goalSaves.Add(goalSave);
            }
            questSave.questGoals = goalSaves;
            SaveData.current.questData.quests.Add(questSave);
        }
    }

    private void SaveManager_OnLoad()
    {
        foreach (var saveQuest in SaveData.current.questData.quests)
        {
            //Quest quest = new Quest();
            Quest quest = (Quest)ScriptableObject.CreateInstance("Quest");
            List<QuestGoal> goals = new List<QuestGoal>();
            //goals.Capacity = saveQuest.questGoals.Capacity;
            for (int i = 0; i < saveQuest.questGoals.Count; i++)
            {
                QuestGoal goal = new QuestGoal();
                goal._currentAmount = saveQuest.questGoals[i].currentAmount;
                goals.Add(goal);
            }
            quest.questGoals = goals;
            quest.InitSave();
        }
    }

    public void AddQuest(Quest quest)
    {
        questQueue.Add(quest);
        quest.Init();
    }
    public void RemoveQuest(Quest quest)
    {
        questQueue.Remove(quest);
    }
}