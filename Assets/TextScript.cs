using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;



    [SerializeField] Text dialogueText;
    [SerializeField] GameObject actionselector;
    [SerializeField] GameObject moveselector;
    [SerializeField] GameObject movedetails;
    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTexts;
    [SerializeField] List<Text> moves;

    [SerializeField] Text descriptionF;
    [SerializeField] Text descriptionH;

    public void SetDialogue(string dialogue)
    {

        dialogueText.text = dialogue;

    }

    public IEnumerator TypeDialogue(string dialogue)
    {

        dialogueText.text = "";
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

    }

    public void EnableDialogueText(bool enabled)
    {
        dialogueText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionselector.SetActive(enabled);
    }



    public void EnableMoveSelector(bool enabled)
    {
        moveselector.SetActive(enabled);
        movedetails.SetActive(enabled);
    }



    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if (i == selectedAction)
                actionTexts[i].color = highlightedColor;

            else
                actionTexts[i].color = Color.black;
        }

    }

    public void UpdateMoveSelection(int CurrentMove)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i == CurrentMove)
                moveTexts[i].color = highlightedColor;

            else
                moveTexts[i].color = Color.black;
        }

    }


    public void SetMoveName()
    {
        for (int i = 0; i < moves.Count; ++i)
        {

            if (i < moves.Count)
                moveTexts[i].text = "-";
            else
                moveTexts[i].text = "-";

        }

    }

}
