using UnityEngine;
using static BattleManager;
using System;

public class FlowerSprites : MonoBehaviour
{

    private SpriteRenderer Flower;

    //sprite variables
    public Sprite FlowerIdle;
    public Sprite FlowerAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Flower.sprite = FlowerIdle;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
