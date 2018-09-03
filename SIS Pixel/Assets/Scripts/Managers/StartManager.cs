using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public void LoadAnalyticMode()
    {
        SceneManager.LoadScene("AnalyticMode", LoadSceneMode.Single);
    }

    public void LoadGameMode()
    {
        SceneManager.LoadScene("BattleMode", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
