using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Screen_Transition : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private PlayableAsset _show;
    [SerializeField] private PlayableAsset _hide;

    private bool _isPlayingShow = true;

    private void OnEnable()
    {
        if (_isPlayingShow)
        {
            _isPlayingShow = false;
            _playableDirector.playableAsset = _show;
            _playableDirector.Play();
        }
        else
        {
            _isPlayingShow = true;
            _playableDirector.playableAsset = _hide;
            _playableDirector.Play();
        }   
    }
}
