using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = System.Random;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    //timer
    private float timer = 25f;
    private bool startTimer = false;
    public Text timerText;

    public GameObject StartGamePanel;

    //random operations
    Random rand = new Random();
    private int mainCodeNumber;
    public Text leftNumberText, middleNumberText, rightNumberText;
    private int leftNumber, middleNumber, rightNumber;
    private int randOperation, randCountOfOperations;

    //failure or success first part
    private bool playerSucceseed;
    public GameObject SuccessPanel;
    public GameObject FailurePanel;

    //reset button
    private int saveLeftNumber, saveMiddleNumber, saveRightNumber;

    //second  part
    private bool secondPartStarted = false;
    public GameObject WinGamePanel, LoseGamePanel;

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartSecondPart()
    {
        FailurePanel.SetActive(false);
        SuccessPanel.SetActive(false);
        timer = 10f;
        startTimer= true;
        secondPartStarted= true;

        mainCodeNumber = rand.Next(0, 9);
        MakeOperations();
        leftNumberText.text = leftNumber.ToString();
        middleNumberText.text = middleNumber.ToString();
        rightNumberText.text = rightNumber.ToString();
    }

    public void StartGame()
    {
        startTimer= true;
        //

        mainCodeNumber = rand.Next(0, 9);
        //

        //словарь не используется
        var operations = new Dictionary<int, string>() 
        {
            {0, "+2 | -1 | +0"},
            {1, "+0 | -1 | +2"},
            {2, "+1 | +2 | +1"},
        };

        MakeOperations();

        if ((leftNumber == middleNumber) && (leftNumber == rightNumber))
        {
            MakeOperations();
        }

        leftNumberText.text = leftNumber.ToString();
        middleNumberText.text = middleNumber.ToString();
        rightNumberText.text = rightNumber.ToString();
        //
    }

    public void MakeOperations()
    {
        leftNumber = middleNumber = rightNumber = mainCodeNumber;
        randCountOfOperations = rand.Next(1, 4);

        for (int i = 0; i < randCountOfOperations; i++)
        {
            randOperation = rand.Next(0, 2);

            if (randOperation == 0)
            {
                leftNumber -= 2;
                middleNumber += 1;
            }
            else if (randOperation == 1)
            {
                middleNumber += 1;
                rightNumber -= 2;
            }
            else if (randOperation == 2)
            {
                leftNumber -= 1;
                middleNumber -= 2;
                rightNumber -= 1;
            }
        }
        saveLeftNumber = leftNumber;
        saveMiddleNumber = middleNumber;
        saveRightNumber = rightNumber;
    }

    public void CheckSucceses()
    {
        if ((leftNumber == middleNumber) && (leftNumber == rightNumber) && (leftNumber == mainCodeNumber))
        {
            if (secondPartStarted != true)
            {
                SuccessPanel.SetActive(true);
                playerSucceseed = true;
                startTimer = false;
            }
            else
            {
                WinGamePanel.SetActive(true);
                startTimer= false;
            }
        }
    }    

    public void LeftButtonClick()
    {
        if (leftNumber  <= 8)
        {
            if (middleNumber >= -9)
            {   
                    leftNumber += 2;
                    middleNumber -= 1;

                    leftNumberText.text = leftNumber.ToString();
                    middleNumberText.text = middleNumber.ToString();
                    rightNumberText.text = rightNumber.ToString();              
            }
        }
        CheckSucceses();
    }

    public void MiddleButtonClick()
    {
            if (middleNumber >= -9)
            {
                if (rightNumber <= 8)
                {
                    middleNumber -= 1;
                    rightNumber += 2;

                    leftNumberText.text = leftNumber.ToString();
                    middleNumberText.text = middleNumber.ToString();
                    rightNumberText.text = rightNumber.ToString();
                }
            }
        CheckSucceses();
    }

    public void RightButtonClick()
    {
        if (leftNumber <= 9)
        {
            if (middleNumber <= 8)
            {
                if (rightNumber <= 9)
                {
                    leftNumber += 1;
                    middleNumber += 2;
                    rightNumber += 1;

                    leftNumberText.text = leftNumber.ToString();
                    middleNumberText.text = middleNumber.ToString();
                    rightNumberText.text = rightNumber.ToString();
                }
            }
        }
        CheckSucceses();
    }

    public void ResetNumbers()
    {
        leftNumberText.text = saveLeftNumber.ToString();
        middleNumberText.text = saveMiddleNumber.ToString();
        rightNumberText.text = saveRightNumber.ToString();
        leftNumber = saveLeftNumber;
        middleNumber = saveMiddleNumber; 
        rightNumber= saveRightNumber;
    }

    void Start()
    {
        StartGamePanel.SetActive(true);
    }

    void Update()
    {
        if (startTimer == true)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.Round(timer).ToString();
        }

        if (timer <= 0 )
        {
            if(playerSucceseed != true)
            {
                if (secondPartStarted != true)
                {
                    FailurePanel.SetActive(true);
                    playerSucceseed = false;
                    startTimer = false;
                }

            }

            if (secondPartStarted == true)
            {
                LoseGamePanel.SetActive(true);
                startTimer = false;
            }

        }
    }
}
