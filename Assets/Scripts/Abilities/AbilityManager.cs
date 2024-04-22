using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    /*
https://www.youtube.com/watch?v=ry4I6QyPw4E
https://www.youtube.com/watch?v=0HCOZo5N-t4
https://www.youtube.com/watch?v=Jv9jGyIWelU&t=300s
https://www.youtube.com/watch?v=k7whbYamd_w
    */

    TargetSelection targetSelection;
    Transform target;

    void Start() {
        targetSelection = GetComponent<TargetSelection>();
        targetSelection.OnTargetSelected += OnTargetSelected;
    }

    public void HandleAbility(Ability ability) {
        if (target != null) {
            // If ability has a cast time, start a timer here, and if the cast bar finished activate the ability. TODO 
            ability.Activate(transform, target);
        }
    }

    void OnTargetSelected(Transform selection) {
        target = selection;
    }
}
