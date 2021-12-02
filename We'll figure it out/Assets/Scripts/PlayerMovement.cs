using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Image Player;
    public Texture2D cursorTexture, FullCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public Sprite DownSpriteMan, UpSpriteMan;
    public Canvas Stir, Skim, Ladel, Cutscene;
    public int StartingPercentage = 50, boilingHeat=1, PlayerStrength=1,otherGoal=0, otherGoalGoal=10, otherGoalNumber=0, otherGoalGoalNumber=3;
    private int currentPercentage,CountDown;
    public Slider stirPercentage,skimPercentage;
    private bool lost = false;
    public Text oGTXT, sTXT,CutsceneText;
    void Start()
    {
        StartCoroutine("OpeningCutscene");
        stirPercentage.value = StartingPercentage;
        currentPercentage = StartingPercentage;
        /*StartCoroutine("StirGoingDown");
        oGTXT.text = otherGoal + "/" + otherGoalGoal + " Right click when this is complete";*/
    }

    // Update is called once per frame
    void Update()
    {
        if (otherGoalNumber == otherGoalGoalNumber)
        {
            oGTXT.text = otherGoal + "/" + otherGoalGoal + " Press space when complete.";
        }

        else
        {
            oGTXT.text = otherGoal + "/" + otherGoalGoal + " Next Task Meter, right click when complete";
        }
        stirPercentage.value = currentPercentage;
        if (Input.GetMouseButtonDown(0)&&!lost){
            currentPercentage += PlayerStrength;
            Player.sprite = DownSpriteMan;
            //otherGoal += 1;
        }
        if (Input.GetMouseButtonUp(0) && !lost)
        {
            Player.sprite = UpSpriteMan;
        }
        if (Input.GetMouseButtonDown(1) && !lost)
        {
            if (otherGoal < otherGoalGoal)
            {
                Debug.Log("loss");
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(otherGoalNumber == otherGoalGoalNumber && otherGoal <= otherGoalGoal + 2 && otherGoal >= otherGoalGoal - 1)
            {
                Debug.Log("One 'point' (Ladeled)");
                otherGoal = 0;
                otherGoalNumber = 0;
            }
            else
            {
                Debug.Log("loss");
                lost = true;
            }
        }
        if (currentPercentage <= 10 || currentPercentage >= 90)
        {
            Debug.Log("loss");
            lost = true;
        }
        if (otherGoal >= otherGoalGoal + 2)
        {
            Debug.Log("Loss");
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
    IEnumerator OpeningCutscene()
    {
        CutsceneText.text = "Sugar was a valuable commodity that Europeans had trouble making between the 1700’s-1800’s. " +
            "This required work with a plant that they never mastered, as they were more familiar with things like wheat. " +
            "Work that would be done through slavery, as they were the ones familiar with sugarcane, the plant used for it.";
        yield return new WaitForSeconds(21f);
        CutsceneText.text = "Slaves would use billhooks to harvest them, cutting into the ground to pull them out before cutting the top and the leaves off before loading them onto carts. " +
            "The plants went to the mill, where they would be squeezed of their juice. " +
            "Slaves in the mill were often fatigued and at risk of losing an arm.";
        yield return new WaitForSeconds(18f);
        CutsceneText.text = "Afterwards, the juice would go to the boiling house where it would be boiled and skimmed until it could be transferred to a smaller vat; " +
            "the cycle repeats until a thick viscous sugar liquid is produced. " +
            "That was then drained, and the leftovers were dried.";
        yield return new WaitForSeconds(18f);
        CutsceneText.text = "This left them with a decent-sized sugar crystal that would be broken down before being sold. " +
            "The heat and intensive labor was grueling to the point where could have the potential to give slaves heatstroke or worse. " +
            "You are a slave working at the final step of this portion of the process.";
        yield return new WaitForSeconds(18f);
        Stir.gameObject.SetActive(true);
        Cutscene.gameObject.SetActive(false);
        StartCoroutine("StirGoingDown");
        oGTXT.text = otherGoal + "/" + otherGoalGoal + " Right click when this is complete";

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
            yield return new WaitForSeconds(1);
            CountDown--;
            sTXT.text = "Hurry with skimming! " + CountDown;
            if (CountDown < 0&&skimPercentage.value<100)
            {
                lost = true;
                Debug.Log("Lost");
            }
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
