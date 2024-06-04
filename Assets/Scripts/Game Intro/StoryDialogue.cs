using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class DialoguePhase
{
    public PlayableAsset timeline;
    public string[] dialogueSentences;
}

public class StoryDialogue : MonoBehaviour
{
    [Header("General Information")]
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private GameObject _dialogueContainer;
    [SerializeField] private GameObject _portraitGameObject;
    [SerializeField] private GameObject _portraitNameGameObject;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private float _delayUntilDialogueShows;


    //remember to set this number back to 0 - the reason it's on 5 is because you wanted to work on the main menu without going through all the phases when in PLAY MODE
    [Header("set it to zero DONT FORGET!!!!")]
    [SerializeField] private int _currentPhaseIndex = 0;
    

    [Header("Phases")]
    [SerializeField] private List<DialoguePhase> _dialoguePhases;

    private int _currentSentenceIndex = 0;
    private float _dialogueSpeed = 0.03f;
    private bool _canInteract = false;

    void Start()
    {
        if (_dialoguePhases.Count > 0)
        {
            StartCoroutine(PlayPhase(_currentPhaseIndex));
        }
        else
        {
            Debug.LogError("No dialogue phases set in the inspector");
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && _canInteract)
        {
            NextSentence();
        }
    }

    private IEnumerator PlayPhase(int phaseIndex)
    {
        _currentSentenceIndex = 0;
        DialoguePhase phase = _dialoguePhases[phaseIndex];

        _dialogueText.text = "";
        _playableDirector.playableAsset = phase.timeline;
        _playableDirector.Play();
        if (phase.dialogueSentences.Length > 0)
        {
            yield return new WaitForSeconds((float)phase.timeline.duration + _delayUntilDialogueShows);

            if (_currentPhaseIndex == 4)
            {
                _portraitGameObject.SetActive(false);
                _portraitNameGameObject.SetActive(false);
                _dialogueText.fontSize = 40;
                _dialogueText.alignment = TextAlignmentOptions.Center;
                _dialogueText.alignment = TextAlignmentOptions.Midline;
                _dialogueText.margin = new Vector4(10, -11f, 10, 8.5f);
            }

            _dialogueContainer.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            NextSentence();
        }
        else
        {
            yield return new WaitForSeconds((float)phase.timeline.duration);
            _currentPhaseIndex++;

            if (_currentPhaseIndex < _dialoguePhases.Count)
            {
                StartCoroutine(PlayPhase(_currentPhaseIndex));
            }
        }
    }

    private void NextSentence()
    {
        if (_currentPhaseIndex < _dialoguePhases.Count)
        {
            DialoguePhase currentPhase = _dialoguePhases[_currentPhaseIndex];
            if (_currentSentenceIndex < currentPhase.dialogueSentences.Length)
            {
                _canInteract = false;
                _dialogueText.text = "";
                StartCoroutine(WriteSentence(currentPhase.dialogueSentences));
            }
            else
            {
                _dialogueContainer.SetActive(false);
                _currentPhaseIndex++;

                if (_currentPhaseIndex < _dialoguePhases.Count)
                {
                    StartCoroutine(PlayPhase(_currentPhaseIndex));
                }
            }
        }
    }

    private IEnumerator WriteSentence(string[] sentences)
    {
        foreach (char character in sentences[_currentSentenceIndex].ToCharArray())
        {
            _dialogueText.text += character;
            yield return new WaitForSeconds(_dialogueSpeed);
        }
        _currentSentenceIndex++;
        _canInteract = true;
    }
}
