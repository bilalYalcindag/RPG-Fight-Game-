using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 7.0f;
    public float rotationSpeed = 100.0f;
    public LayerMask groundLayers;
    public float groundCheckDistance = 0.2f;
    public Transform groundCheck;
    public Slider playerHealthSlider;
    public Slider playerUltiSlider;
    public float health = 100;
    public ParticleSystem ultiParticle;

    private float maxVerticalAngle = 30;
    private Animator playerAnimator;
    private bool isGrounded;
    private float yVelocity = 0.0f;
    private float gravity = -9.81f;
    private float verticalRotation = 0.0f;
    private Camera camera;
    

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        camera = FindAnyObjectByType<Camera>();
    }
    void Update()
    {
        Move();
        Turn();
        Jump();
        PlayerAttack();
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerAnimator.SetFloat("InputY", 1);
        }
        else
        {
            playerAnimator.SetFloat("InputY", 0);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            
            playerAnimator.SetFloat("InputX",1);
            
        }
        else
        {
            playerAnimator.SetFloat("InputX", 0);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerAnimator.SetFloat("InputX", 1);
        }
        else
        {
            playerAnimator.SetFloat("InputX", 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 15;
        }
        else
        {
            speed = 10;
        }
    }

    private void Turn()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotationInput = Input.GetAxis("Mouse Y");

        Vector3 horizontalTurn = new Vector3(0.0f, horizontalRotation, 0.0f) * rotationSpeed * Time.deltaTime;

        // Yatay dönüþ
        transform.Rotate(horizontalTurn, Space.World);

    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayers, QueryTriggerInteraction.Ignore);

        if (isGrounded && yVelocity < 0)
        {
            yVelocity = 0f;
            playerAnimator.SetBool("IsInAir", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            yVelocity += Mathf.Sqrt(jumpForce * -2f * gravity);
            playerAnimator.SetBool("IsInAir", true);
        }

        yVelocity += gravity * Time.deltaTime;
        transform.Translate(Vector3.up * yVelocity * Time.deltaTime, Space.World);

      
    }

    public void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetTrigger("Attack");
            

        }
        else if (Input.GetMouseButtonDown(1) && playerUltiSlider.value>=100)
        {
            playerAnimator.SetTrigger("Ability");
            
            ParticleSystem particleInstance = Instantiate(ultiParticle, gameObject.transform);

            Debug.Log(particleInstance.name);
            Destroy(particleInstance, 1f);
            playerUltiSlider.value = 0;
        }

    }

    public void PlayerDamage(float damage)
    {
        health -= damage;
        playerHealthSlider.value = health;

        if(health <= 0)
        {
           playerHealthSlider.value = 0;
            
        }
    }


}
