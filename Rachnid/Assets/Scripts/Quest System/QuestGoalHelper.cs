using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoalHelper : MonoBehaviour
{
    [SerializeField] private QuestGoalIdentifier goal;
    [SerializeField] private int amount = 1;
    private QuestManager questManager => QuestManager.Instance;

    public void AddToGoal()
    {
        if(questManager.activeQuest != null)
            questManager.activeQuest.GetGoal(goal).currentAmount += amount;
    }
}