using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_RunSpeed;
    [SerializeField] private float m_JumpScale;
    [SerializeField] private float m_JumpTime;

    private float defaultSpeed;
    private float defaultJumpScale;
    private float runSpeed;
    private float jumpSpeed;
    private float jumpSpeedWhileRunnig;

    private Collider2D col;
    private Rigidbody2D rg;
    private Vector2 position;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        col = rg.GetComponentInChildren<Collider2D>();
        defaultSpeed = m_Speed;
        defaultJumpScale = m_JumpScale;
        runSpeed = m_RunSpeed + m_Speed;
        jumpSpeed = m_Speed * m_JumpScale;
        jumpSpeedWhileRunnig = jumpSpeed + runSpeed;

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
    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D col = collision.transform.GetComponent<Collider2D>();
        if (col != null)
        {
            position = transform.position;
            if (Input.GetKeyDown(KeyCode.Space) == false)
            {
                if (position != null)
                {
                    transform.position = position;
                }
                
            }
        }
    }
    */

    private IEnumerator Jumping()
    {
        col.enabled = false;
        yield return new WaitForSeconds(m_JumpTime);
        m_Speed = defaultSpeed;
        col.enabled = true;
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
            Debug.Log(m_Speed);
            StartCoroutine(Jumping());
        }
    }
    

    // Update is called once per frame
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
