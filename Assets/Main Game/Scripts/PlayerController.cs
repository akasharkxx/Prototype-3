using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10.0f;
    public float gravityModifier = 1.0f;

    public bool isGrounded = true;
    public bool gameOver = false;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private Rigidbody playerRb;
    private Animator playerAnimator;
    private AudioSource playerAudio;

    private int jumpTriggerHash;
    private int deathBoolHash;
    private int deathIntHash;

    private void Start()
    {
        //Setup Animator
        playerAnimator = GetComponent<Animator>();
        
        //Setup Rigidbody and gravity
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0.0f, -9.8f, 0.0f);
        Physics.gravity *= gravityModifier;

        //Setup Aduio Source
        playerAudio = GetComponent<AudioSource>();

        //Optimized Animator
        jumpTriggerHash = Animator.StringToHash("Jump_trig");
        deathBoolHash = Animator.StringToHash("Death_b");
        deathIntHash = Animator.StringToHash("DeathType_int");
    }

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isGrounded && !gameOver)
        {
            //Add Jump force using Unity Physics
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            //Ensure to diable jump while in air
            isGrounded = false;
            
            //Dirt particle stop while jumping
            dirtParticle.Stop();
            
            //Play Jump animation
            playerAnimator.SetTrigger(jumpTriggerHash);

            //Play Jump Sound
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            //Dirt Particle if running on ground
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacles"))
        {
            gameOver = true;
            Debug.Log("Game Over!");

            //Particles
            explosionParticle.Play();
            dirtParticle.Stop();

            //Play clip when crashes
            playerAudio.PlayOneShot(crashSound, 1.0f);

            //Animation
            playerAnimator.SetBool(deathBoolHash, true);
            playerAnimator.SetInteger(deathIntHash, 1);

            this.enabled = false;
        }
    }
}
