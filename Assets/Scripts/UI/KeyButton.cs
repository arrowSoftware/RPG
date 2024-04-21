using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    ControlBinding key;

    public void SetBind(ControlBinding key) {
        this.key = key;
    }

    void Update()
    {
        if (key != null) {
            if (key.GetControlBindingDown()) {
            GetComponent<Button>().onClick.Invoke();
            }            
        }
    }
}
