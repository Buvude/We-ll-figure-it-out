using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Text CutsceneText;
    public bool PressToContinue;
    public Image cutsceneBackground, background2, background1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("OpeningCutscene");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)||Input.GetKey(KeyCode.LeftControl))
        {
            PressToContinue = true;
        }
    }
    IEnumerator OpeningCutscene()
    {
        CutsceneText.text = "Sugar was a valuable commodity that Europeans had trouble making between the 1700’s-1800’s.\n" +
            "The production of sugar required work with sugar cane, a plant that they were unfamiliar with.\n" +
            "[Click to Skip]";
        PressToContinue = false;
        for (int i = 0; i < 21; i++)
        {
            yield return new WaitForSeconds(1f);
            if (PressToContinue)
            {
                break;
            }
        }
       
        CutsceneText.text = "Instead, slaves in the Caribbean and America were forced to produce sugar for them.\n"+
            "Slaves would use hoes to create square holes in the soil for the sugarcane to be planted in, a grueling process known as cane-holing.\n" +
            "[Click to Skip]";
        PressToContinue = false;
        for (int i = 0; i < 18; i++)
        {
            yield return new WaitForSeconds(1f);
            if (PressToContinue)
            {
                break;
            }
        }
        cutsceneBackground.enabled = false;
        background2.enabled = true;
        CutsceneText.text = " Come harvest, slaves used billhooks, sharp curved knives, to harvest the sugarcane, cutting the canes six inches from the ground, before cutting the top and leaves off.\n" +
            "The plants were then bundled with string, loaded onto carts, \n" +
            "and sent to the mill, where they would be squeezed of their juice..\n" +
            "[Click to Skip]";
        PressToContinue = false;
        for (int i = 0; i < 18; i++)
        {
            yield return new WaitForSeconds(1f);
            if (PressToContinue)
            {
                break;
            }
        }
        background2.enabled = false;
        background1.enabled = true;
        CutsceneText.text = "Afterwards, the juice would go to the boiling house where it would be boiled and skimmed, then it would be transferred to a smaller vat; this cycle was repeated, making the liquid thicker and darker each time, until it was about to crystallize.\n" +
            "Then it would be transferred to an unheated vat to cool.\n" +
            "[Click to Skip]";
        PressToContinue = false;
        for (int i = 0; i < 18; i++)
        {
            yield return new WaitForSeconds(1f);
            if (PressToContinue)
            {
                break;
            }
        }
        CutsceneText.text = " The heat and intensive labor in the boiling house was exhausting, sometimes resulting in heatstroke or worse.\n" +
            "The sugar liquid was then stored in clay pots for several days and drained, leaving sugar crystals behind, which would be dried in the sun and then sent off to be sold.\n" +
            "[Click to skip]";
        cutsceneBackground.enabled = false;
        background2.enabled = true;
        SceneManager.LoadScene(2);

    }
}
