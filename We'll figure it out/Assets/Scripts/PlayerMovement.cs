using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool paused;
    public Slider playerSlider;
    public Button rPot, lPot, chutebtn;
    public bool PressToContinue, LadelFull, LadelMode, sugarInLadelCooked, cookedSugarOut;
    public int chute, LeftPot, RightPot;
    public Image Player, cutsceneBackground, background1, background2, PlayerSliderImage;
    public Texture2D cursorTexture, FullCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public Sprite DownSpriteMan, UpSpriteMan, emptyimage, fullImage;
    public Canvas Stir, Skim, Ladel, Cutscene, gameOverMenu, pauseMenu;
    public int StartingPercentage = 50, boilingHeat=1, PlayerStrength=1,otherGoal=0, otherGoalGoal=10, otherGoalNumber=0, otherGoalGoalNumber=3;
    private int currentPercentage,CountDown;
    public Slider stirPercentage,skimPercentage;
    private bool lost = false;
    public Text oGTXT, sTXT,GameOverReason;
    void Start()
    {
        Time.timeScale = 1;
        stirPercentage.value = StartingPercentage;
        currentPercentage = StartingPercentage;
        StartCoroutine("StirGoingDown");
        oGTXT.text = otherGoal + "/" + otherGoalGoal + " Right click when this is complete";
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            playerSlider.interactable = false;
        }
        else if (!paused)
        {
            playerSlider.interactable = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = true;
            //activate pause menu
        }
        if (!paused)
        {
            if (LadelMode && !lost)
            {
                if (RightPot == 0 && LeftPot == 3)
                {
                    Stir.gameObject.SetActive(true);
                    Ladel.gameObject.SetActive(false);
                    LeftPot = 3;
                    RightPot = 3;
                    StartCoroutine("StirGoingDown");
                    LadelMode = false;
                }
                if (Input.GetKeyDown(KeyCode.Space)&&!paused)
                {
                    if (playerSlider.value >= .244 && playerSlider.value <= .466)
                    {
                        SmallPotPressed();
                    }
                    else if (playerSlider.value >= .689 && playerSlider.value <= .907)
                    {
                        LargePotPressed();
                    }
                    else if (playerSlider.value <= .087)
                    {
                        chutePressed();
                    }
                }
                if (LadelFull)
                {
                    PlayerSliderImage.GetComponent<Image>().sprite = fullImage;
                }
                else if (!LadelFull)
                {
                    PlayerSliderImage.GetComponent<Image>().sprite = emptyimage;
                }
                rPot.GetComponentInChildren<Text>().text = RightPot.ToString();
                lPot.GetComponentInChildren<Text>().text = LeftPot.ToString();
            }
            else if (!LadelMode)
            {
                mouseCursorChangeToDefaultLadle();
            }
            if (otherGoalNumber == otherGoalGoalNumber)
            {
                oGTXT.text = otherGoal + "/" + otherGoalGoal + " Press space when complete.";
            }

            else
            {
                oGTXT.text = otherGoal + "/" + otherGoalGoal + " Next Task Meter, right click when complete";
            }
            stirPercentage.value = currentPercentage;
            if (Input.GetMouseButtonDown(0) && !lost && !paused)
            {
                currentPercentage += PlayerStrength;
                Player.sprite = DownSpriteMan;
                //otherGoal += 1;
            }
            if (currentPercentage >= 90 || currentPercentage <= 10)
            {
                GameOverReason.text = "loss reason: Sugar was left alone to long or was overstirred. Sugar was burned or you were burned by splashing sugar";
                lost = true;
            }
            if (Input.GetMouseButtonUp(0) && !lost && !paused)
            {
                Player.sprite = UpSpriteMan;
            }
            if (Input.GetMouseButtonDown(1) && !lost && !paused)
            {
                if (otherGoal < otherGoalGoal)
                {
                    GameOverReason.text = "loss reason: moved onto next stage to early, sugar was burned";
                    lost = true;
                }
                else
                {
                    //Debug.Log("Skimmed");
                    //StopCoroutine("StirGoDown");
                    StartCoroutine("HurryWithSkim");
                    Stir.gameObject.SetActive(false);
                    Skim.gameObject.SetActive(true);
                    otherGoal = 0;

                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && !LadelMode && !paused)
            {
                if (otherGoalNumber == otherGoalGoalNumber && otherGoal <= otherGoalGoal + 2 && otherGoal >= otherGoalGoal - 1)
                {
                    LadelMode = true;
                    StopAllCoroutines();
                    Stir.gameObject.SetActive(false);
                    Ladel.gameObject.SetActive(true);

                    otherGoal = 0;
                    otherGoalNumber = 0;
                }
                else
                {
                    GameOverReason.text = "loss reason: Moved onto next stage to early, sugar burned";
                    lost = true;
                }
            }
            /*if (currentPercentage <= 10 || currentPercentage >= 90)
            {
                Debug.Log("loss reason: Sugar was left alone to long or was overstirred. Sugar was burned or you were burned by splashing sugar");
                lost = true;
            }*/
            if (otherGoal >= otherGoalGoal + 2)
            {
                GameOverReason.text = "Loss reason: moved to skimming to early, sugar burned";
                lost = true;
            }
            if (skimPercentage.value == 100)
            {
                StopCoroutine("HurryWithSkim");
                otherGoal = 0;
                otherGoalNumber++;
                //StartCoroutine("StirGoingDown");
                Skim.gameObject.SetActive(false);
                Stir.gameObject.SetActive(true);
                skimPercentage.value = 0;
                stirPercentage.value = 50;
            }
        }
        if (lost)
        {
            Time.timeScale = 0;
            stirPercentage.value = 50;
            gameOverMenu.gameObject.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
        {
            if (paused)
            {
                Unpause();
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.gameObject.SetActive(true);
                paused = true;
            }
        }
    }
    public void RestartScene()
    {
        Unpause();
        lost = false;
        //Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        paused = false;
    }
    public void quit()
    {
        Application.Quit();
    }
   
    IEnumerator StirGoingDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            currentPercentage -= boilingHeat;
            otherGoal++;
        }
        
    }
    IEnumerator HurryWithSkim()
    {
        CountDown = 3;
        while (true)
        {
            if (!paused)
            {
                yield return new WaitForSeconds(1);
                CountDown--;
                sTXT.text = "Hurry with skimming! " + CountDown;
                if (CountDown < 0 && skimPercentage.value < 100)
                {
                    lost = true;

                    GameOverReason.text = "Skimming took to long and the sugar was burnt"; 
                }
            }
        }
    }
    public void chutePressed()
    {
        if (sugarInLadelCooked&&LadelFull&&!lost)
        {
            LadelFull = false;
            sugarInLadelCooked = false;
            if (LeftPot == 0)
            {
                cookedSugarOut = true;
            }
        }
        else if (!sugarInLadelCooked && LadelFull && !lost)
        {
            lost = true;
            GameOverReason.text="Loss reason: Sent unfinished Sugar onto next stage";
        }
    }
    public void SmallPotPressed()
    {
        if(LadelFull && !lost && cookedSugarOut)
        {
            LadelFull = false;
            LeftPot++;
        }
        else if(LadelFull && !lost && !cookedSugarOut&&!sugarInLadelCooked)
        {
            lost = true;
            GameOverReason.text="Loss reason, uncooked sugar mixxed with cooked sugar";
        }
        else if (!cookedSugarOut && !lost && !LadelFull)
        {
            LadelFull = true;
            LeftPot--;
            sugarInLadelCooked = true;
        }
        if(LadelFull&&sugarInLadelCooked && !lost)
        {
            LadelFull = false;
            LeftPot++;
            sugarInLadelCooked = false;
        }
        if (!LadelFull && !lost&&!cookedSugarOut)
        {
            LeftPot--;
            LadelFull = true;
            sugarInLadelCooked = true;
        }


    }
    public void LargePotPressed()
    {
        if(LadelFull&&sugarInLadelCooked && !lost)
        {
            lost = true;
            GameOverReason.text="Loss reason: Mixxed cooked and uncooked sugar";
        }
        if(LadelFull && !lost && !sugarInLadelCooked)
        {
            RightPot++;
            LadelFull = false;
            sugarInLadelCooked = false;
        }
        if(!LadelFull && !lost)
        {
            LadelFull = true;
            RightPot--;
            sugarInLadelCooked = false;
        }
    }
    public void mouseCursorChangeToEmptyLadle()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void mouseCursorChangeToFullLadle()
    {
        Cursor.SetCursor(FullCursor, hotSpot, cursorMode);
    }

    public void mouseCursorChangeToDefaultLadle()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }

}
