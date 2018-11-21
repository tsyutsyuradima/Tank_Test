using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageValueComponent : MonoBehaviour
{
    public void SetTextValue(string text)
    {
        TextMesh textMesh = GetComponent<TextMesh>();
        textMesh.text = text;
    }

    public void HelpYourself()
    {
        GameObject.DestroyImmediate(gameObject);
    }
}