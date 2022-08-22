using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IPlayer
{
    public static Player Instance;
    public bool canMove = false;
    public bool dead = false;
    public CharacterController playerController;
    public PlayerController controller;
    public float health = 100;
    public float maxHealth = 100;
    [SerializeField] private Image healthBar;

    private void Awake()
    {
        Instance = this;
        playerController = GetComponent<CharacterController>();
        controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (dead)
        {
            canMove = false;
        }
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public void Damage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            dead = true;
        }
    }
}