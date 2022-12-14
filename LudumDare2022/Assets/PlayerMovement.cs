using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float accel;
    public float fric;

    public float gravMin;
    public float gravMax;

    public int jumpsAmount;
    int jumpsLeft;
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    Vector2 moveInput;
    Rigidbody2D rb;
    float scaleX;
    bool canJump = true;

    float gravityBase;
    float gravityCur;

    Coroutine jumpCoroutine;

    public GameObject body;

    Coroutine deathCoroutine;

    AudioSource audioSource;
    public AudioClip FallScream;
    public AudioClip FallExplosion;
    public AudioClip LandSFX;
    public AudioClip JumpSFX;

    bool prevGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityBase = rb.gravityScale;
        gravityCur = gravityBase;
        scaleX = body.transform.localScale.x;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput.y > 0 && canJump)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
        gravityCur = Mathf.Pow(Mathf.Lerp(1.2f, 0.2f, (float)God.Instance.seconds / God.secondsMax), 2) * gravityBase;
    }

    public void Move()
    {
        Flip();
        float acc = Time.deltaTime * moveInput.x == 0 ? fric : accel;
        bool grounded = IsGrounded();
        if (grounded && !prevGrounded)
        {
            audioSource.PlayOneShot(LandSFX, 0.5f);
            GetComponent<Animator>().SetTrigger("Land");
        }
        prevGrounded = grounded;
        if (!grounded)
        {
            acc *= 0.3f;
        }
        else
        {
            rb.gravityScale = gravityCur;
        }
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, moveInput.x * moveSpeed, acc), rb.velocity.y);
    }

    public void Flip()
    {
        if (moveInput.x > 0)
        {
            body.transform.localScale = new Vector3(scaleX, body.transform.localScale.y, body.transform.localScale.z);
        }
        if (moveInput.x < 0)
        {
            body.transform.localScale = new Vector3((-1) * scaleX, body.transform.localScale.y, body.transform.localScale.z);
        }
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            jumpsLeft = jumpsAmount;
        }
        if (jumpsLeft > 0 && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioSource.PlayOneShot(JumpSFX, 0.3f);
            GetComponent<Animator>().SetTrigger("Jump");
            if (jumpCoroutine != null)
            {
                StopCoroutine(jumpCoroutine);
            }
            jumpCoroutine = StartCoroutine(JumpCoroutine());
            jumpsLeft--;
        }
    }

    IEnumerator JumpCoroutine()
    {
        canJump = false;
        float time = 0f;
        while (true)
        {
            float fallControl = moveInput.y;
            if (time >= 0.5f)
            {
                fallControl = 0f;
                canJump = true;
                if (IsGrounded())
                {
                    break;
                }
            }
            time += Time.deltaTime;
            rb.gravityScale = Mathf.Lerp(gravMax, gravMin, Mathf.Clamp01(fallControl)) * gravityCur;
            yield return null;
        }
        jumpCoroutine = null;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Killbox" && deathCoroutine == null)
        {
            if (!God.Instance.Parachute)
            {
                deathCoroutine = StartCoroutine(FallCoroutine());
            }
            else
            {
                StartCoroutine(God.Instance.EndingCoroutine());
            }
        }
        if (collision.gameObject.tag == "AutoDoor")
        {
            God.Instance.outside = true;
            DoorInteractable.UpdateOutside();
        }
    }
    IEnumerator FallCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        audioSource.PlayOneShot(FallScream);
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(FallExplosion);
        God.Instance.Restart();
    }
}