using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ping_Pong_Dialogue_Controller : MonoBehaviour
{
    
    [SerializeField] private GameObject _dialogueContainer;

    [Space(10)]
    [Header("Dialogue Texts")]
    [Space (5)]

    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private string[] _sentences;
    [SerializeField] private float _dialogueSpeed;

    [Space(10)]
    [Header("Character's Name")]
    [Space(5)]

    [SerializeField] private TextMeshProUGUI _characterNameText;
    [SerializeField] private string _characterName;

    [Space(10)]
    [Header("Character's Image")]
    [Space(5)]

    [SerializeField] private Image _portraitImageDisplayArea;
    [SerializeField] private Sprite _portraitSprite;
    [SerializeField] private GameObject _spriteIndicator;
    [SerializeField] private Sprite _playerPortraitSprite;

    [Space(10)]
    [Header("Player's Reference")]
    [Space(5)]

    [SerializeField] private PunkKid_MainPlayerControls _kid;

    [Space(10)]
    [Header("Portrait's Display Turn")]
    [Space(5)]

    [SerializeField] private Sprite[] _spriteTurn;

    private int _element = 0;

    private bool _canInteract = true;
    private bool _areaTrigger;
    private bool _cutDialogue = false;

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

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _canInteract == true && _areaTrigger == true)
            {
                _cutDialogue = false;
                StopPlayer();
                _characterNameText.text = _characterName;
                _portraitImageDisplayArea.sprite = _portraitSprite;
                _dialogueContainer.SetActive(true);
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
            
            Debug.Log(_sentences[_element]);
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
