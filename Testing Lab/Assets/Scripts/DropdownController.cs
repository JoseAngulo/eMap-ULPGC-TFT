using TMPro;
using UnityEngine;

public class DropdownController : MonoBehaviour
{
    public GameObject dropdown;

    public void changeDropLabel(string label)
    {
        dropdown.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = label;
    }
}
