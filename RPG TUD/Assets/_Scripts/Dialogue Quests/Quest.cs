using UnityEngine;

public enum QuestState
{
    NotStarted,
    InProgress,
    Completed
}

[CreateAssetMenu(menuName = "RPG/Quest")]
public class Quest : ScriptableObject
{
    public string questID;
    public string questTitle;

    [TextArea(3, 6)]
    public string description;

    public QuestState state = QuestState.NotStarted;
}
