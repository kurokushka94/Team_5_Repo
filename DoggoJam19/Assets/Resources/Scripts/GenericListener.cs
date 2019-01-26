using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericListener : MonoBehaviour
{
	//needs to be filled in inspector
	public List<GameObject> Triggers;

	public bool IsActivated = false;
	private bool AlreadyActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		bool success = true;

		BaseCondition trig;

		for(int i = 0; i < Triggers.Count; ++i)
		{
			trig = Triggers[i].GetComponent<BaseCondition>();

			Debug.Log("Trig value" + trig);

			if(!trig.IsActivated)
			{
				success = false;
				break;
			}
		}

		if(success && !AlreadyActivated)
		{
			IsActivated = true;
			AlreadyActivated = true;
			AllTriggersOccured();
		}
    }
	
	private void AllTriggersOccured()
	{
		//for debug only
		//transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
	}
}
