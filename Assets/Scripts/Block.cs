using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject[] walls;

    public GameObject coinGold;

    private int[] odds = { 0, 1, 2 };

    private void Start()
    {
        CoinSpawn();
    }

    public void Del_Walls(int index)
    {
        walls[index].SetActive(false);
    }

    private void CoinSpawn()
    {
        if (Random.Range(0, odds.Length) == 0)
            coinGold.SetActive(true);
    }

}
