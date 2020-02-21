using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public float jumpSpeed;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public Vector3 respawnPosition;
    public float knockbackForce;
    public float knockbackFrames;
    public AudioSource jumpSound;
    public AudioSource hurtSound;
    public float onPlatformSpeedNerf;
    public Rigidbody2D myRB;
    public bool canMove;
    public SpriteRenderer theSpriteRenderer;
    public GameObject sword;

    public float iframes;

    private float iframesTimer;


    private bool onPlatform;
    private float currentMoveSpeed;
    private float knockbackCounter;
    public Animator myAnimator;
    private LevelManager theLevelManager;

    void Start()
    {

        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        respawnPosition = transform.position;
        theLevelManager = FindObjectOfType<LevelManager>();
        currentMoveSpeed = moveSpeed;
        canMove = true;
        theSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetKeyDown(KeyCode.F))
        {
            myAnimator.Play("playerattack");
            StartCoroutine(Attack());
        }

        if (knockbackCounter <= 0f && canMove)
        {
            if (onPlatform)
            {
                currentMoveSpeed = moveSpeed * onPlatformSpeedNerf;
            }
            else
            {
                currentMoveSpeed = moveSpeed;
            }

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRB.velocity = new Vector3(currentMoveSpeed, myRB.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRB.velocity = new Vector3(-currentMoveSpeed, myRB.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                myRB.velocity = new Vector3(0f, myRB.velocity.y, 0f);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myRB.velocity = new Vector3(myRB.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
            }

        }

        if (knockbackCounter > 0f)
        {
            knockbackCounter -= Time.deltaTime;

            if (transform.localScale.x > 0f)
            {
                myRB.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            }
            else
            {
                myRB.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
            }
        }

        if (iframesTimer > 0f)
        {
            iframesTimer -= Time.deltaTime;
            theLevelManager.invincibilityFrames = true;
        }

        if (iframesTimer <= 0f)
        {
            theLevelManager.invincibilityFrames = false;
        }

        myAnimator.SetFloat("speed", Mathf.Abs(myRB.velocity.x));
        myAnimator.SetBool("grounded", isGrounded);
    }

    public IEnumerator Attack()
    {
        sword.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        sword.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "KillPlane")
        {
            theLevelManager.Respawn();
        }

        if (other.tag == "Checkpoint")
        {
            respawnPosition = other.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
            onPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            onPlatform = false;
        }
    }

    public void Knockback()
    {
        knockbackCounter = knockbackFrames;
        theLevelManager.invincibilityFrames = true;
        StartCoroutine(IFramesFlash());
    }

    public IEnumerator IFramesFlash()
    {
        iframesTimer = iframes;
        while (iframesTimer > 0f)
        {
            theSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(.1f);
            theSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(.1f);
        }
    }
}
