using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan_Animation : MonoBehaviour
{
    public Animator TrashCanAnim;
    private float RandomPlay;

    private void Start()
    {
        RandomDelay();
    }

    private void RandomDelay()
    {
        RandomPlay = Random.Range(0f, 10f);
       
    }




}
