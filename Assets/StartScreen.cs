using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{

    public GameObject StartScreenPanel;

    void Start()
    {
    }

    void Update()
    {

    }

  
    public void StartGame()
    {
        StartScreenPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        StartScreenPanel.SetActive(true);
    }
}
