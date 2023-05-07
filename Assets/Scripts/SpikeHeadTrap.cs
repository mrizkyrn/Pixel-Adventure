using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadTrap : MonoBehaviour
{
    [SerializeField] private float hitCooldown = 10f;
    private float lastHitTime;

    [SerializeField] private float smashDistance = 1f; 
    [SerializeField] private float smashTime = 1f;
    [SerializeField] private float backTime = 5f;
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private Vector2 direction = Vector2.down;

    private Vector2 originalPosition;
    private Vector2 targetPosition;
    public bool isSmashed;
    private float elapsedTime;
    private float delayStartTime;


    private float blinkCooldown = 3f;
    private float blinkStartTime;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();

        blinkStartTime = Time.time;
        lastHitTime = Time.time;

        originalPosition = transform.position;
        isSmashed = false;

        targetPosition = originalPosition + direction * smashDistance;
    }

    private void Update()
    {
        if (Time.time >= blinkStartTime + blinkCooldown)
        {
            animator.SetTrigger("Blink");
            blinkStartTime = Time.time;
        }

        if (!isSmashed && Time.time >= lastHitTime + hitCooldown)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / smashTime);

            transform.position = Vector2.Lerp(originalPosition, targetPosition, normalizedTime);

            if (normalizedTime >= 1f)
            {
                isSmashed = true;
                animator.SetTrigger("BottomHit");
                delayStartTime = Time.time;
                elapsedTime = 0f;
            }
        }

        if (isSmashed && Time.time >= delayStartTime + delayTime)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / backTime);

            transform.position = Vector2.Lerp(targetPosition, originalPosition, normalizedTime);

            if (normalizedTime >= 1f)
            {
                lastHitTime = Time.time;
                isSmashed = false;
                elapsedTime = 0f;
            }
        }
    }

    private void PlayHitAnimation()
    {
        if (direction == Vector2.down)
            animator.SetTrigger("BottomHit");
        else if (direction == Vector2.left)
            animator.SetTrigger("LeftHit");
        else if (direction == Vector2.right)
            animator.SetTrigger("RightHit");
        else if (direction == Vector2.up)
            animator.SetTrigger("TopHit");
    }
}
