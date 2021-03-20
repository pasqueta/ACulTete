using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvent : MonoBehaviour
{
    [System.Serializable]
    public class CollisionEvent : UnityEvent<Collision2D>
    {
    }
    [System.Serializable]
    public class TriggerEvent : UnityEvent<Collider2D>
    {
    }

    CollisionEvent m_onColliderEnter = new CollisionEvent();
    CollisionEvent m_onColliderExit = new CollisionEvent();
    TriggerEvent m_onTriggerEnter = new TriggerEvent();
    TriggerEvent m_onTriggerExit = new TriggerEvent();

    #region GET/SET
    public CollisionEvent OnColliderEnterEvent { get => m_onColliderEnter; set => m_onColliderEnter = value; }
    public CollisionEvent OnColliderExitEvent { get => m_onColliderExit; set => m_onColliderExit = value; }
    public TriggerEvent OnTriggerEnterEvent { get => m_onTriggerEnter; set => m_onTriggerEnter = value; }
    public TriggerEvent OnTriggerExitEvent { get => m_onTriggerExit; set => m_onTriggerExit = value; }
    #endregion

    #region UNITY
    // Start is called before the first frame update
    void Start()
    {
        if (m_onColliderEnter == null)
        {
            m_onColliderEnter = new CollisionEvent();
        }
        if (m_onColliderExit == null)
        {
            m_onColliderExit = new CollisionEvent();
        }
        if (m_onTriggerEnter == null)
        {
            m_onTriggerEnter = new TriggerEvent();
        }
        if (m_onTriggerExit == null)
        {
            m_onTriggerExit = new TriggerEvent();
        }
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        m_onColliderEnter.Invoke(collision);
    }

	private void OnCollisionExit2D(Collision2D collision)
	{
        m_onColliderExit.Invoke(collision);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        m_onTriggerEnter.Invoke(collision);
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        m_onTriggerExit.Invoke(collision);
    }
	#endregion
}
