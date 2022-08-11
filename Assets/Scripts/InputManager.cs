using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public LayerMask layerMask;

    public RaycastHit2D hit;

    private bool canCalculate;

    private void Awake()
    {
        instance = Singleton.GetInstance<InputManager>();
    }

    private void OnEnable()
    {
        UiManager.instance.onGamePanelUnloaded.AddListener(()=> IgnoreInput(true));
        UiManager.instance.onGamePanelLoaded.AddListener(()=> IgnoreInput(false));
    }

    private void OnDisable()
    {
        UiManager.instance.onGamePanelUnloaded.RemoveListener(()=> IgnoreInput(true));
        UiManager.instance.onGamePanelLoaded.RemoveListener(()=> IgnoreInput(false));
    }

    private void Start()
    {
        IgnoreInput(true);
    }

    private void Update()
    {
        hit = new RaycastHit2D();
        
        if(!canCalculate) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero, Mathf.Infinity, layerMask.value);
        }
    }

    public void IgnoreInput(bool ignore)
    {
        this.canCalculate = !ignore;
    }
}
