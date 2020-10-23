﻿using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public abstract class Health : MonoBehaviour {
    public static string ANIMATION_HIT = "hit";
    public static string ANIMATION_DEAD = "dead";
    private Animator animator;

    [SerializeField]
    protected int health;

    //the health as the game starts
    private int initialHealth;

    //the explosion when something is destroyed
    [SerializeField]
    private GameObject explosionPrefab;
    //object that holds the explosionPrefab
    private GameObject explosion;

    //bool that determines whether or not the character is dead
    protected bool isDead;

    void Awake()
    {
        //the character isn't dead at the start of the game so isDead is false
        isDead = false;

        initialHealth = health;
    }

    void Start()
    {
        //instantiate the explosionPrefab and store it in explosion
        explosion = Instantiate(explosionPrefab);
        explosion.SetActive(false);
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        //each time the object is disabled then enabled i have to make sure the character is considered alive
        //and their health is restored to its initial value
        isDead = false;
        health = initialHealth;
    }

    void Update()
    {
        //if the health is less than 0 and the player isn't dead...
        if (health <= 0 && !isDead)
        {
            //...then kill them
            DieWhithAnimation();
        }

        //if the health drops below 0 then push it back up to 0
        if (health < 0) {
            health = 0; 
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    //take away a certain amount of health depending on the parameter passed in
    public void Damage(int damage)
    {
        health -= damage;
        try
        {
        ChangeAnimation(ANIMATION_HIT);
        //print("DAMAGE INIMIGO");
        }
        catch (System.Exception)
        {
            
            throw;
        }
        
    }

    //method to kill the character
    public void DieWhithAnimation()
    {
        //changes isDead to true to confirm the character is dead...
        isDead = true;
        //...puts their health to 0 just to be sure...
        health = 0;
        //...move the explosion to the characters position and rotation...
        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;
        //...enables the explosion, disables the character and disables the explosion in 0.5f seconds
        //explosion.SetActive(true);
        //gameObject.SetActive(false);
        Invoke("DisableWhithAnimation", 1f);
        ChangeAnimation(ANIMATION_DEAD);
    }
    public void DieWithAD()
    {
        //print("MOrreu 1 Inicio");
        //changes isDead to true to confirm the character is dead...
        isDead = true;
        //...puts their health to 0 just to be sure...
        health = 0;
        //...move the explosion to the characters position and rotation...
        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;
        //...enables the explosion, disables the character and disables the explosion in 0.5f seconds
        //explosion.SetActive(true);
        //gameObject.SetActive(false);
        Invoke("DisableWhithAnimation", 1.5f);
        ChangeAnimation(ANIMATION_DEAD);
        Invoke("showAdInter", 1f);
        //print("MOrreu 1 FIM");
    }

    private void showAdInter() {
        //print("showAdInter Inicio");
        RewardedAdsScript.getInstance().ShowRewardedVideo();
        //print("showAdInter FIM");
    }

    public void Disable()
    {
        explosion.SetActive(false);
    }
    public void DisableWhithAnimation()
    {
        //print("DisableWhithAnimation Inicio");
        //explosion.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ChangeAnimation(string animation) {
        animator.SetTrigger(animation);
    }
}