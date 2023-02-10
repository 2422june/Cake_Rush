using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public PlayerController GetPlayer { get { return player;} }
    PlayerController player;
    public int CurrentUnit { get; set; }
    public int[] HaveResource { get; private set; } = new int[3];
} 
