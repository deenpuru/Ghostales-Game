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
    [SerializeField] private GameObject _spriteIndicator;
    [SerializeField] private PunkKid_MainPlayerControls kid;

    private int _element = 0;

    private bool canClick = true;
    private bool _areaTrigger;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
            _areaTrigger = true;
            _spriteIndicator.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
            _areaTrigger = false;
            _spriteIndicator.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start: _element = " + _element);
        PortraitImageDisplayArea.sprite = _portraitSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)
            &&
            canClick == true
            &&
            _areaTrigger == true)

        {
            StopPlayer();
            dialogueContainer.SetActive(true);
            NextSentence();
        }
    }

    private void StopPlayer()
    {
        kid.IsAbleToMove = false;
        kid.movement.x = 0;
        kid.movement.y = 0;
        kid.animator.SetFloat("Speed", 0);
    }

    private void NextSentence()
        {
            if (_element <= Sentences.Length - 1) 
            {
                canClick = false;
                DialogueText.text = "";
                StartCoroutine(WriteSentence());
                
        }
            else
            {
                DialogueText.text = "";
                _element = 0;
                canClick = true;
                dialogueContainer.SetActive(false);
                kid.IsAbleToMove = true;
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
            canClick = true;
        }

   
         
      
}
