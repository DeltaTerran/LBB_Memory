using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
	//Значения, указывающие количество ячеек сетки и их расстояние друг от друга.
	public const int gridRows = 2;
	public const int gridCols = 4;
	public const float offsetX = 2f;
	public const float offsetY = 2.5f;
	/// <summary>
	///  Ссылка для карты в сцене
	/// </summary>
	[SerializeField] private MemoryCard originalCard;
    [SerializeField] private TextMesh scoreLabel;
    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;


    private int _score = 0;
    /// <summary>
    /// Массив для ссылок на ресурсы-спрайты
    /// </summary>
    [SerializeField] private Sprite[] images;
    // Use this for initialization
    void Start()
    {
		//Положение первой карты; положение остальных карт отсчитывается от этой точки.
		Vector3 startPos = originalCard.transform.position;
        //Объявляем целочисленный массив с парами идентификато ров для всех четырех спрайтов с изображениями карт.
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
				MemoryCard card;
                if (i == 0 && j == 0)
                {
					card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }
                //Идентификаторы получаем из перемешанного списка, а не из случайных чисел.
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
        
    }
    /// <summary>
    /// Реализация алгоритма тасования Кнута.
    /// </summary>
    /// <param name="numbers"></param>
    /// <returns></returns>
    //private int[] ShuffleArray(int[] numbers)
    //{
    //    int[] newArray = numbers.Clone() as int[];
    //    for (int i = 0; i < newArray.Length; i++)
    //    {
    //        int tmp = newArray[i];
    //        int r = UnityEngine.Random.Range(i, newArray.Length);
    //        newArray[i] = newArray[r];
    //        newArray[r] = tmp;
    //    }
    //    return newArray;
    //}
    private int[] ShuffleArray(int[] numbers)
    {
        //Реализация алгоритма тасования Кнута. 
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = UnityEngine.Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public bool canReveal
    {
        get
        {
            return _secondRevealed == null;
        }
    }
    public void CardRevealed (MemoryCard card)
    {
        //Сохраняем карты в одну из двух переменных, в зависимости от того, какая из них свободна.
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
            
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            scoreLabel.text = "Score: " + _score;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        //Очищаем переменные вне зависимости от того, было ли совпадение.
        _firstRevealed = null;
        _secondRevealed = null;
    }
    /// <summary>
    /// Эта команда загружает ресурс scene.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene("main");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
