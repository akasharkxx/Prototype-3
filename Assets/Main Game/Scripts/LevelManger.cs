using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManger : MonoBehaviour
{
    public Button restartButton;

    private void Start()
    {
        restartButton.onClick.AddListener(ReloadLevel);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
}
