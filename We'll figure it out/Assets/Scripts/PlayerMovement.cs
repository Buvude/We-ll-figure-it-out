using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Canvas Stir, Skim, Ladel;
    public int StartingPercentage = 50, boilingHeat=1, PlayerStrength=1,otherGoal=0, otherGoalGoal=10, otherGoalNumber=0, otherGoalGoalNumber=3;
    private int currentPercentage,CountDown;
    public Slider stirPercentage,skimPercentage;
    private bool lost = false;
    public Text oGTXT, sTXT;
    void Start()
    {
        stirPercentage.value = StartingPercentage;
        currentPercentage = StartingPercentage;
        StartCoroutine("StirGoingDown");
        oGTXT.text = otherGoal + "/" + otherGoalGoal + " Right click when this is complete";
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
            //otherGoal += 1;
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
                StopCoroutine("StirGoDown");
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
            StartCoroutine("StirGoingDown");
            Skim.gameObject.SetActive(false);
            Stir.gameObject.SetActive(true);
            skimPercentage.value = 0;
            stirPercentage.value = 50;
        }
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
}
