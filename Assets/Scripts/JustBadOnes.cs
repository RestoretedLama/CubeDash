using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustBadOnes : MonoBehaviour
{
    private GameManager gameManager;

    public int LifeKiller;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        gameManager.LifeCounter(LifeKiller);
    }
}
