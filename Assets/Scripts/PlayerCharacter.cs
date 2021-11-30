using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private ColliderEvent m_colliderWeakness = null;

    UnityEvent m_onWeaknessTouch = new UnityEvent();

	#region GET/SET
	public UnityEvent OnWeaknessTouch { get => m_onWeaknessTouch; set => m_onWeaknessTouch = value; }
	#endregion

	#region UNITY
	// Start is called before the first frame update
	void Start()
    {
        if(m_onWeaknessTouch == null)
		{
            m_onWeaknessTouch = new UnityEvent();
		}

        m_colliderWeakness.OnTriggerEnterEvent.AddListener(WeaknessIsTouch);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	#endregion

    void WeaknessIsTouch(Collider2D collider)
	{
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            m_onWeaknessTouch.Invoke();
            Debug.Log(transform.root + ": Outch my head !!");
        }
	}
}
