using UnityEngine;
using UnityEngine.XR;
using UnityEngine.EventSystems;

public class TargetSelection : MonoBehaviour
{
    [SerializeField]
    private Transform selection;

    [SerializeField]
    private Transform highlight;

    public Interactable focus;
    private RaycastHit rayCastHit;

    public event System.Action<Transform> OnTargetSelected;

    void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        
        if (highlight != null) {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayCastHit)) {
            highlight = rayCastHit.transform;
            Interactable interactable = rayCastHit.collider.GetComponent<Interactable>();

            if (interactable != null /*&& highlight != selection*/) {
                if (highlight.gameObject.GetComponent<Outline>() != null) {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                } else {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    outline.OutlineColor = Color.red;
                    outline.OutlineWidth = 5.0f;
                }
            } else {
                highlight = null;
            }
        }

        // Left click select.
        if (Input.GetMouseButtonDown(0)) {
            if (highlight) {
                if (selection != null) {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = rayCastHit.transform;
                selection.gameObject.GetComponent<Outline>().enabled = true;
                highlight = null;
                OnTargetSelected(selection);
            } /*else {
                if (selection) {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                    OnTargetSelected(null);
                    RemoveFocus();
                }
            }*/
        }

        // Right mouse button, interact
        if (Input.GetMouseButtonDown(1)) {
            if (highlight) {
                if (selection != null) {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = rayCastHit.transform;
                selection.gameObject.GetComponent<Outline>().enabled = true;
                highlight = null;

                Interactable interactable = rayCastHit.collider.GetComponent<Interactable>();
                if (interactable != null) {
                    SetFocus(interactable);
                }
            }/* else {
                if (selection) {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                    RemoveFocus();
                }
            }*/
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (selection) {
                selection.gameObject.GetComponent<Outline>().enabled = false;
                selection = null;
                RemoveFocus();
                OnTargetSelected(null);
            }
        }
    }

    void SetFocus(Interactable newFocus) {
        if (newFocus != focus) {
            if (focus != null) {
                focus.OnDefocused();
            }
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus() {
        if (focus != null) {
            focus.OnDefocused();            
        }
        focus = null;
    }
}
