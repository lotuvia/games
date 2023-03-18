using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public Text guardianCountText;
    public Text workmanCountText;
    public Text foodCountText;
    public Text houseCountText;
    public Text houseCost;
    public Text RaidCountEnemiesText;
    public Text raidsCountText;
    public Text guardianFoodText;
    public Text workmanFoodText;
    public Text workmanCostText;
    public Text guardianCostText;

    public TextMeshProUGUI raidGuardiansText;
    public TextMeshProUGUI raidEnemiesText;
    public TextMeshProUGUI raidNumberText;
    public TextMeshProUGUI raidguardiansLeftText;

    public ImageTimer workmanFoodTimer;
    public ImageTimer guardianFoodTimer;
    public Button makeGuardianButton;
    public Button makeWorkmanButton;
    public Image makeWorkmanImageTimer;
    public Image makeGuardianImageTimer;
    public Image raidTimeLineImage;
    public Button buyHouseButton;
    public GameObject loseScreen;
    public GameObject guardianImage;
    public GameObject lvl2HouseImage;
    public GameObject lvl3HouseImage;
    public GameObject lvl4HouseImage;
    public GameObject raidScreen;
    public GameObject StartScreen;
    public GameObject WinScreen;
    public int maxWorkers;
    public int maxWorkersIncreasePerhouse;
    public int lvl2houseCost;
    public int lvl3houseCost;
    public int lvl4houseCost;

    public int m_guardianCount = 0;
    public int m_workmanCount;
    public int m_foodCount;
    public int m_foodToWin;
    public int m_foodPerGuardian;
    public int m_foodPerWorkman;
    public int m_workmanCost;
    public int m_guardianCost;
    public float m_guardianCreateTime;
    public float m_workmanCreateTime;
    public float raidMaxTime;
    public float raidMaxTimeIncrease;
    public int nextRaidCount;
    public int raidIncrease;

    public TextMeshProUGUI resultInfoText;

    private int resultGuardians;
    private float resultTime;

    private int houseCount = 1;
    private int raidsCount = 1;
    private float guardianTimer = -2;
    private float workmanTimer = -2;
    private float raidTimer = 0;

    private bool win = false;
    private bool lose = false;
    private bool KOSTYL = true; //при создании самого первого война из еды отнимается 16 единиц я не знаю почему. я перелопатил весь код я не знаю почему
    private bool paused;

    void Start()
    {

        UpdateText();
        paused = true;
        Time.timeScale = 0;
        houseCountText.text = houseCount.ToString();
        houseCost.text = lvl2houseCost.ToString();
        raidsCountText.text = raidsCount.ToString();
    }

    public void HideImages()
    {
        if (m_guardianCount == 0)
            guardianImage.SetActive(false);
             
        if (m_guardianCount > 0)
            guardianImage.SetActive(true);
    }

    void Update()
    {
        resultTime += Time.deltaTime;
        //guardian image hide/show

        //raid timer
        raidTimer += Time.deltaTime;
        raidTimeLineImage.fillAmount = raidTimer/raidMaxTime;

        if (raidTimer >= raidMaxTime)
        {
            //---
            raidGuardiansText.text = m_guardianCount.ToString();
            raidEnemiesText.text = nextRaidCount.ToString();
            raidNumberText.text = "рейд " + raidsCount.ToString();
            //---

            raidTimer = 0;
            m_guardianCount -= nextRaidCount;
            nextRaidCount += raidIncrease;
            guardianFoodText.text = (m_guardianCount * m_foodPerGuardian).ToString();
            raidsCount += 1;

            if(m_foodCount  <  0)
                loseScreen.SetActive(true);

            if(raidsCount > 5)
                raidIncrease = 2;

            raidsCountText.text = raidsCount.ToString();
                raidScreen.SetActive(true);

            //---
            raidguardiansLeftText.text = "осталось войнов - " + m_guardianCount.ToString();
            //---

            raidMaxTime += raidMaxTimeIncrease;
        }

        //create workman timer
        if (workmanTimer > 0)
        {
            workmanTimer -= Time.deltaTime;
                        //makeWorkmanImageTimer.fillAmount = workmanTimer / m_workmanCreateTime;
        }
        else if (workmanTimer > -1)
        {
                         //makeWorkmanImageTimer.fillAmount = 0;
            makeWorkmanButton.interactable = true;
            m_workmanCount += 1;
            workmanTimer = -2;
            workmanFoodText.text = (m_workmanCount * m_foodPerWorkman).ToString();
        }

        //create guardian timer
        if(guardianTimer > 0)
        {
            guardianTimer -= Time.deltaTime;
                     //makeGuardianImageTimer.fillAmount = guardianTimer / m_guardianCreateTime;
        }
        else if(guardianTimer > -1)
        {
                        //makeGuardianImageTimer.fillAmount = 0;
            makeGuardianButton.interactable= true;
            m_guardianCount += 1;
            resultGuardians += 1;
            guardianTimer = -2;

            if(KOSTYL == true)
                m_foodCount += 16;
           
            KOSTYL = false;
            guardianFoodText.text = (m_guardianCount * m_foodPerGuardian).ToString();
        }

        //resouces timer
        if (workmanFoodTimer.Tick)
            m_foodCount += m_workmanCount * m_foodPerWorkman;
        if (guardianFoodTimer.Tick)
        {
            m_foodCount -= m_guardianCount * m_foodPerGuardian;
        }

        //win check
        if (m_foodCount >= m_foodToWin)
        {
            if(!win)
            {
                StartScreen.SetActive(true);
                win = true;
                WinScreen.SetActive(true);
                PauseGame();
            }
        }

        //lose check
        if (m_guardianCount < 0)
        {
            if(!lose)
            {
                loseScreen.SetActive(true);
                resultInfoText.text = "Времени сыграно:   " + (Mathf.Round(resultTime)).ToString() + "секунд" + "\n\n" +
                                      "Рейдов отбито:     " + raidsCount.ToString() + "\n\n" +
                                      "Войнов создано:    " + resultGuardians.ToString();
                lose = true;
            }
        }

        HideImages();
        ButtonsActivities();
        UpdateText();
    }

    public void ButtonsActivities()
    {
        if (m_foodCount < m_guardianCost)
            makeGuardianButton.interactable = false;
        if ((m_foodCount >= m_guardianCost) && (makeGuardianButton.interactable == false) && (guardianTimer == -2))
            makeGuardianButton.interactable = true;

        if (m_foodCount < m_workmanCost)
            makeWorkmanButton.interactable = false;

        if ((m_foodCount >= m_workmanCost)
            && (makeWorkmanButton.interactable == false)
            && (workmanTimer == -2)
            && (m_workmanCount < maxWorkers))
            makeWorkmanButton.interactable = true;

        if ((makeGuardianButton.interactable == false)
            && (m_foodCount >= m_guardianCost)
            && (guardianTimer == -2))
            makeGuardianButton.interactable = true;

        if ((makeWorkmanButton.interactable == false)
            && (m_foodCount >= m_workmanCost)
            && (workmanTimer == -2))
            makeWorkmanButton.interactable = true;

        if ((m_workmanCount == maxWorkers))
            makeWorkmanButton.interactable = false;

        if (!lvl2HouseImage.active)
        {
            if(m_foodCount < lvl2houseCost)
                buyHouseButton.interactable = false;
            else
                buyHouseButton.interactable = true;
        }
        else
        {
            if (!lvl3HouseImage.active)
            {
                if (m_foodCount < lvl3houseCost)
                    buyHouseButton.interactable = false;
                else
                    buyHouseButton.interactable = true;
            }

            else
            {
                if (!lvl4HouseImage.active)
                {
                    if (m_foodCount < lvl4houseCost)
                        buyHouseButton.interactable = false;
                    else
                        buyHouseButton.interactable = true;
                }
            }
        }
    }

    public void BuyHouse()
    {
        if(lvl3HouseImage.active)
        {
            if (m_foodCount >= lvl4houseCost)
            {
                houseCount += 1;
                lvl4HouseImage.SetActive(true);
                maxWorkersIncreasePerhouse *= maxWorkersIncreasePerhouse;
                m_workmanCost *= 4;
                m_guardianCost *= 2;
                maxWorkers += maxWorkersIncreasePerhouse;
                m_foodCount -= lvl4houseCost;
                houseCost.text = "---";
            }
        }

        if ((lvl2HouseImage.active) && (!lvl3HouseImage.active))
        {
            if (m_foodCount >= lvl3houseCost)
            {
                houseCount += 1;
                lvl3HouseImage.SetActive(true);
                maxWorkers += maxWorkersIncreasePerhouse;
                m_foodCount -= lvl3houseCost;
                houseCost.text = lvl4houseCost.ToString();
            }
        }

        if  (!lvl2HouseImage.active)
              if  (m_foodCount >= lvl2houseCost)
            {
                houseCount += 1;
                lvl2HouseImage.SetActive(true);
                maxWorkers += maxWorkersIncreasePerhouse;
                m_foodCount -= lvl2houseCost;
                houseCost.text = lvl3houseCost.ToString();
            }

      
        houseCountText.text = houseCount.ToString();

    }

    public void PauseGame()
    {
        if (paused)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;

        paused = !paused;
    }

    private void UpdateText()
    {
        guardianCountText.text = m_guardianCount.ToString();
        workmanCountText.text = m_workmanCount.ToString() + "/" +  maxWorkers.ToString();
        foodCountText.text = m_foodCount.ToString();

        guardianCostText.text = m_guardianCost.ToString();
        workmanCostText.text = m_workmanCost.ToString();

        RaidCountEnemiesText.text = nextRaidCount.ToString();
        workmanFoodText.text = (m_workmanCount * m_foodPerWorkman).ToString();
        guardianFoodText.text = (m_guardianCount * m_foodPerGuardian).ToString();
    }

    public void MakeWorkman()
    {
        m_foodCount -= m_workmanCost;
        makeWorkmanButton.interactable = false;
        workmanTimer = m_workmanCreateTime;
    }

    public void MakeGuardian()
    {
        m_foodCount -= m_guardianCost;
        makeGuardianButton.interactable = false;
        guardianTimer = m_guardianCreateTime;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
