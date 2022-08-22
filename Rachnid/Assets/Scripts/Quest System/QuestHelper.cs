using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHelper : MonoBehaviour
{
    public void AddQuest(Quest quest)
    {
        QuestManager.Instance.AddQuest(quest);
    }
}