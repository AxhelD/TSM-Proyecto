using UnityEngine.UI;
using UnityEngine;

public class GeysermanMovePeripheralScript : MonoBehaviour
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
	public float toWaterBarTimerMax;
	public float rechargeWater;

	public bool grounded = true;
	public bool airIdle = false;

	[HideInInspector]
	private string[] stringArray;
	private int right1;
	private int right2;
	private int right3;
	private int left1;
	private int left2;
	private int left3;

	private float waterCapacity = 100f;
	private bool isRestored = false;
	private bool waterRestore = true;
	private float toWaterBarTimer;
	private Rigidbody rb;
	private Animator animator;
	private RaycastHit raycastHit;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		SerialManagerScript.WhenReceiveDataCall += ReceiveData;
	}

	void ReceiveData(string incomingString)
	{
		stringArray = incomingString.Split(new char[] { ',' });
		int.TryParse(stringArray[0], out left1);
		int.TryParse(stringArray[1], out left2);
		int.TryParse(stringArray[2], out left3);
		int.TryParse(stringArray[3], out right1);
		int.TryParse(stringArray[4], out right2);
		int.TryParse(stringArray[5], out right3);
	}

	void FixedUpdate()
	{
		toWaterBarTimer -= Time.deltaTime;
		if (waterCapacity < 0 && waterRestore)
		{
			toWaterBarTimer = toWaterBarTimerMax;
			waterRestore = false;
		}
		RestoreCapacity(toWaterBarTimer);

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
			SerialManagerScript.SendInfo("f");
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
		if ((left2 == 0 & right2 == 1 & left1 == 1 & right1 == 1) | (left2 == 0 & right2 == 0 & left1 == 0 & right1 == 1) && waterCapacity > 0)
		{
			WaterCapacity();
			SerialManagerScript.SendInfo("a");
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
		if ((left2 == 1 & right2 == 0 & left1 == 1 & right1 == 1) | (left2 == 0 & right2 == 0 & left1 == 1 & right1 == 0) && waterCapacity > 0)
		{
			WaterCapacity();
			SerialManagerScript.SendInfo("d");
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
		if ((left2 == 1 & right2 == 1 & left1 == 0 & right1 == 1) | (left2 == 0 & right2 == 1 & left1 == 0 & right1 == 0) && waterCapacity > 0)
		{
			WaterCapacity();
			SerialManagerScript.SendInfo("a");
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
		if ((left2 == 1 & right2 == 1 & left1 == 1 & right1 == 0) | (left2 == 1 & right2 == 0 & left1 == 0 & right1 == 0) && waterCapacity > 0)
		{
			WaterCapacity();
			SerialManagerScript.SendInfo("d");
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
		if (left2 == 0 & right2 == 0 & !(right1 == 1 | left1 == 1) && waterCapacity > 0)
		{
			WaterCapacity();
			SerialManagerScript.SendInfo("c");
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
		if (right1 == 0 & right2 == 0 & !(left2 == 0 | left1 == 0) && waterCapacity > 0)
		{
			WaterCapacity();
			SerialManagerScript.SendInfo("e");
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
		if (left1 == 0 & left2 == 0 & !(right2 == 0 | right1 == 0) && waterCapacity > 0)
		{
			WaterCapacity();
			SerialManagerScript.SendInfo("b");
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
		if (right1 == 0 & left1 == 0 & !(left2 == 0 | right2 == 0) && waterCapacity > 0)
		{
			WaterCapacity();
			SerialManagerScript.SendInfo("c");
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
		if (!(right1 == 0 | left1 == 0 | left2 == 0 | right2 == 0) | (right1 == 0 & left1 == 0 & left2 == 0 & right2 == 0))
		{
			SerialManagerScript.SendInfo("f");
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
				if (left3 == 0 & right3 == 1)
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
				if (left3 == 1 & right3 == 0)
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
				if (!(left3 == 0 | right3 == 0))
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

	private void RestoreCapacity(float toWaterBarTimer)
	{
		if (waterCapacity <= 0)
		{
			SerialManagerScript.SendInfo("f");
			if (isRestored)
			{
				waterCapacity = 100;
				JetCapacity.fillAmount = (float)waterCapacity / 100f;
				waterRestore = true;
				isRestored = false;
			}
			else if (!isRestored)
			{
				if (toWaterBarTimer < 0)
				{
					if (JetCapacity.fillAmount <= 1)
					{
						JetCapacity.fillAmount += shrinkSpeed * Time.deltaTime;
					}
				}
				if (JetCapacity.fillAmount >= 1)
				{
					isRestored = true;
				}
			}
		}
		else if (waterCapacity > 0 && waterCapacity < 100)
		{

			waterCapacity += 10 * rechargeWater * Time.deltaTime;
			JetCapacity.fillAmount = (float)waterCapacity / 100f;
		}
	}
}
