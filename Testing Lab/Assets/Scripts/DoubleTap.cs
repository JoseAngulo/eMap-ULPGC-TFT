using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoubleTap : MonoBehaviour {

    public Button button;
    private int counter;
    public float clickTimer = 0.5f;


	void Start () {
        button.onClick.AddListener(buttonListener);
	}

    private void buttonListener() {
        counter++;
        if(counter == 1){
            StartCoroutine("doubleClickEvent");
        }
    }

    IEnumerator doubleClickEvent()
    {
        yield return new WaitForSeconds(clickTimer);

        if(counter > 1)
        {
            print("Double Click");
            counter = 0;
        }

        yield return new WaitForSeconds(.05f);
        counter = 0;
    }
	
	
}
