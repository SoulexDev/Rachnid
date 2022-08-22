using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGoalUI : MonoBehaviour
{
    public TextMeshProUGUI qText;
    public QuestGoal questGoal;
    [SerializeField] private LayoutElement layoutElement;
    private IEnumerator Start()
    {
        qText.transform.localPosition = new Vector2(-400, 0);
        while (layoutElement.preferredHeight < qText.preferredHeight)
        {
            layoutElement.preferredHeight = Mathf.Lerp(layoutElement.preferredHeight, qText.preferredHeight, Time.deltaTime * 5);
            qText.transform.localPosition = Vector2.Lerp(qText.transform.localPosition, Vector2.zero, Time.deltaTime * 5);
            yield return null;
        }
        layoutElement.preferredHeight = qText.preferredHeight;
        qText.transform.localPosition = Vector2.zero;
    }
    public IEnumerator End()
    {
        while (layoutElement.preferredHeight > 0)
        {
            layoutElement.preferredHeight = Mathf.Lerp(layoutElement.preferredHeight, 0, Time.deltaTime * 5);
            qText.transform.localPosition = Vector2.Lerp(qText.transform.localPosition, new Vector2(-400, 0), Time.deltaTime * 5);
            yield return null;
        }
        Destroy(gameObject);
    }
}