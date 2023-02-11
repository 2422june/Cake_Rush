using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager
{
    public delegate void KeyAction(KeyCode keyCode);
    public KeyAction KeyActionHandler = null;

    public void OnUpdate()
    {
        if(Input.anyKey && KeyActionHandler != null)
        {

        }
    }
}
