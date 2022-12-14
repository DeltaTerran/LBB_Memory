using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
	//Эта функция вызывается после щелчка на объекте.
	[SerializeField] private GameObject cardBack;
	public void OnMouseDown()
    {
		//Kод деактивации запускается только в случае, когда объект активен / видим.
		if (cardBack.activeSelf)
        {
			cardBack.SetActive(false);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
