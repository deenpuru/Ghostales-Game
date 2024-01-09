using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoopAnimation : MonoBehaviour
{
    public bool _notInteracted;

    [SerializeField] private int _minRange;
    [SerializeField] private int _maxRange;
    [SerializeField] private string _animationTrigger;

    [Space(20)]
    [Header("-------------------Character's Animation-------------------")]
    [Space(20)]

    [SerializeField] private Animator _trashCanWhileInteracting;

    //Trash can NPC loop animation

    private void Start()
    {
        StartCoroutine(LoopAnimation());
    }

    private IEnumerator LoopAnimation()
    {
        while (true)
        {
            if (_notInteracted)
            {
                int testRandomTime = Random.Range(_minRange, _maxRange);
                GetComponent<Animator>().SetTrigger(_animationTrigger);
                yield return new WaitForSeconds(testRandomTime);
            }
            else if (!_notInteracted)
            {
                _trashCanWhileInteracting.SetTrigger("isBlinking");
                //yield return null;
            }
           
        }
    }
}
