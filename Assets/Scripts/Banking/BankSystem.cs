using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BankSystem : MonoBehaviour
{
    
    private Coroutine DelayNextTry;
    private int _money;
    private void OnEnable()
    {

    }

    public int TryAskForMoney()
    {
        int money = Random.Range(50, 150);
        if (money < 90)
        {
            return 0;
        }

        return money;
    }

}
