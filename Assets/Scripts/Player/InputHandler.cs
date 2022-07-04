using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
	public static InputHandler instance;

	public Animator animator;

	private PlayerInput controls;
	private GameObject currentTeleporter;



	[Header("Input Values")]
	public Action<InputArgs> OnJumpPressed;
	public Action<InputArgs> OnJumpReleased;
	public Action<InputArgs> OnDash;


	public Vector2 MoveInput { get; private set; }
	public float ClimbInput { get; private set; }

	public bool hasOpenedDoor = false;
	public bool GoToNextSentence = false;

	private void Awake()
	{


		#region Singleton
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}
		#endregion

		controls = new PlayerInput();

		#region Assign Inputs
		//Player movement
		controls.Player.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
		controls.Player.Movement.canceled += ctx => MoveInput = Vector2.zero;

		//Player Jump
		controls.Player.Jump.performed += ctx => OnJumpPressed(new InputArgs { context = ctx });
		controls.Player.JumpUp.performed += ctx => OnJumpReleased(new InputArgs { context = ctx });

		//Player Dash
		controls.Player.Dash.performed += ctx => OnDash(new InputArgs { context = ctx });

		//Player Attack
		controls.Player.Attack.performed += ctx => Attack();
		controls.Player.Attack.canceled += ctx => AttackEnd();

		//Player Open Door
		controls.Player.OpenDoor.performed += ctx => hasOpenedDoor = true;
		controls.Player.OpenDoor.canceled += ctx => hasOpenedDoor = false;

		//Player Climb
		controls.Player.Climb.performed += ctx => ClimbInput = ctx.ReadValue<float>();
        controls.Player.Climb.canceled += ctx => ClimbInput = 0;


		//Plays the next sentence
		controls.Player.DialogueNext.performed += ctx => GoToNextSentence = true;
		controls.Player.DialogueNext.canceled += ctx => GoToNextSentence = false;

        #endregion
    }

    #region Events
    public class InputArgs
	{
		public InputAction.CallbackContext context;
	}
	#endregion

	#region OnEnable/OnDisable
	private void OnEnable()
	{
		controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
	}
	#endregion

	

	// Update is called once per frame
	void Update()
	{
		if (hasOpenedDoor == true)
		{
			if (currentTeleporter != null)
			{
				transform.position = currentTeleporter.GetComponent<TeleporterBehavior>().GetDestination().position;
			}
		}
	}

    #region TRIGGER TELEPORTER
    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Teleporter"))
		{
			currentTeleporter = collision.gameObject;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Teleporter"))
		{
			if (collision.gameObject == currentTeleporter)
			{
				currentTeleporter = null;
			}
		}

	}
    #endregion


    #region ATTACK FUNCTION
    private void Attack()
    {
		animator.SetBool("HasAttacked", true);
		
	}


	private void AttackEnd()
    {
		animator.SetBool("HasAttacked", false);

	}
    #endregion


	
}


