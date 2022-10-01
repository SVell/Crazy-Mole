using UnityEngine;

namespace SVell 
{
	public class Launcher : MonoBehaviour
	{
		[SerializeField] private LineRenderer lineRenderer;
		[SerializeField] private float lineSize = 3f;
		[SerializeField] private float launchStrength;
		[SerializeField] private float loseVelocity = 0.05f;

		private Mole _mole;
		
		private Rigidbody _rigidbody;

		private Vector3 _secondPoint;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_mole = GetComponent<Mole>();
		}

		private void Update()
		{
			Launch();

			if (_mole.MoleState == MoleState.Moving)
			{
				CheckMoleMovement();
			}
		}

		private void Launch()
		{
			if(_mole.MoleState != MoleState.Launching) return;
			
			Vector3? worldPoint = CastMouseClickRay();
			
			if (!worldPoint.HasValue) return;
			
			if (Input.GetMouseButton(0))
			{
				DrawLine(worldPoint.Value);
			}

			if (Input.GetMouseButtonUp(0))
			{
				Shoot(worldPoint.Value);
				_mole.SetMoleState(MoleState.Moving);
			}
		}

		private void CheckMoleMovement()
		{
			if (_rigidbody.velocity.magnitude <= loseVelocity)
			{
				Debug.Log("GG");
				_mole.SetMoleState(MoleState.Stopped);
			}
		}

		private void DrawLine(Vector3 worldPoint)
		{
			Vector3 pos = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

			if ((pos - transform.position).magnitude <= lineSize)
			{
				_secondPoint = pos;
			}
			else
			{
				_secondPoint = (pos - transform.position).normalized * lineSize + transform.position;
			}

			Vector3[] positions =
			{
				transform.position, 
				_secondPoint
			};

			lineRenderer.SetPositions(positions);
			lineRenderer.enabled = true;
		}

		private void Shoot(Vector3 worldPoint)
		{
			lineRenderer.enabled = false;
			
			Vector3 pos = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

			Vector3 dir = -(pos - transform.position).normalized;

			float strength;
			
			if ((_secondPoint - transform.position).magnitude < lineSize)
			{
				strength = launchStrength * (Vector3.Distance(_secondPoint, transform.position) / lineSize);
			}
			else
			{
				strength = launchStrength;
			}
			
			_rigidbody.AddForce(dir * strength);
		}

		private Vector3? CastMouseClickRay()
		{
			Vector3 screenMousePosFar = new Vector3(
				Input.mousePosition.x,
				Input.mousePosition.y,
				Camera.main.farClipPlane);
			Vector3 screenMousePosNear = new Vector3(
				Input.mousePosition.x,
				Input.mousePosition.y,
				Camera.main.nearClipPlane);
			
			Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
			Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

			if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out var hit,
				    float.PositiveInfinity))
			{
				return hit.point;
			}
			else
			{
				return null;
			}
		}
	}
}
