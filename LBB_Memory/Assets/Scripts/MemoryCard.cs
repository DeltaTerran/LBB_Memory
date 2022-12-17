using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
	//Эта функция вызывается после щелчка на объекте.
	[SerializeField] private GameObject cardBack;
	[SerializeField] private SceneController controller;
	private int _id;
	public int id
    {
		get 
		{
			return _id;
		}
    }
	/// <summary>
	/// Открытый метод, которым могут пользоваться другие сценарии для передачи
	/// указанному объекту новых спрайтов.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="image"></param>
	public void SetCard(int id, Sprite image)
    {
		_id = id;
		GetComponent<SpriteRenderer>().sprite = image;
    }
	public void OnMouseDown()
    {
		//Kод деактивации запускается только в случае, когда объект активен / видим.
		//Проверка свойства контроллера canReveal, гарантирующая, что одновременно можно открыть всего две карты.
		if (cardBack.activeSelf && controller.canReveal)
        {
			cardBack.SetActive(false);
			controller.CardRevealed(this);
        }
    }
	/// <summary>
	/// Открытый метод, позволяющий компоненту SceneController снова скрыть карту (вернув на место спрайт card_back).
	/// </summary>
	public void Unreveal()
    {
		cardBack.SetActive(true);
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
