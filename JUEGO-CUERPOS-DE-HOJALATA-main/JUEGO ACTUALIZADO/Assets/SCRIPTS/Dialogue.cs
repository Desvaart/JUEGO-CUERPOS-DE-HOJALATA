using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour

{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;

    private float typingTime = 0.05f;

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    private animacionKungFu animK;

    void Start()
    {
        animK = GameObject.Find("maquina_kungfu").GetComponent<animacionKungFu>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( animK.talk)
        {
            dialoguePanel.SetActive(true);

            if (!didDialogueStart)
             
            {
                StartDialogue();
            }



            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
                
            }
            //else 
            //{
            //    StopAllCoroutines();
            //    dialogueText.text = dialogueLines[lineIndex];
            //}
        }

        if (dialogueText.text == dialogueLines[lineIndex] && !animK.talk)
        {
            dialoguePanel.SetActive(false);
        }


    }

    private void StartDialogue() 
    { 
    didDialogueStart = true;  
    dialoguePanel.SetActive(true);
    dialogueMark.SetActive(false);
    lineIndex = 0;
        //Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine() 
    {
     lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            //didDialogueStart=false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(false);
            //Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //    isPlayerInRange = true;
    //        dialogueMark.SetActive(true);
    //        Debug.Log("Se puede iniciar un dialogo");
    //}
    //}

    //private void OnTriggerExit(Collider collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        isPlayerInRange = false;
    //        dialogueMark.SetActive(false);
    //        Debug.Log("No se puede iniciar un dialogo");
    //    }
    //}
}
