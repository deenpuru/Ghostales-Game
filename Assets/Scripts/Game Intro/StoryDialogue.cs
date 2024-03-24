using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class StoryDialogue : MonoBehaviour
{
    [Header("General Information")]
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private GameObject _dialogueContrainer;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private string[] _dialogueSentences;
    private int _element = 0;
    private float _dialogueSpeed = 0.03f;
    private bool _canInteract = true;

    [Header("Phase 01")]
    [SerializeField] private PlayableAsset _timelinePart01;
    [SerializeField] private float _delayUntilDialogueShows01;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FirstPhase());
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _canInteract == true)
        {
            FirstPhase();
            NextSentence();
        }
    }
    IEnumerator FirstPhase()
    {
        _dialogueText.text = "";
        _playableDirector.playableAsset = _timelinePart01;
        _playableDirector.Play();
        yield return new WaitForSeconds((float)_timelinePart01.duration + _delayUntilDialogueShows01);
        _dialogueContrainer.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        foreach (char Character in _dialogueSentences[_element].ToCharArray())
        {
            _dialogueText.text += Character;
            yield return new WaitForSeconds(_dialogueSpeed);
        }
        _element++;

        //yield return new WaitForSeconds(0.2f);
        // _dialogueText.text = "";
 
    }

    private void NextSentence()
    {
        if (_element <= _dialogueSentences.Length - 1)
        {
            _canInteract = false;
            _dialogueText.text = "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            _dialogueContrainer.SetActive(false);
        }
     
    }
    
    private IEnumerator WriteSentence()
    {
        foreach (char Character in _dialogueSentences[_element].ToCharArray())
        {
            _dialogueText.text += Character;
            yield return new WaitForSeconds(_dialogueSpeed);
        }
        _element++;
        _canInteract = true;
    }
   
}
