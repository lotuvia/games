using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Animations : MonoBehaviour
{
    //worman sprites
    public Sprite _1WorkmanSprite;
    public Sprite _2WorkmanSprite;

    //guardian sprites
    public Sprite _1GuardianSprite;
    public Sprite _2GuardianSprite;
    public Sprite _3GuardianSprite;
    public Sprite _4GuardianSprite;
    public Sprite _5GuardianSprite;

    //enemy sprites
    public Sprite _1EnemyAtackSprite;
    public Sprite _2EnemyAtackSprite;
    public Sprite _3EnemyAtackSprite;

    public float maxTimerWorman;
    public float maxTimerGuardian;
    public float maxTimerGuardianAtack;
    public float maxTimerEnemy;

    private int currentGuardianSprite = 1;
    private int currentGuardianAtackSprite = 1;
    private int currentEnemyAtackSprite = 1;

    public Image workmanImage;
    public Image guardianImage;
    public Image guardianAtackImage;
    public Image enemyImage;

    //private Image img;
    private bool firstSpriteCheck = true;
    private float anTimerWorkman = 0f;
    private float anTimerGuardian = 0f;
    private float anTimerGuardianAtack = 0f;
    private float anTimerEnemy = 0f;

    void Start()
    {
       // img = GetComponent<Image>();
    }

    void Update()
    {
        #region workman
        anTimerWorkman += Time.deltaTime;
        if ((anTimerWorkman > maxTimerWorman))
        {
            if (firstSpriteCheck == true)
            {
                workmanImage.sprite = _2WorkmanSprite;
                firstSpriteCheck = !firstSpriteCheck;
            }
            else
            {
                workmanImage.sprite = _1WorkmanSprite;
                firstSpriteCheck = !firstSpriteCheck;
            }
            anTimerWorkman = 0f;
        }
        #endregion

        #region guardian
        anTimerGuardian += Time.deltaTime;
        if ((anTimerGuardian > maxTimerGuardian))
        {
            if (currentGuardianSprite == 1)
            {
                guardianImage.sprite = _2GuardianSprite;
                currentGuardianSprite = 2;
            }
            else if (currentGuardianSprite == 2)
            {
                guardianImage.sprite = _1GuardianSprite;
                currentGuardianSprite = 1;
            }
            anTimerGuardian = 0f;
        }
        #endregion

        #region guardianAtack
        anTimerGuardianAtack += Time.deltaTime;
        if ((anTimerGuardianAtack > maxTimerGuardianAtack))
        {
            if (currentGuardianAtackSprite == 1)
            {
                guardianAtackImage.sprite = _2GuardianSprite;
                currentGuardianAtackSprite = 2;
            }
            else if (currentGuardianAtackSprite == 2)
            {
                guardianAtackImage.sprite = _3GuardianSprite;
                currentGuardianAtackSprite = 3;
            }
            else if (currentGuardianAtackSprite == 3)
            {
                guardianAtackImage.sprite = _4GuardianSprite;
                currentGuardianAtackSprite = 4;
            }
            else if (currentGuardianAtackSprite == 4)
            {
                guardianAtackImage.sprite = _5GuardianSprite;
                currentGuardianAtackSprite = 5;
            }
            else if (currentGuardianAtackSprite == 5)
            {
                guardianAtackImage.sprite = _1GuardianSprite;
                currentGuardianAtackSprite = 1;
            }
            anTimerGuardianAtack = 0f;
            #endregion
        }


        anTimerEnemy += Time.deltaTime;
         if (anTimerEnemy > maxTimerEnemy)
        {
            if (currentEnemyAtackSprite == 1)
            {
                enemyImage.sprite = _2EnemyAtackSprite;
                currentEnemyAtackSprite = 2;
            }
            else if (currentEnemyAtackSprite == 2)
            {
                enemyImage.sprite = _3EnemyAtackSprite;
                currentEnemyAtackSprite = 3;
            }
            else if (currentEnemyAtackSprite == 3)
            {
                enemyImage.sprite = _1EnemyAtackSprite;
                currentEnemyAtackSprite = 1;
            }
            anTimerEnemy = 0f;
        }
    }
}
    