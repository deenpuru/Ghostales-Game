using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Color _hoverColor;
    [SerializeField] private Color _normalColor;
    [SerializeField] private TextMeshProUGUI[] _buttons;

    [Space(20)]

    [SerializeField] Transform _pointerPosition;

    [Space(20)]

    public Vector3 _firstButton;
    public Vector3 _secondButton;
    public Vector3 _thirdButton;


    private TextMeshProUGUI _currentButton;
    private int _element = 0;

    private void Start()
    {
        _pointerPosition.transform.localPosition = _firstButton;
        _currentButton = _buttons[_element]; 
        _currentButton.color = _hoverColor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            if (_element < _buttons.Length - 1) // if( 1 < 3-1 ===== 2 ............ 1<2)  _buttons[0], _buttons[1], _buttons[2] 
            {
                _element++;
            }
            else
            {
                _element = 0;
            }
            MovePointer();
            _currentButton.color = _normalColor;
            _currentButton = _buttons[_element];
            _currentButton.color = _hoverColor;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {   

            if (_element > 0)
            {
                _element--;
            }
            else
            {
                _element = _buttons.Length - 1;
            }
            MovePointer();
            _currentButton.color = _normalColor;
            _currentButton = _buttons[_element];
            _currentButton.color = _hoverColor;                              
        }
    }

    private void MovePointer()
    {
        switch (_element)
        {
            case 0:

                _pointerPosition.transform.localPosition = _firstButton;
                break;

            case 1:

                _pointerPosition.transform.localPosition = _secondButton;
                break;

            case 2:

                _pointerPosition.transform.localPosition = _thirdButton;
                break;

        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Town");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
