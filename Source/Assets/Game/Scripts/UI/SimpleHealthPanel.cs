using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleHealthPanel : MonoBehaviour
{
    public Text Count;

	void Start ()
    {
        GameManager.instance.OnPlayerHealthUpdate += OnPlayerHealthUpdate;
	}

    private void OnPlayerHealthUpdate(int value)
    {
        Count.text = value.ToString();
    }
}