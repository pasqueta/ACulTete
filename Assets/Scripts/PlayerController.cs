using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidbody2D = null;
    [SerializeField] private BoxCollider2D m_groundChecker = null;

    [SerializeField] private float m_speed = 10.0f;
    [SerializeField] private float m_jumpSpeed = 5.0f;
    [SerializeField] private LayerMask m_layerGround;

    Vector2 m_currentVelocity = Vector2.zero;
    private bool m_doJump = false;
    private bool m_isGround = false;
    Vector2 moveDirection = Vector2.zero;
    InputActionControls inputActionController = null;


    #region UNITY
    private void Reset()
	{
        m_rigidbody2D.GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Awake()
    {
        inputActionController = new InputActionControls();
    }

	private void Start()
	{
        inputActionController.Player.Jump.performed += _ => Jump();
    }

	private void OnEnable()
	{
        inputActionController.Enable();
    }
    private void OnDisable()
    {
        inputActionController.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        m_currentVelocity = m_rigidbody2D.velocity;

        /*if(Input.GetKey(KeyCode.Q))
		{
            m_currentVelocity.x = -m_speed;
        }
        else if(Input.GetKey(KeyCode.D))
		{
            m_currentVelocity.x = m_speed;
        }
		else
		{
            m_currentVelocity.x = 0.0f;
        }*/

        moveDirection.x = inputActionController.Player.Move.ReadValue<Vector2>().x;

        m_currentVelocity.x = moveDirection.x * m_speed;

        /*if (m_isGround)
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space))
            {
                m_currentVelocity.y = m_jumpSpeed;
            }
        }*/
    }

    void FixedUpdate()
    {
        m_isGround = IsGround();

        m_rigidbody2D.velocity = m_currentVelocity;
    }
    #endregion

    bool IsGround()
	{
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapBoxAll(m_groundChecker.transform.position, new Vector2(m_groundChecker.size.x, m_groundChecker.size.y), 0.0f);

        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        m_isGround = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (1 << colliders[i].gameObject.layer == m_layerGround.value)
                {
                    m_isGround = true;
                    break;
                }
            }
        }

        Debug.DrawLine(m_groundChecker.transform.position, m_groundChecker.transform.position + Vector3.down, m_isGround ? Color.blue : Color.red);
        
        return m_isGround;
	}

    void Jump()
	{
        if (m_isGround)
        {
            m_rigidbody2D.AddForce(Vector2.up * m_jumpSpeed, ForceMode2D.Impulse);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump!");
    }
}
