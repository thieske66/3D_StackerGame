using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    public InputActionAsset InputControls;
    private string actionMap = "StackControls";
    private string actionName = "PlaceLayer";
    private InputAction placeLayerAction;

    public StackController StackController;


    private void Awake()
    {
        placeLayerAction = InputControls.FindActionMap(actionMap).FindAction(actionName);
        registerInputAction();
    }

    private void OnEnable()
    {
        
        placeLayerAction.Enable();
    }

    private void registerInputAction()
    {
        placeLayerAction.performed += placeLayer;
    }

    private void placeLayer(InputAction.CallbackContext obj)
    {
        StackController.PlaceActiveLayer();
    }
}
