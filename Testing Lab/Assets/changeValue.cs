using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeValue : MonoBehaviour
{

    public GameObject dropdown;

    public void change()
    {
        Debug.Log("CAMBIANDO VALOR DEL DROPDOWN EN CÓDIGO " + dropdown.name);
        dropdown.GetComponent<TMP_Dropdown>().value = 1;
    }
}
