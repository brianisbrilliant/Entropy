using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utility : MonoBehaviour
{
    [Header("For randomizing the level's colors")]
    public Renderer[] cubes;
    public Material[] mats;

    void Start()
    {
        StartCoroutine(TwoMinRestart());

        foreach(Renderer rend in cubes)
        {
            rend.material = mats[Random.Range(0, mats.Length - 1)];
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Start") || Input.GetKeyDown(KeyCode.Escape)) RestartLevel();
    }

    public void ChangeLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator TwoMinRestart()
    {
        yield return new WaitForSeconds(120);
        RestartLevel();
    }
}
