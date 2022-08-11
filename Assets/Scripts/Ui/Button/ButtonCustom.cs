using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Button))]
public class ButtonCustom : MonoBehaviour
{
    public Button Button {get; private set;}
    
    private UnityEvent onClick = new UnityEvent();

    protected virtual void Start() 
    {
        Button = GetComponent<Button>();
        
        Button.onClick.AddListener(Play);
    } 
    
    public void MakeInteractable(bool isInteractable)
    {
        Button.interactable = isInteractable;
    }

    public void AddToButtonEvent(Action action)
    {
        onClick.AddListener(()=> action.Invoke());
    }

    public void RemoveFromButtonEvent(Action action)
    {
        onClick.RemoveListener(()=> action.Invoke());
    }

    public void Play()
    {
        onClick.Invoke();
    }
}
