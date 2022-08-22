using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public List<QuestGoal> questGoals = new List<QuestGoal>();
    private List<QuestGoalUI> questGoalUIs = new List<QuestGoalUI>();
    public void Init()
    {
        QuestGoal.OnAmountAdd += QuestGoal_OnAmountAdd;
        questGoals.ForEach(g => EnableGoals(g));
    }
    void EnableGoals(QuestGoal g)
    {
        g._currentAmount = 0;
        QuestGoalUI newUI = Instantiate(QuestManager.Instance.questGoalUI, QuestManager.Instance.questGoalUIContainer).GetComponent<QuestGoalUI>();
        newUI.qText.text = g.description;
        newUI.questGoal = g;
        questGoalUIs.Add(newUI);
    }
    public void InitSave()
    {
        QuestGoal.OnAmountAdd += QuestGoal_OnAmountAdd;
        questGoalUIs.Clear();
        questGoals.ForEach(g => EnableSaveGoals(g));
    }
    void EnableSaveGoals(QuestGoal g)
    {
        if (g.Complete())
            return;
        QuestGoalUI newUI = Instantiate(QuestManager.Instance.questGoalUI, QuestManager.Instance.questGoalUIContainer).GetComponent<QuestGoalUI>();
        newUI.qText.text = g.description;
        newUI.questGoal = g;
        questGoalUIs.Add(newUI);
    }

    private void QuestGoal_OnAmountAdd(QuestGoal goal)
    {
        if (goal.Complete())
        {
            QuestGoalUI ui = questGoalUIs.Find(g => g.questGoal == goal);
            ui.StopAllCoroutines();
            ui.StartCoroutine(ui.End());
        }
        if (questGoals.TrueForAll(g => g.Complete()))
        {
            QuestManager.Instance.RemoveQuest(this);
        }
    }
    public QuestGoal GetGoal(QuestGoalIdentifier goalIdentifier)
    {
        foreach (QuestGoal goal in questGoals)
        {
            if (goalIdentifier == goal.goalIdentity)
                return goal;
        }
        return null;
    }
}
[System.Serializable]
public class QuestGoal
{
    public delegate void OnAddCompletion(QuestGoal goal);
    public static event OnAddCompletion OnAmountAdd;
    public QuestGoalIdentifier goalIdentity;
    public string description;
    [HideInInspector]
    public int currentAmount
    {
        get => _currentAmount;
        set
        {
            _currentAmount = value;
            OnAmountAdd.Invoke(this);
        }
    }
    [HideInInspector] public int _currentAmount = 0;
    public int requiredAmount = 1;
    
    public bool Complete() => currentAmount >= requiredAmount;
}