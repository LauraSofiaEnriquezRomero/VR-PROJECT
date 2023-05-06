using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ip : MonoBehaviour
{
    public Button Button;
    public InputField InputField;
    private string txtInput;

    
    void Start()
    {
		Button btn = Button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);        
    }

	void TaskOnClick(){
        txtInput = InputField.text;
		Debug.Log (txtInput);
	}
}