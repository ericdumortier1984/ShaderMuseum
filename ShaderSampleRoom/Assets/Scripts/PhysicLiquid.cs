using UnityEngine;

public class PhysicLiquid : MonoBehaviour
{
	[Header("Physic Liquid Settings")]
	[SerializeField] private Material physicLiquid;

	[Header("Wobble Settings")]
	[SerializeField] private float wobbleAmplitude = 0.02f;
	[SerializeField] private float wobbleSpeed = 1.5f;
	[SerializeField] private float wobbleRecovery = 2.0f;

	private Vector3 lastPosition;
	private Vector3 velocity;
	private Vector3 angularVelocity;
	private Quaternion lastRotation;

	private float wobblePulse;
	private float wobbleX;
	private float wobbleZ;

	private void Start()
	{
		lastPosition = transform.position;
		lastRotation = transform.rotation;
	}

	private void Update()
	{
		wobblePulse += Time.deltaTime * wobbleSpeed;

		SetWobbleRecovery();
		SetWobbleSine();
		SetWobbleVelocity();

		lastPosition = transform.position;
		lastRotation = transform.rotation;
	}

	private void SetWobbleRecovery()
	{
		wobbleX = Mathf.Lerp(wobbleX, 0, Time.deltaTime * wobbleRecovery);
		wobbleZ = Mathf.Lerp(wobbleZ, 0, Time.deltaTime * wobbleRecovery);
	}

	private void SetWobbleSine()
	{
		float wobbleSine = Mathf.Sin(wobblePulse);

		float finalWobbleXPos = wobbleX * wobbleSine;
		float finalWobbleZPos = wobbleZ * wobbleSine;

		physicLiquid.SetFloat("_WobbleX", finalWobbleXPos);
		physicLiquid.SetFloat("_WobbleZ", finalWobbleZPos);
	}

	private void SetWobbleVelocity()
	{
		velocity = (transform.position - lastPosition) / Time.deltaTime;

		Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(lastRotation);

		Vector3 deltaEuler = deltaRotation.eulerAngles;

		angularVelocity = new Vector3(
			Mathf.DeltaAngle(0, deltaEuler.x),
			Mathf.DeltaAngle(0, deltaEuler.y),
			Mathf.DeltaAngle(0, deltaEuler.z)
		);

		wobbleX += Mathf.Clamp((velocity.x + angularVelocity.z * 0.2f) * wobbleAmplitude, -wobbleAmplitude, wobbleAmplitude);
		wobbleZ += Mathf.Clamp((velocity.z + angularVelocity.x * 0.2f) * wobbleAmplitude, -wobbleAmplitude, wobbleAmplitude);
	}
}
