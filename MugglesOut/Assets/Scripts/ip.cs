using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ip : MonoBehaviour
{
    public Button Button;
    public InputField InputField;
    public string ipInput;

    
    void Start()
    {
		Button btn = Button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);        
    }

	void TaskOnClick(){
    ipInput = InputField.text;
		Debug.Log (ipInput);
	}
}