using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Quests
{
    Edelweiss,
    Tulip,
}

public class GameManager : Singleton<GameManager>
{

    private Dictionary<Quests, bool> questsCompleted = new Dictionary<Quests, bool>();

    [SerializeField]
    private GameObject gameOverScreen;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        questsCompleted.Add(Quests.Edelweiss, false);
        questsCompleted.Add(Quests.Tulip, false);
    }

    public void WinGame()
    {
        //Time.timeScale = 0.0f;
        PlayerCharacter player = PlayerCharacter.Instance;
        player.DisableControls();
        StartCoroutine(WinDelay(player));
    }

    public void LoseGame()
    {
        Time.timeScale = 0.0f;
        PlayerCharacter.Instance.DisableControls();
        gameOverScreen.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator WinDelay(PlayerCharacter player)
    {
        yield return new WaitForSeconds(3);
        GameObject familiar = Flower.Instance.SpawnFamiliar();
        familiar.GetComponentInChildren<DialogueManager>().TriggerDialogue(player.gameObject.GetComponent<DialogueTrigger>());
    }

    public void CompleteQuest(Quests quest)
    {
        questsCompleted[quest] = true;
    }

    public bool GetQuestCompleted(Quests quest)
    {
        return questsCompleted[quest];
    }

    public void ReloadScene()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1.0f;
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
