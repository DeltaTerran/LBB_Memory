using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour {
	/// <summary>
	/// Ссылка на целевой объект для информирования о щелчках.
	/// </summary>
	[SerializeField] private GameObject targetObject;
	[SerializeField] private string targetMessage;
	public Color highlightColor = Color.cyan;

	public void OnMouseEnter()
    {
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite != null)
        {
			sprite.color = highlightColor;
        }
    }
	public void OnMouseExit()
    {
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite != null)
		{
			sprite.color = Color.white;
		}
	}
	public void OnMouseDown()
    {
		//В момент щелчка размер кнопки слегка увеличивается.
		transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }
	public void OnMouseUp()
	{
		//В момент щелчка размер кнопки слегка увеличивается.
		transform.localScale = Vector3.one;
		//После щелчка на кнопке отправляем cообщение целевому объекту.
		if (targetObject != null)
        {
			targetObject.SendMessage(targetMessage);
        }
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
