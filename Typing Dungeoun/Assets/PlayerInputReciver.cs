using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReciver : MonoBehaviour
{
    public static PlayerInputReciver Instance;

    public Action<string> OnFilteredButtonPressed = delegate { };

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            // A-Z keys
            if (key >= KeyCode.A && key <= KeyCode.Z && Input.GetKeyDown(key))
            {
                OnFilteredButtonPressed?.Invoke(key.ToString());
            }

            // Space
            if (key == KeyCode.Space && Input.GetKeyDown(key))
            {
                OnFilteredButtonPressed?.Invoke(key.ToString());
            }

            // Backspace
            if (key == KeyCode.Backspace && Input.GetKeyDown(key))
            {
                OnFilteredButtonPressed?.Invoke(key.ToString());
            }
        }
    }
}
