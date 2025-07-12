using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private UnitBehaviour _unit;

    public Action<string> OnMessageUpdated = delegate { };
    
    private string _currentMessage;
    private PlayerInputUI _playerInputUI;
    
    private void Start()
    {
        PlayerInputReciver.Instance.OnFilteredButtonPressed += OnButtonPressed;
        _playerInputUI = PlayerInputUI.Instance;
        MessageUpdated();
    }

    private void OnDisable()
    {
        PlayerInputReciver.Instance.OnFilteredButtonPressed -= OnButtonPressed;
    }

    private void OnButtonPressed(string button)
    {
        InputHandler(button);
    }

    private void InputHandler(string button)
    {
        if (button == "Space")
        {
            HandleSpaceBarPressed();
        }
        else if (button == "Backspace")
        {
            HandleBackSpace();
        }
        else
        {
            AddLetterToWord(button);
        }

        MessageUpdated();
    }

    private void HandleBackSpace()
    {
        ClearMessage();
    }

    public void ClearMessage()
    {
        _currentMessage = "";
    }    
    
    private void HandleSpaceBarPressed()
    {
        if (_currentMessage.Length > 0)
        {
            if (_currentMessage.Last() != ' ')
            {
                _currentMessage += ' ';
            }
        }
    }

    private void AddLetterToWord(string letter)
    {
        _currentMessage += letter;
    }

    private void MessageUpdated()
    {
        OnMessageUpdated?.Invoke(_currentMessage);
        _playerInputUI.UpdateMessageUI(_currentMessage);
      

    }
}
