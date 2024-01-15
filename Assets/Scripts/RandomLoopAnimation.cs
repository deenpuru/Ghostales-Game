using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoopAnimation : MonoBehaviour
{

    [SerializeField] private int _minRange;
    [SerializeField] private int _maxRange;
    [SerializeField] private string _defaultAnimationTrigger;
    [SerializeField] private string _interactedAnimationTrigger;


    [Space(20)]
    [Header("-------------------Character's Animation-------------------")]
    [Space(20)]

    [SerializeField] private Animator _trashCanWhileInteracting;

    [SerializeField] private Ping_Pong_Dialogue_Controller _dialogueController;

    private bool _notInteracted = true;

    //Trash can NPC loop animation

    private void Start()
    {
        StartCoroutine(LoopAnimation());
    }

    private void Update()
    {
        if (_dialogueController._checkDialogueON && _notInteracted)
        {
            _notInteracted = false;
            _trashCanWhileInteracting.SetTrigger(_defaultAnimationTrigger);
        }
    }
    private void PauseAnimation()
    {
       if (_dialogueController._checkDialogueON)
        {
            _trashCanWhileInteracting.SetTrigger(_interactedAnimationTrigger);
        }
      
    }


    private IEnumerator LoopAnimation()
    {
        while (_notInteracted)
        {
                int testRandomTime = Random.Range(_minRange, _maxRange);
                GetComponent<Animator>().SetTrigger(_defaultAnimationTrigger);
                yield return new WaitForSeconds(testRandomTime);
        }
    }
}
