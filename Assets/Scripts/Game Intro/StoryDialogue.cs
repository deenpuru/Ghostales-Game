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
    [SerializeField] private string[] _dialogueSentencesPhase02;
    [SerializeField] private float _delayUntilDialogueShows;

    private int _element = 0;
    private float _dialogueSpeed = 0.03f;
    private bool _canInteract = false;

    [Header("Phase 01")]
    [SerializeField] private PlayableAsset _timelinePart01;

    [Header("Phase 02")]
    [SerializeField] private PlayableAsset _timelinePart02;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FirstPhase());
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _canInteract == true)
        {
            NextSentence();
        }
    }
    IEnumerator FirstPhase()
    {
        _dialogueText.text = "";
        _playableDirector.playableAsset = _timelinePart01;
        _playableDirector.Play();
        yield return new WaitForSeconds((float)_timelinePart01.duration + _delayUntilDialogueShows);
        _dialogueContrainer.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        NextSentence();   
    }

    IEnumerator SecondPhase()
    {
        Debug.Log("phase 2 is playing");
        yield return new WaitForSeconds(1f);
        _dialogueText.text = "";
        _playableDirector.playableAsset = _timelinePart02;
        _playableDirector.Play();
        yield return new WaitForSeconds((float)_timelinePart02.duration + _delayUntilDialogueShows);
        _dialogueContrainer.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        if (_element <= _dialogueSentencesPhase02.Length - 1)
        {
            _canInteract = false;
            _dialogueText.text = "";
            StartCoroutine(WriteSentencePhase02());
        }
        else
        {
            _dialogueContrainer.SetActive(false);
            //StartCoroutine(SecondPhase());
        }
       
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
            StartCoroutine(SecondPhase());
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

    private IEnumerator WriteSentencePhase02()
    {
        foreach (char Character in _dialogueSentencesPhase02[_element].ToCharArray())
        {
            _dialogueText.text += Character;
            yield return new WaitForSeconds(_dialogueSpeed);
        }
        _element++;
        _canInteract = true;
    }
}
