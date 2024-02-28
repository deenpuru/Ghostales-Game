using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPointerController : MonoBehaviour
{
    private RectTransform pointerRectTransform;
    private RectTransform currentButtonRectTransform;

    void Start()
    {
        // Get the RectTransform component of the pointer GameObject
        pointerRectTransform = GetComponent<RectTransform>();

        // Initially position the pointer at the first button
        currentButtonRectTransform = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
        UpdatePointerPosition();
    }

    void Update()
    {
        // Check if the currently selected button has changed
        if (EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>() != currentButtonRectTransform)
        {
            // Update the reference to the currently selected button
            currentButtonRectTransform = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
            UpdatePointerPosition();
        }
    }

    void UpdatePointerPosition()
    {
        // Update the pointer's position to match the currently selected button
        pointerRectTransform.position = currentButtonRectTransform.position;
    }
}
