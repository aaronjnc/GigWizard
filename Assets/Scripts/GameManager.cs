using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void WinGame()
    {
        Time.timeScale = 0.0f;
        PlayerCharacter.Instance.DisableControls();
        Debug.Log("You won the game");
    }

    public void LoseGame()
    {
        Time.timeScale = 0.0f;
        PlayerCharacter.Instance.DisableControls();
        Debug.Log("You lost the game");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
