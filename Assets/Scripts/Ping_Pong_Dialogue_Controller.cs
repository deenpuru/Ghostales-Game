using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ping_Pong_Dialogue_Controller : MonoBehaviour
{
    
    [SerializeField] private GameObject _dialogueContainer;

    [Space(20)]
    [Header("-------------------Dialogue Texts-------------------")]
    [Space (20)]

    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private string[] _sentences;
    [SerializeField] private float _dialogueSpeed;

    [Space(20)]
    [Header("-------------------Character's Name-------------------")]
    [Space(20)]

    [SerializeField] private TextMeshProUGUI _characterNameText;
    [SerializeField] private string[] _characterNameTurn;

    [Space(20)]
    [Header("-------------------Character's Image-------------------")]
    [Space(20)]

    [SerializeField] private Image _portraitImageDisplayArea;
    [SerializeField] private Sprite[] _spriteTurn;

    [Space(20)]
    [Header("-------------------Character's Animation-------------------")]
    [Space(20)]

    [SerializeField] private Animator _indicatorAnimation;

    [Space(20)]
    [Header("-------------------Player's Reference-------------------")]
    [Space(20)]

    [SerializeField] private PunkKid_MainPlayerControls _kid;

    private int _element = 0;

    private bool _canInteract = true;
    private bool _areaTrigger;
    private bool _cutDialogue = false;


    /**************************************************************************/

    // Character behavior when entering trigger area

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
            _areaTrigger = true;
            _indicatorAnimation.SetBool("Hide", false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
            _areaTrigger = false;
            _indicatorAnimation.SetBool("Hide", true);
        }
    }

    /**************************************************************************/

    // Dialogue system behavior

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _canInteract == true && _areaTrigger == true)
        {
            _cutDialogue = false;
            StopPlayer();
            _dialogueContainer.SetActive(true);
            _indicatorAnimation.SetBool("Hide", true);
            NextSentence();
        }
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            _cutDialogue = true;
            EndDialogueText();
        }
    }
    private void EndDialogueText()
    {
        _indicatorAnimation.SetBool("Hide", false);
        _dialogueText.text = "";
        _element = 0;
        _canInteract = true;
        _dialogueContainer.SetActive(false);
        _kid.IsAbleToMove = true;
        
    }
    private void StopPlayer()
    {
        _kid.IsAbleToMove = false;
        _kid.movement.x = 0;
        _kid.movement.y = 0;
        _kid.animator.SetFloat("Speed", 0);
    }

    private void NextSentence()
    {
        if (_element <= _sentences.Length - 1) 
        {
            _portraitImageDisplayArea.sprite = _spriteTurn[_element];
            _characterNameText.text = _characterNameTurn[_element];
            _canInteract = false;
            _dialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            EndDialogueText();
        }
    }


        private IEnumerator WriteSentence()
        {
            foreach (char Character in _sentences[_element].ToCharArray())
            {
                _dialogueText.text += Character;
                 yield return new WaitForSeconds(_dialogueSpeed);

                if (_cutDialogue == true)
                {
                    yield break;
                }
               
            }

            _element++;
            _canInteract = true;
        }

}
