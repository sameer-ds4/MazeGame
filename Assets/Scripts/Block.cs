using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject[] walls;

    public void Del_Walls(int index)
    {
        walls[index].SetActive(false);
    }
}

public enum Status
{
    Open,
    Occupied,
    Current
}
