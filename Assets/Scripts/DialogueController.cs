using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private string[] Sentences;
    [SerializeField] private float DialogueSpeed;
    [SerializeField] private Image PortraitImageDisplayArea;
    [SerializeField] private Sprite _portraitSprite;
    [SerializeField] private GameObject dialogueContainer;

    private int _element = 0;
    private int exampleDebug = 0;

    private bool canClick = true;
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start: _element = " + _element);
        PortraitImageDisplayArea.sprite = _portraitSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && canClick) 
        {
            dialogueContainer.SetActive(true);
            NextSentence();
        }
    }

    private void NextSentence()
        {
            //check = false;
            if (_element <= Sentences.Length - 1) 
            {
                // Debug
                exampleDebug = Sentences.Length - 1;
                Debug.Log("is _element (" + _element + ") < or = to the length of Sentences - 1? (" + Sentences.Length + " - 1 = " + exampleDebug +") - TRUE" );
                //
                canClick = false;
                DialogueText.text = "";
                StartCoroutine(WriteSentence());
            }
            else
            {
                Debug.Log("is _element (" + _element + ") < or = to the length of Sentences - 1? (" + Sentences.Length + " - 1 = " + exampleDebug + ") - FALSE");
                DialogueText.text = "";
                _element = 0;
                canClick = true;
            }
           
        }


        private IEnumerator WriteSentence()
        {
            foreach (char Character in Sentences[_element].ToCharArray())
            {
                DialogueText.text += Character;
                yield return new WaitForSeconds(DialogueSpeed);
            }
            _element++;
            Debug.Log("_element = " + _element);
            canClick = true;
        }

   
         
      
}
