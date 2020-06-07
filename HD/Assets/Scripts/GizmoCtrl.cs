using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoCtrl : MonoBehaviour
{
	public Color _color = Color.cyan;
	public float _radius = 0.1f;

	void OnDrawGizmos()
	{
		Gizmos.color = _color;
		Gizmos.DrawSphere(this.transform.position, _radius);
	}
}
