using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private float _winSceneLoadDelay = 2f;
    [SerializeField] private float _loseSceneLoadDelay = 2f;
    void Start()
    {
        GameEventsManager.instance.OnWin += WinGame;
        GameEventsManager.instance.OnPlayerDeath += LoseGame;
    }
    void OnDisable()
    {
        GameEventsManager.instance.OnWin -= WinGame;
        GameEventsManager.instance.OnPlayerDeath -= LoseGame;
    }
    public void LoadSceneByNameIdk(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadSceneWithDelay(string name, float delay)
    {
        StartCoroutine(LoadDelay(name, delay));
    }
    private void WinGame()
    {
        StartCoroutine(LoadDelay("MainMenu", _winSceneLoadDelay));
    }
    private void LoseGame()
    {
        StartCoroutine(LoadDelay("MainMenu", _loseSceneLoadDelay));
    }
    public IEnumerator LoadDelay(string name, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadSceneByNameIdk(name);
    }
}
