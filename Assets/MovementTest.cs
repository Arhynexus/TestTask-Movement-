using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MovementTest : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_RunSpeed;
    [SerializeField] private float m_JumpScale;
    [SerializeField] private float m_JumpTime;

    private float defaultSpeed;
    private float runSpeed;
    private float jumpSpeed;
    private float jumpSpeedWhileRunnig;

    private Collider2D playerCollider;
    private Rigidbody2D rg;
    void Start()
    {
        SetStartStats();
    }

    private void SetStartStats()
    {
        defaultSpeed = m_Speed;
        runSpeed = m_RunSpeed + m_Speed;
        jumpSpeed = m_Speed * m_JumpScale;
        jumpSpeedWhileRunnig = jumpSpeed + runSpeed;

        rg = GetComponent<Rigidbody2D>();
        playerCollider = rg.GetComponentInChildren<Collider2D>();
        rg.freezeRotation = true;
    }

    public void SpeedUp()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetKeyDown(KeyCode.Space) == false)
        {
            m_Speed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            m_Speed = defaultSpeed;
        }
        
    }

    private IEnumerator Jumping()
    {
        playerCollider.enabled = false;
        yield return new WaitForSeconds(m_JumpTime);
        m_Speed = defaultSpeed;
        playerCollider.enabled = true;
        yield return null;
    }
    
    private void JumpWhileRunnig()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetKey(KeyCode.Space) == true)
        {
            m_Speed = jumpSpeedWhileRunnig;
            StartCoroutine(Jumping());
        }
    }
    public void Jump()
    {
        if (Input.GetKey(KeyCode.LeftShift) ==  false && Input.GetKeyDown(KeyCode.Space) == true)
        {
            m_Speed = jumpSpeed;
            StartCoroutine(Jumping());
        }
    }

    void Update()
    {
        float hotizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        SpeedUp();
        Jump();
        JumpWhileRunnig();

        rg.velocity = new Vector3(hotizontal * m_Speed, vertical * m_Speed, 0);
        rg.angularVelocity = 0;
    }
}
