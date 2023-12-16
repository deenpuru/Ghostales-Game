using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoopAnimation : MonoBehaviour
{
    public bool _notInteracted;

    [SerializeField] private int _minRange;
    [SerializeField] private int _maxRange;
    [SerializeField] private string _animationTrigger;

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
                yield return null;
            }
           
        }
    }
}
