using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public DisplayBar healthBar;

    private Rigidbody2D rb;

    public float knockbackForce = 5f;

    public GameObject playerDeathEffect;

    public static bool hitRecently = false;

    public float hitRecoveryTime = 0.2f;

    //Audio
    private AudioSource playerAudio;
    public AudioClip playerHitSound;
    public AudioClip playerDeathSound;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on player");

        }

        healthBar.SetMaxValue(health);

        hitRecently = false;
    }

    public void Knockback(Vector3 enemyPosition)
    {
        if (hitRecently)
        {
            return;
        }

        hitRecently = true;

        StartCoroutine(RecoverFromHit());

        Vector2 direcion = transform.position - enemyPosition;

        direcion.Normalize();

        direcion.y = direcion.y * 0.5f + 0.5f;

        rb.AddForce(direcion * knockbackForce, ForceMode2D.Impulse);
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(hitRecoveryTime);

        hitRecently = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.SetMaxValue(health);

        if(health <= 0)
        {
            Die();
        }
        else
        {
            playerAudio.PlayOneShot(playerHitSound);
        }

    }

    public void Die()
    {
        playerAudio.PlayOneShot(playerDeathSound);
        ScoreManager.gameOver = true;

        GameObject deathEffect = Instantiate(playerDeathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffect, 2f);

        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
