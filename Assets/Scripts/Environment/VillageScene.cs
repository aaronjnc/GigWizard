using HeneGames.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct QuestDialoguePrompts
{
    public Quests questEnum;
    public DialogueManager dialogueManager;
}

public class VillageScene : MonoBehaviour
{
    [SerializeField]
    private DialogueManager intro = new DialogueManager();

    [SerializeField]
    private List<QuestDialoguePrompts> questDialogues = new List<QuestDialoguePrompts>();

    private DialogueTrigger trigger;

    private Dictionary<Quests, DialogueManager> dialogueManagerDictionary = new Dictionary<Quests, DialogueManager>();

    private void Start()
    {
        trigger = GetComponentInParent<DialogueTrigger>();
        foreach (QuestDialoguePrompts prompt in questDialogues)
        {
            dialogueManagerDictionary.Add(prompt.questEnum, prompt.dialogueManager);
        }
        Quests currentQuest = Quests.Edelweiss;
        if (GameManager.Instance.GetQuestCompleted(Quests.Edelweiss))
        {
            currentQuest = Quests.Tulip;
            PlayDialogue(currentQuest);
        }
        else
        {
            intro.TriggerDialogue(trigger);
        }        
    }

    public void WaitForSeconds()
    {
        StartCoroutine(NextConversation());
    }

    IEnumerator NextConversation()
    {
        yield return new WaitForSeconds(3);
        PlayDialogue(Quests.Edelweiss);
    }

    private void PlayDialogue(Quests currentQuest)
    {
        dialogueManagerDictionary[currentQuest].enabled = true;
        dialogueManagerDictionary[currentQuest].TriggerDialogue(trigger);
    }
}
