using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int width = 0;
    public int height = 0;

    public GameObject gridManager;

	// Use this for initialization
	void Start () {
        gridManager.GetComponent<GridManager>().SetGridState(false);
	}

    // Generate the grid.
    public void Generate(bool gameMode=false)
    {
        if (width == 0 || height == 0)
        {
            width = 50;
            height = 50;
        }
        gridManager.GetComponent<GridManager>().SetGridState(true);
        gridManager.GetComponent<GridManager>().Initialize(width, height, gameMode);
    }

    // Set width using input if possible.
    public void SetWidth(string w)
    {
        int.TryParse(w, out width);
    }

    // Set height using input if possible.
    public void SetHeight(string h)
    {
        int.TryParse(h, out height);
    }

    public void BackToStartScreen()
    {
        SceneManager.LoadScene("StartingScreen", LoadSceneMode.Single);
    }
}
