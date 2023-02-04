using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private int money;
    public int Money { get { return money; } set { money = value; } }
}
