using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GeysermanMoveScript : MonoBehaviour
{
	public GameObject armWaterjetPrefab;
	public GameObject legWaterjetPrefab;
	public GameObject upShot1;
	public GameObject upShot2;
	public GameObject groundUpShot1;
	public GameObject groundUpShot2;
	public GameObject armShot;
	public GameObject groundArmShot;
	public GameObject sideShot1;
	public GameObject sideShot2;
	public GameObject legShot;
	public GameObject downShot1;
	public GameObject downShot2;
	public GameObject sound;

	public Image JetCapacity;

	public float flySpeed;
	public float runSpeed = 15;
	public float shrinkSpeed;
	public float neededWater;

	public bool grounded = true;
	public bool airIdle = false;

	private float waterCapacity = 100f;
	private bool isRestored = false;
	private Rigidbody rb;
	private Animator animator;
	private RaycastHit raycastHit;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
	}

    void FixedUpdate()
	{
		RestoreCapacity();

		if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), -transform.up, out raycastHit, 1.1f))
		{
			if (airIdle & !(Input.GetKey(KeyCode.E) | Input.GetKey(KeyCode.R)))
			{
				animator.SetBool("Landing", true);
			}
			else
			{
				animator.SetBool("Landing", false);
			}
			grounded = true;

		}
		else
		{
			grounded = false;
		}
		if ((float)GetComponent<GeysermanHealthScript>().geyserHealth <= 0)
		{
			if (grounded)
			{
				animator.SetBool("AirIdle", false);
				animator.SetBool("UpShot", false);
				animator.SetBool("ArmShot", false);
				animator.SetBool("SideShot", false);
				animator.SetBool("LegShot", false);
				animator.SetBool("DownShot", false);
				animator.SetBool("GroundUpShot", false);
				animator.SetBool("GroundArmShot", false);
				animator.SetBool("GroundIdle", false);
				animator.SetBool("Run", false);
				animator.SetBool("DeadFall", false);
				animator.SetBool("Landing", true);
				airIdle = false;
				sound.SetActive(false);
			}
			else
			{
				rb.velocity = new Vector3(0, -1.5f, 0) * flySpeed;
				animator.SetBool("AirIdle", false);
				animator.SetBool("UpShot", false);
				animator.SetBool("ArmShot", false);
				animator.SetBool("SideShot", false);
				animator.SetBool("LegShot", false);
				animator.SetBool("DownShot", false);
				animator.SetBool("GroundUpShot", false);
				animator.SetBool("GroundArmShot", false);
				animator.SetBool("GroundIdle", false);
				animator.SetBool("Run", false);
				animator.SetBool("DeadFall", true);
				animator.SetBool("Landing", false);
				airIdle = true;
				sound.SetActive(false);
			}
			return;
		}
		//Input W
		if ((Input.GetKey(KeyCode.W) & !Input.GetKey(KeyCode.Y) & !Input.GetKey(KeyCode.S) & !Input.GetKey(KeyCode.G)) | (Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.Y) & Input.GetKey(KeyCode.S) & !Input.GetKey(KeyCode.G)) && waterCapacity > 0)
		{
			WaterCapacity();
			transform.localScale = new Vector3(-1, 1, 1);
			if (!grounded)
			{
				rb.velocity = new Vector3(1, -1, 0) * flySpeed;
				animator.SetBool("AirIdle", false);
				animator.SetBool("UpShot", false);
				animator.SetBool("ArmShot", true);
				animator.SetBool("SideShot", false);
				animator.SetBool("LegShot", false);
				animator.SetBool("DownShot", false);
				animator.SetBool("GroundUpShot", false);
				animator.SetBool("GroundArmShot", false);
				animator.SetBool("GroundIdle", false);
				animator.SetBool("Run", false);
				airIdle = false;
				GameObject waterShot = Instantiate(armWaterjetPrefab, armShot.transform.position, armShot.transform.rotation);
				waterShot.GetComponent<Rigidbody>().velocity = new Vector3(-1, 1, 0) * flySpeed;
				sound.SetActive(true);
			}
			else
			{
				rb.velocity = new Vector3(0, 0, 0);
				animator.SetBool("AirIdle", false);
				animator.SetBool("UpShot", false);
				animator.SetBool("ArmShot", false);
				animator.SetBool("SideShot", false);
				animator.SetBool("LegShot", false);
				animator.SetBool("DownShot", false);
				animator.SetBool("GroundUpShot", false);
				animator.SetBool("GroundArmShot", true);
				animator.SetBool("GroundIdle", false);
				animator.SetBool("Run", false);
				airIdle = false;
				GameObject waterShot = Instantiate(armWaterjetPrefab, groundArmShot.transform.position, groundArmShot.transform.rotation);
				waterShot.GetComponent<Rigidbody>().velocity = new Vector3(-1, 1, 0) * flySpeed;
				sound.SetActive(true);
			}
		}
		//Input Y
		if ((!Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.Y) & !Input.GetKey(KeyCode.S) & !Input.GetKey(KeyCode.G)) | (Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.Y) & !Input.GetKey(KeyCode.S) & Input.GetKey(KeyCode.G)) && waterCapacity > 0)
		{
			WaterCapacity();
			transform.localScale = new Vector3(1, 1, 1);
			if (!grounded)
			{
				rb.velocity = new Vector3(-1, -1, 0) * flySpeed;
				animator.SetBool("AirIdle", false);
				animator.SetBool("UpShot", false);
				animator.SetBool("ArmShot", true);
				animator.SetBool("SideShot", false);
				animator.SetBool("LegShot", false);
				animator.SetBool("DownShot", false);
				animator.SetBool("GroundUpShot", false);
				animator.SetBool("GroundArmShot", false);
				animator.SetBool("GroundIdle", false);
				animator.SetBool("Run", false);
				airIdle = false;
				GameObject waterShot = Instantiate(armWaterjetPrefab, armShot.transform.position, armShot.transform.rotation);
				waterShot.GetComponent<Rigidbody>().velocity = new Vector3(1, 1, 0) * flySpeed;
				sound.SetActive(true);
			}
			else
			{
				rb.velocity = new Vector3(0, 0, 0);
				animator.SetBool("AirIdle", false);
				animator.SetBool("UpShot", false);
				animator.SetBool("ArmShot", false);
				animator.SetBool("SideShot", false);
				animator.SetBool("LegShot", false);
				animator.SetBool("DownShot", false);
				animator.SetBool("GroundUpShot", false);
				animator.SetBool("GroundArmShot", true);
				animator.SetBool("GroundIdle", false);
				animator.SetBool("Run", false);
				airIdle = false;
				GameObject waterShot = Instantiate(armWaterjetPrefab, groundArmShot.transform.position, groundArmShot.transform.rotation);
				waterShot.GetComponent<Rigidbody>().velocity = new Vector3(1, 1, 0) * flySpeed;
				sound.SetActive(true);
			}
		}
		//Input S
		if ((!Input.GetKey(KeyCode.W) & !Input.GetKey(KeyCode.Y) & Input.GetKey(KeyCode.S) & !Input.GetKey(KeyCode.G)) | (Input.GetKey(KeyCode.W) & !Input.GetKey(KeyCode.Y) & Input.GetKey(KeyCode.S) & Input.GetKey(KeyCode.G)) && waterCapacity > 0)
		{
			WaterCapacity();
			transform.localScale = new Vector3(-1, 1, 1);
			rb.velocity = new Vector3(1, 1, 0) * flySpeed;
			animator.SetBool("AirIdle", false);
			animator.SetBool("UpShot", false);
			animator.SetBool("ArmShot", false);
			animator.SetBool("SideShot", false);
			animator.SetBool("LegShot", true);
			animator.SetBool("DownShot", false);
			animator.SetBool("GroundUpShot", false);
			animator.SetBool("GroundArmShot", false);
			animator.SetBool("GroundIdle", false);
			animator.SetBool("Run", false);
			airIdle = false;
			GameObject waterShot = Instantiate(legWaterjetPrefab, legShot.transform.position, legShot.transform.rotation);
			waterShot.GetComponent<Rigidbody>().velocity = new Vector3(-1, -1, 0) * flySpeed;
			sound.SetActive(true);
		}
		//Input G
		if ((!Input.GetKey(KeyCode.W) & !Input.GetKey(KeyCode.Y) & !Input.GetKey(KeyCode.S) & Input.GetKey(KeyCode.G)) | (!Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.Y) & Input.GetKey(KeyCode.S) & Input.GetKey(KeyCode.G)) && waterCapacity > 0)
		{
			WaterCapacity();
			transform.localScale = new Vector3(1, 1, 1);
			rb.velocity = new Vector3(-1, 1, 0) * flySpeed;
			animator.SetBool("AirIdle", false);
			animator.SetBool("UpShot", false);
			animator.SetBool("ArmShot", false);
			animator.SetBool("SideShot", false);
			animator.SetBool("LegShot", true);
			animator.SetBool("DownShot", false);
			animator.SetBool("GroundUpShot", false);
			animator.SetBool("GroundArmShot", false);
			animator.SetBool("GroundIdle", false);
			animator.SetBool("Run", false);
			airIdle = false;
			GameObject waterShot = Instantiate(legWaterjetPrefab, legShot.transform.position, legShot.transform.rotation);
			waterShot.GetComponent<Rigidbody>().velocity = new Vector3(1, -1, 0) * flySpeed;
			sound.SetActive(true);
		}
		//Input W Y
		if (Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.Y) & !(Input.GetKey(KeyCode.G) | Input.GetKey(KeyCode.S)) && waterCapacity > 0)
		{
			WaterCapacity();
			if (!grounded)
			{
				rb.velocity = new Vector3(0, -1.5f, 0) * flySpeed;
				animator.SetBool("AirIdle", false);
				animator.SetBool("UpShot", true);
				animator.SetBool("ArmShot", false);
				animator.SetBool("SideShot", false);
				animator.SetBool("LegShot", false);
				animator.SetBool("DownShot", false);
				animator.SetBool("GroundUpShot", false);
				animator.SetBool("GroundArmShot", false);
				animator.SetBool("GroundIdle", false);
				animator.SetBool("Run", false);
				airIdle = false;
				GameObject waterShot1 = Instantiate(armWaterjetPrefab, upShot1.transform.position, upShot1.transform.rotation);
				waterShot1.GetComponent<Rigidbody>().velocity = new Vector3(0, 1.5f, 0) * flySpeed;
				GameObject waterShot2 = Instantiate(armWaterjetPrefab, upShot2.transform.position, upShot2.transform.rotation);
				waterShot2.GetComponent<Rigidbody>().velocity = new Vector3(0, 1.5f, 0) * flySpeed;
				sound.SetActive(true);
			}
			else
			{
				rb.velocity = new Vector3(0, 0, 0);
				animator.SetBool("AirIdle", false);
				animator.SetBool("UpShot", false);
				animator.SetBool("ArmShot", false);
				animator.SetBool("SideShot", false);
				animator.SetBool("LegShot", false);
				animator.SetBool("DownShot", false);
				animator.SetBool("GroundUpShot", true);
				animator.SetBool("GroundArmShot", false);
				animator.SetBool("GroundIdle", false);
				animator.SetBool("Run", false);
				airIdle = false;
				GameObject waterShot1 = Instantiate(armWaterjetPrefab, groundUpShot1.transform.position, groundUpShot1.transform.rotation);
				waterShot1.GetComponent<Rigidbody>().velocity = new Vector3(0, 1.5f, 0) * flySpeed;
				GameObject waterShot2 = Instantiate(armWaterjetPrefab, groundUpShot2.transform.position, groundUpShot2.transform.rotation);
				waterShot2.GetComponent<Rigidbody>().velocity = new Vector3(0, 1.5f, 0) * flySpeed;
				sound.SetActive(true);
			}
		}
		//Input Y G
		if (Input.GetKey(KeyCode.Y) & Input.GetKey(KeyCode.G) & !(Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.W)) && waterCapacity > 0)
		{
			WaterCapacity();
			transform.localScale = new Vector3(1, 1, 1);
			rb.velocity = new Vector3(-1.5f, 0, 0) * flySpeed;
			animator.SetBool("AirIdle", false);
			animator.SetBool("UpShot", false);
			animator.SetBool("ArmShot", false);
			animator.SetBool("SideShot", true);
			animator.SetBool("LegShot", false);
			animator.SetBool("DownShot", false);
			animator.SetBool("GroundUpShot", false);
			animator.SetBool("GroundArmShot", false);
			animator.SetBool("GroundIdle", false);
			animator.SetBool("Run", false);
			airIdle = false;
			GameObject waterShot1 = Instantiate(armWaterjetPrefab, sideShot1.transform.position, sideShot1.transform.rotation);
			waterShot1.GetComponent<Rigidbody>().velocity = new Vector3(1.5f, 0, 0) * flySpeed;
			GameObject waterShot2 = Instantiate(legWaterjetPrefab, sideShot2.transform.position, sideShot2.transform.rotation);
			waterShot2.GetComponent<Rigidbody>().velocity = new Vector3(1.5f, 0, 0) * flySpeed;
			sound.SetActive(true);
		}
		//Input W S
		if (Input.GetKey(KeyCode.S) & Input.GetKey(KeyCode.W) & !(Input.GetKey(KeyCode.Y) | Input.GetKey(KeyCode.G)) && waterCapacity > 0)
		{
			WaterCapacity();
			transform.localScale = new Vector3(-1, 1, 1);
			rb.velocity = new Vector3(1.5f, 0, 0) * flySpeed;
			animator.SetBool("AirIdle", false);
			animator.SetBool("UpShot", false);
			animator.SetBool("ArmShot", false);
			animator.SetBool("SideShot", true);
			animator.SetBool("LegShot", false);
			animator.SetBool("DownShot", false);
			animator.SetBool("GroundUpShot", false);
			animator.SetBool("GroundArmShot", false);
			animator.SetBool("GroundIdle", false);
			animator.SetBool("Run", false);
			airIdle = false;
			GameObject waterShot1 = Instantiate(armWaterjetPrefab, sideShot1.transform.position, sideShot1.transform.rotation);
			waterShot1.GetComponent<Rigidbody>().velocity = new Vector3(-1.5f, 0, 0) * flySpeed;
			GameObject waterShot2 = Instantiate(legWaterjetPrefab, sideShot2.transform.position, sideShot2.transform.rotation);
			waterShot2.GetComponent<Rigidbody>().velocity = new Vector3(-1.5f, 0, 0) * flySpeed;
			sound.SetActive(true);
		}
		//Input S G
		if (Input.GetKey(KeyCode.G) & Input.GetKey(KeyCode.S) & !(Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.Y)) && waterCapacity > 0)
		{
			WaterCapacity();
			rb.velocity = new Vector3(0, 1.5f, 0) * flySpeed;
			animator.SetBool("AirIdle", false);
			animator.SetBool("UpShot", false);
			animator.SetBool("ArmShot", false);
			animator.SetBool("SideShot", false);
			animator.SetBool("LegShot", false);
			animator.SetBool("DownShot", true);
			animator.SetBool("GroundUpShot", false);
			animator.SetBool("GroundArmShot", false);
			animator.SetBool("GroundIdle", false);
			animator.SetBool("Run", false);
			airIdle = false;
			GameObject waterShot1 = Instantiate(legWaterjetPrefab, downShot1.transform.position, downShot1.transform.rotation);
			waterShot1.GetComponent<Rigidbody>().velocity = new Vector3(0, -1.5f, 0) * flySpeed;
			GameObject waterShot2 = Instantiate(legWaterjetPrefab, downShot2.transform.position, downShot2.transform.rotation);
			waterShot2.GetComponent<Rigidbody>().velocity = new Vector3(0, -1.5f, 0) * flySpeed;
			sound.SetActive(true);
		}
		//Input --
		if (!(Input.GetKey(KeyCode.G) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.Y)) | (Input.GetKey(KeyCode.G) & Input.GetKey(KeyCode.S) & Input.GetKey(KeyCode.W) & Input.GetKey(KeyCode.Y)))
		{
			if (!grounded)
			{
				animator.SetBool("AirIdle", true);
				animator.SetBool("UpShot", false);
				animator.SetBool("ArmShot", false);
				animator.SetBool("SideShot", false);
				animator.SetBool("LegShot", false);
				animator.SetBool("DownShot", false);
				animator.SetBool("GroundUpShot", false);
				animator.SetBool("GroundArmShot", false);
				animator.SetBool("GroundIdle", false);
				animator.SetBool("Run", false);
				airIdle = true;
				sound.SetActive(false);
			}
			else
			{
				if (Input.GetKey(KeyCode.X) & !Input.GetKey(KeyCode.V))
				{
					transform.localScale = new Vector3(1, 1, 1);
					rb.velocity = new Vector3(-1, 0, 0) * runSpeed;
					animator.SetBool("AirIdle", false);
					animator.SetBool("UpShot", false);
					animator.SetBool("ArmShot", false);
					animator.SetBool("SideShot", false);
					animator.SetBool("LegShot", false);
					animator.SetBool("DownShot", false);
					animator.SetBool("GroundUpShot", false);
					animator.SetBool("GroundArmShot", false);
					animator.SetBool("GroundIdle", false);
					animator.SetBool("Run", true);
					airIdle = false;
					sound.SetActive(false);
				}
				if (!Input.GetKey(KeyCode.X) & Input.GetKey(KeyCode.V))
				{
					transform.localScale = new Vector3(-1, 1, 1);
					rb.velocity = new Vector3(1, 0, 0) * runSpeed;
					animator.SetBool("AirIdle", false);
					animator.SetBool("UpShot", false);
					animator.SetBool("ArmShot", false);
					animator.SetBool("SideShot", false);
					animator.SetBool("LegShot", false);
					animator.SetBool("DownShot", false);
					animator.SetBool("GroundUpShot", false);
					animator.SetBool("GroundArmShot", false);
					animator.SetBool("GroundIdle", false);
					animator.SetBool("Run", true);
					airIdle = false;
					sound.SetActive(false);
				}
				if (!(Input.GetKey(KeyCode.X) | Input.GetKey(KeyCode.V)))
				{
					rb.velocity = new Vector3(0, 0, 0);
					animator.SetBool("AirIdle", false);
					animator.SetBool("UpShot", false);
					animator.SetBool("ArmShot", false);
					animator.SetBool("SideShot", false);
					animator.SetBool("LegShot", false);
					animator.SetBool("DownShot", false);
					animator.SetBool("GroundUpShot", false);
					animator.SetBool("GroundArmShot", false);
					animator.SetBool("GroundIdle", true);
					animator.SetBool("Run", false);
					airIdle = false;
					sound.SetActive(false);
				}
			}
		}
	}

	private void WaterCapacity()
	{
		waterCapacity -= neededWater;
		JetCapacity.fillAmount = (float)waterCapacity / 100f;
	}

	private void RestoreCapacity()
	{
		if (waterCapacity <= 0)
		{
			if (isRestored)
			{
				waterCapacity = 100;
				JetCapacity.fillAmount = (float)waterCapacity / 100f;
			}
			else if (!isRestored)
			{
				StartCoroutine("RestoreAttack");
			}
		}
	}

	IEnumerator RestoreAttack()
	{
		waterCapacity = 0;
		yield return new WaitForSecondsRealtime(3.0f);
		if (1 >= JetCapacity.fillAmount && waterCapacity == 0) 
		{
			JetCapacity.fillAmount += shrinkSpeed * Time.deltaTime;
		}
		if (JetCapacity.fillAmount >= 1) 
		{
			yield return new WaitForSecondsRealtime(0.1f);
			isRestored = true;
			yield return new WaitForSecondsRealtime(0.1f);
			isRestored = false;
		}
	}
}

