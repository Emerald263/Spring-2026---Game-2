using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour
{

    //audio variables
    public AudioSource soundEffects;
    public AudioClip[] sounds; // Public variable to access the Audio Source component

    //Animation variables
    Animator anim;
    public bool idle;
    public bool attck;
    public float Enemyidle;
    public float Enemyattck;

    [SerializeField] TextScript dialogueBox;


    public enum Battlestates
    {
        Start,
        PlayerAction,
        EnemyMove,
        Busy,

    }


    Battlestates state;
    int CurrentActionBattle;
    int CurrentMove;



    // Start is called before the first frame update
    private void Start()
    {



        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableMoveSelector(false);

        SetupBattle();



        StartCoroutine(SetupBattle());

    }


    // Update is called once per frame
    private void Update()
    {

        if (state == Battlestates.PlayerAction)
        {

            HandleActionSelection();

        }
 
        if (Input.GetKeyDown(KeyCode.L))
        {

            
        }



    }
    void HandleActionSelection()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentActionBattle < 1)
                ++CurrentActionBattle;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentActionBattle > 0)
                --CurrentActionBattle;

        }

        dialogueBox.UpdateActionSelection(CurrentActionBattle);


        if (Input.GetKeyDown(KeyCode.E))
        {

            if (CurrentActionBattle == 0)
            {

                //Fight
                Action();
            }

            if (CurrentActionBattle == 1)
            {

                //Run
                StartCoroutine(BattleFlee());
            }



        }

    }




    void Action()
    {
        Debug.Log("Action");
        state = Battlestates.PlayerAction;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableMoveSelector(true);
        HandleMovementSelection();
    

    }

    void HandleMovementSelection()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentMove < 1)
                ++CurrentMove;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentMove > 0)
                --CurrentMove;

        }

        dialogueBox.UpdateMoveSelection(CurrentMove);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (CurrentMove == 0)
            {
                Debug.Log("Attacked");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelector(false);
                state = Battlestates.Busy;
                StartCoroutine(Attack());


            }

            if (CurrentMove == 1)
            {

                Debug.Log("Boom");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelector(false);
                state = Battlestates.Busy;
                StartCoroutine(Special());

            }



        }

    }

    public IEnumerator Attack()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Attacked"));
        yield return new WaitForSeconds(5f);
        {





            EnemyAction();
        }
    }

    public IEnumerator Special()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Boom"));
        yield return new WaitForSeconds(5f);
        {





            EnemyAction();
        }
    }



    void EnemyAction()
    {
        state = Battlestates.EnemyMove;
        dialogueBox.EnableDialogueText(true);

        StartCoroutine(EnemyAttack());



    }

    public IEnumerator EnemyAttack()
    {
        for (int i = 0; i < 3; i++)
        {

            yield return StartCoroutine(dialogueBox.TypeDialogue($"The Enemy Attacked"));
            yield return new WaitForSeconds(5f);



        }

        Action();

    }

    public void enemydeath()
    {

        StartCoroutine(BattleEndWin());

    }


    public IEnumerator SetupBattle()
    {


        yield return StartCoroutine(dialogueBox.TypeDialogue($"Parasite"));
        yield return new WaitForSeconds(1f);

        Playeraction();
    }

    void Playeraction()
    {

        state = Battlestates.PlayerAction;
        StartCoroutine(dialogueBox.TypeDialogue("Choose an action"));
        dialogueBox.EnableActionSelector(true);
    }

    public IEnumerator BattleFlee()
    {
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You fled"));
        {
            //yield return new WaitForSeconds(1);
            SceneManager.LoadScene(1);

        }
    }

    public IEnumerator BattleEndWin()
    {
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You Won! Gained 50exp and 15gp!"));
        yield return new WaitForSeconds(1f);
        {
            //yield return new WaitForSeconds(1);
            SceneManager.LoadScene(1);

        }


    }

    public IEnumerator BattleEndLose()
    {
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You Lost"));
        yield return new WaitForSeconds(1f);
        {
            //yield return new WaitForSeconds(1);
            SceneManager.LoadScene(2);
  
        }


    }


}
