using UnityEngine;
using System.Collections;

public class DoorSystem : MonoBehaviour
{
	[Header("Door System Settings")]
	[SerializeField] private GameObject door;
	[SerializeField] private float moveTime = 1f;
	[SerializeField] private float closeDelay = 1f;
	[SerializeField] private Transform openPosition;
	[SerializeField] private Transform closedPosition;

	private Coroutine doorSystemCoroutine;
	private Coroutine doorCloseSystemCoroutine;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			MoveDoor(openPosition.position);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			doorCloseSystemCoroutine = StartCoroutine(StartCloseDoorSystem());
		}
	}

	private void MoveDoor(Vector3 targetPosition)
	{
		doorSystemCoroutine = StartCoroutine(StartDoorSystem(targetPosition));
	}

	IEnumerator StartDoorSystem(Vector3 targetPosition)
	{
		Vector3 doorStartPosition = door.transform.position;
		float timeElapsed = 0f;

		while (timeElapsed < moveTime)
		{
			door.transform.position = Vector3.Lerp(doorStartPosition, targetPosition, timeElapsed / moveTime);
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		door.transform.position = targetPosition;

	}

	IEnumerator StartCloseDoorSystem()
	{
		yield return new WaitForSeconds(closeDelay);
		MoveDoor(closedPosition.position);
	}
}
