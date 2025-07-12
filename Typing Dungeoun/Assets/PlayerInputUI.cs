using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInputUI : MonoBehaviour
{
    public static PlayerInputUI Instance;
    [SerializeField] private TextMeshProUGUI _text;
    
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMessageUI(string message)
    {
        _text.text = message;
    }
}
