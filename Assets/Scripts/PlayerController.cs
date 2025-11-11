
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public enum Controls { mobile,pc}

//public class PlayerController : MonoBehaviour
//{


//    public float moveSpeed = 5f;
//    public float jumpForce = 10f;
//    public float doubleJumpForce = 8f;
//    public LayerMask groundLayer;
//    public Transform groundCheck;

//    private Rigidbody2D rb;
//    private bool isGroundedBool = false;
//    private bool canDoubleJump = false;

//    public Animator playeranim;

//    public Controls controlmode;


//    private float moveX;
//    public bool isPaused = false;

//    public ParticleSystem footsteps;
//    private ParticleSystem.EmissionModule footEmissions;

//    public ParticleSystem ImpactEffect;
//    private bool wasonGround;


//   // public GameObject projectile;
//   // public Transform firePoint;

//    public float fireRate = 0.5f; // Time between each shot
//    private float nextFireTime = 0f; // Time of the next allowed shot







//    private void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        footEmissions = footsteps.emission;

//        if (controlmode == Controls.mobile)
//        {
//            UIManager.instance.EnableMobileControls();
//        }


//    }

//    private void Update()
//    {
//        isGroundedBool = IsGrounded();

//        if (isGroundedBool)
//        {
//            canDoubleJump = true; // Reset double jump when grounded

//            if (controlmode == Controls.pc)
//            {
//                moveX = Input.GetAxis("Horizontal");
//            }


//            if (Input.GetButtonDown("Jump"))
//            {
//                Jump(jumpForce);
//            }
//        }
//        else
//        {
//            if (canDoubleJump && Input.GetButtonDown("Jump"))
//            {
//                Jump(doubleJumpForce);
//                canDoubleJump = false; // Disable double jump until grounded again
//            }
//        }

//        if (!isPaused)
//        {
//            // Calculate rotation angle based on mouse position
//            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            Vector3 lookDirection = mousePosition - transform.position;
//            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

//            // ... (your existing code for rotation)

//            // Handle shooting
//            if (controlmode == Controls.pc && Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
//            {
//                Shoot();
//                nextFireTime = Time.time + 1f / fireRate; // Set the next allowed fire time
//            }
//        }
//        SetAnimations();

//        if (moveX != 0)
//        {
//            FlipSprite(moveX);
//        }

//        //impactEffect

//        if(!wasonGround && isGroundedBool)
//        {
//            ImpactEffect.gameObject.SetActive(true);
//            ImpactEffect.Stop();
//            ImpactEffect.transform.position = new Vector2(footsteps.transform.position.x,footsteps.transform.position.y-0.2f);
//            ImpactEffect.Play();
//        }

//        wasonGround = isGroundedBool;


//    }
//    public void SetAnimations()
//    {
//        if (moveX != 0 && isGroundedBool)
//        {
//            playeranim.SetBool("run", true);
//            footEmissions.rateOverTime= 35f;
//        }
//        else
//        {
//            playeranim.SetBool("run",false);
//            footEmissions.rateOverTime = 0f;
//        }

//        playeranim.SetBool("isGrounded", isGroundedBool);

//    }

//    private void FlipSprite(float direction)
//    {
//        if (direction > 0)
//        {
//            // Moving right, flip sprite to the right
//            transform.localScale = new Vector3(1, 1, 1);
//        }
//        else if (direction < 0)
//        {
//            // Moving left, flip sprite to the left
//            transform.localScale = new Vector3(-1, 1, 1);
//        }
//    }
//    private void FixedUpdate()
//    {
//        // Player movement
//        if (controlmode == Controls.pc)
//        {
//            moveX = Input.GetAxis("Horizontal");
//        }



//        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
//    }

//    private void Jump(float jumpForce)
//    {
//        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Zero out vertical velocity
//        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//        playeranim.SetTrigger("jump");
//    }

//    private bool IsGrounded()
//    {
//        float rayLength = 0.25f;
//        Vector2 rayOrigin = new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y - 0.1f);
//        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);
//        return hit.collider != null;
//    }
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if(collision.gameObject.tag == "killzone")
//        {
//            GameManager.instance.Death();
//        }
//    }
























//    //mobile;
//    public void MobileMove(float value)
//    {
//        moveX = value;
//    }
//    public void MobileJump()
//    {
//        if (isGroundedBool)
//        {
//            // Perform initial jump
//            Jump(jumpForce);
//        }
//        else
//        {
//            // Perform double jump if allowed
//            if (canDoubleJump)
//            {
//                Jump(doubleJumpForce);
//                canDoubleJump = false; // Disable double jump until grounded again
//            }
//        }
//    }

//    public void Shoot()
//    {
//        //GameObject fireBall = Instantiate(projectile, firePoint.position, Quaternion.identity);
//        //fireBall.GetComponent<Rigidbody2D>().AddForce(firePoint.right * 500f);
//    }

//    public void MobileShoot()
//    {
//        if (Time.time >= nextFireTime)
//        {
//            Shoot();
//            nextFireTime = Time.time + 1f / fireRate; // Set the next allowed fire time
//        }
//    }

//}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Controls { mobile, pc }

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float doubleJumpForce = 8f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private Rigidbody2D rb;
    private bool isGroundedBool = false;
    private bool canDoubleJump = false;
    private float moveX;

    [Header("Animation & Effects")]
    public Animator playeranim;
    public ParticleSystem footsteps;
    private ParticleSystem.EmissionModule footEmissions;
    public ParticleSystem ImpactEffect;
    private bool wasonGround;

    [Header("Control Mode")]
    public Controls controlmode;
    public bool isPaused = false;

    // 🔒 Nouveau : verrou global des contrôles
    [HideInInspector] public bool controlsLocked = false;

    [Header("Shooting")]
    // public GameObject projectile;
    // public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footEmissions = footsteps.emission;

        if (controlmode == Controls.mobile)
            UIManager.instance.EnableMobileControls();
    }

    private void Update()
    {
        // 🔒 Si les contrôles sont bloqués, stoppe tout
        if (controlsLocked)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            SetAnimations();
            return;
        }

        // --- Contrôles normaux ---
        isGroundedBool = IsGrounded();

        if (isGroundedBool)
        {
            canDoubleJump = true;

            if (controlmode == Controls.pc)
                moveX = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump"))
                Jump(jumpForce);
        }
        else
        {
            if (canDoubleJump && Input.GetButtonDown("Jump"))
            {
                Jump(doubleJumpForce);
                canDoubleJump = false;
            }
        }

        // Tir (PC)
        if (!isPaused && controlmode == Controls.pc && Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        // Animation
        SetAnimations();

        if (moveX != 0)
            FlipSprite(moveX);

        // Impact au sol
        if (!wasonGround && isGroundedBool)
        {
            ImpactEffect.gameObject.SetActive(true);
            ImpactEffect.Stop();
            ImpactEffect.transform.position = new Vector2(footsteps.transform.position.x, footsteps.transform.position.y - 0.2f);
            ImpactEffect.Play();
        }

        wasonGround = isGroundedBool;
    }

    private void FixedUpdate()
    {
        // 🔒 Si bloqué, stop le mouvement physique
        if (controlsLocked)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        if (controlmode == Controls.pc)
            moveX = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump(float force)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        playeranim.SetTrigger("jump");
    }

    private bool IsGrounded()
    {
        float rayLength = 0.25f;
        Vector2 rayOrigin = new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y - 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("killzone"))
            GameManager.instance.Death();
    }

    public void SetAnimations()
    {
        if (moveX != 0 && isGroundedBool)
        {
            playeranim.SetBool("run", true);
            footEmissions.rateOverTime = 35f;
        }
        else
        {
            playeranim.SetBool("run", false);
            footEmissions.rateOverTime = 0f;
        }

        playeranim.SetBool("isGrounded", isGroundedBool);
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // --- MOBILE CONTROLS ---
    public void MobileMove(float value)
    {
        moveX = value;
    }

    public void MobileJump()
    {
        if (controlsLocked) return;

        if (isGroundedBool)
            Jump(jumpForce);
        else if (canDoubleJump)
        {
            Jump(doubleJumpForce);
            canDoubleJump = false;
        }
    }

    public void Shoot()
    {
        // Exemple si tu veux remettre un projectile :
        // GameObject fireBall = Instantiate(projectile, firePoint.position, Quaternion.identity);
        // fireBall.GetComponent<Rigidbody2D>().AddForce(firePoint.right * 500f);
    }

    public void MobileShoot()
    {
        if (controlsLocked) return;

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}
