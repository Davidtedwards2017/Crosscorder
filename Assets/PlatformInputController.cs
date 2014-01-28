using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Require a character controller to be attached to the same game object
[RequireComponent(typeof(CharacterMotor))]
[AddComponentMenu("Character/Platform Input Controller")]


// This makes the character turn to face the current movement speed per default.
public class PlatformInputController : MonoBehaviour
{
	private RecordedInput m_RecordedInput;
	private ControlledInput m_ControlledInput;
	public GunScript m_GunScript;
	public bool IsRecordedInput = false;

	//public LineRenderer Line;

	public RecordedInput RecordedInput
	{
		get
		{
			if( m_RecordedInput ==  null)
			{
				m_RecordedInput = GetComponent<RecordedInput>();

				if(m_RecordedInput != null)
					IsRecordedInput = true;

			}
			return m_RecordedInput;
		}
	}

	public ControlledInput ControlledInput
	{
		get
		{
			if( m_ControlledInput ==  null)
			{
				m_ControlledInput = GetComponent<ControlledInput>();
				
				if(IsRecordedInput != null)
					IsRecordedInput = false;
				
			}
			return m_ControlledInput;
		}
	}

	public GunScript GunScript
	{
		get
		{
			if(m_GunScript == null)
				m_GunScript = GetComponentInChildren<GunScript>();

			return m_GunScript;
		}
	}

    public bool autoRotate = true;
    public float maxRotationSpeed = 360.0f;

    private CharacterMotor motor;


	public Vector3 GetdirectionVector
	{
		get
		{
			Vector3 vec = Vector3.zero;
			if( IsRecordedInput)
			{
				if(this.RecordedInput!= null)
				vec = this.RecordedInput.GetdirectionVector();
			}
			else
			{
				if(this.ControlledInput!= null)
				vec = this.ControlledInput.GetdirectionVector();
			}

			return vec;
		}
	}

	public bool GetJump
	{
		get
		{
			bool bjump = false;
			if( IsRecordedInput)
			{
				if(this.RecordedInput!= null)
					bjump = this.RecordedInput.GetJump();
			}
			else
			{
				if(this.ControlledInput!= null)
					bjump = this.ControlledInput.GetJump();
			}

			return bjump;
		}
	}

	public bool GetFire
	{
		get
		{
			bool bFire = false;
			if( IsRecordedInput)
			{
				if(this.RecordedInput!= null)
					bFire = this.RecordedInput.GetFire();
			}
			else
			{
				if(this.ControlledInput!= null)
					bFire = this.ControlledInput.GetFire();
			}
			
			return bFire;
		}
	}

	public Vector3 GetMousePos
	{
		get
		{
			Vector3 aim = Vector3.zero;
			if( IsRecordedInput)
			{
				if(this.RecordedInput!= null)
					aim = this.RecordedInput.GetMousePos();
			}
			else
			{
				if(this.ControlledInput != null)
					aim = this.ControlledInput.GetMousePos();
			}
			
			return aim;
		}
	}
    // Use this for initialization
    void Awake()
    {
		//Line = gameObject.AddComponent<LineRenderer> ();
		//Line.SetVertexCount (2);
        motor = GetComponent<CharacterMotor>();
		//InputClass = GetComponent<aInput> ();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input vector from kayboard or analog stick
		Vector3 directionVector = GetdirectionVector;
			//new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (directionVector != Vector3.zero)
        {
            // Get the length of the directon vector and then normalize it
            // Dividing by the length is cheaper than normalizing when we already have the length anyway
            var directionLength = directionVector.magnitude;
            directionVector = directionVector / directionLength;

            // Make sure the length is no bigger than 1
            directionLength = Mathf.Min(1, directionLength);

            // Make the input vector more sensitive towards the extremes and less sensitive in the middle
            // This makes it easier to control slow speeds when using analog sticks
            directionLength = directionLength * directionLength;

            // Multiply the normalized direction vector by the modified length
            directionVector = directionVector * directionLength;
        }

        // Rotate the input vector into camera space so up is camera's up and right is camera's right
        directionVector = Camera.main.transform.rotation * directionVector;

        // Rotate input vector to be perpendicular to character's up vector
        Quaternion camToCharacterSpace = Quaternion.FromToRotation(-Camera.main.transform.forward, transform.up);
        directionVector = (camToCharacterSpace * directionVector);

        // Apply the direction to the CharacterMotor
        motor.inputMoveDirection = directionVector;

		if (directionVector != Vector3.zero)
						Debug.Log ("input not zero");

		motor.inputJump = GetJump;
			//Input.GetButton("Jump");

        // Set rotation to the move direction	
        if (autoRotate && directionVector.sqrMagnitude > 0.01)
        {
            Vector3 newForward = ConstantSlerp(transform.forward, directionVector, maxRotationSpeed * Time.deltaTime);
            newForward = ProjectOntoPlane(newForward, transform.up);
            transform.rotation = Quaternion.LookRotation(newForward, transform.up);
        }


		if (GetFire) 
		{
			GunScript.Fire (GetMousePos);
		}

		//Line.SetPosition (0, transform.position);
		//Line.SetPosition (1, GetMousePos);
		//this.GunScript.AimGun (new Vector3(0,1,0));
    }

    Vector3 ProjectOntoPlane(Vector3 v, Vector3 normal)
    {
        return v - Vector3.Project(v, normal);
    }

    Vector3 ConstantSlerp(Vector3 from, Vector3 to, float angle)
    {
        float value = Mathf.Min(1, angle / Vector3.Angle(from, to));
        return Vector3.Slerp(from, to, value);
    }
}