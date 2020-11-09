using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

	public GameObject Player;
	public GameObject Enemy;
	public float fov = 120f;
	public float viewDistance = 10f;
	private bool isAware = false;
	private NavMeshAgent agent;
	private Renderer renderer;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		renderer = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isAware) 
		{
			if ((Vector3.Angle (Vector3.forward, transform.InverseTransformPoint (Player.transform.position)) < fov / 2f) && Vector3.Distance (Player.transform.position, transform.position) < viewDistance) 
			{
				agent.SetDestination (Player.transform.position);
				renderer.material.color = Color.black;
			} else 
			{
				agent.SetDestination (Enemy.transform.position);
				SearchForPlayer ();
				renderer.material.color = Color.red;
			}

		} else
		{
			SearchForPlayer ();
			renderer.material.color = Color.red;
		}
		
	}
	public void SearchForPlayer()
	{
		if (Vector3.Angle (Vector3.forward, transform.InverseTransformPoint (Player.transform.position)) < fov / 2f) 
		{
			if (Vector3.Distance (Player.transform.position, transform.position) < viewDistance) 
			{	
				RaycastHit hit;
				if (Physics.Linecast (transform.position, Player.transform.position, out hit, -1)) 
				{
					if (hit.transform.CompareTag ("Player")) {
						OnAware ();
					}
				}
			}
		} 
			
	}
	public void OnAware()
	{
		isAware = true;
	}
}
