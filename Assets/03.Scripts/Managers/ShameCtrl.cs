using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShameCtrl : MonoBehaviour
{
    [SerializeField]
    private Sprite[] numbers;
    [SerializeField]
    private Image[] values;
    public void UpdateShame(int value)
    {
        bool notZero = false;
        if (value < 0)
        {
            value = 0;
        }

        for (int i = 0; i < values.Length; i++)
        {
            int number = System.Convert.ToInt32(value.ToString("D" + values.Length)[i].ToString());
            if (number != 0 || i == values.Length - 1)
            {
                notZero = true;
            }
            if (notZero)
            {
                values[i].gameObject.SetActive(true);
                values[i].sprite = numbers[number];
            }
            else
            {
                values[i].gameObject.SetActive(false);
            }
        }

    }

}
