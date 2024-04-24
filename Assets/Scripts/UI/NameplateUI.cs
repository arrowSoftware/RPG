using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterStats))]
public class NameplateUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    float visibleTime = 5.0f; // Time to keep naeplate active after not taking damge.
    float lastMadeVisibleTime;
    public Canvas canvas;

    Transform ui;
    Image healthSlider;
    Transform cam;

    void Start()
    {
        cam = Camera.main.transform;

        ui = Instantiate(uiPrefab, canvas.transform).transform;
        healthSlider = ui.GetChild(0).GetComponent<Image>();
        ui.GetChild(1).GetComponent<TMP_Text>().SetText(transform.name);

        int level = transform.GetComponent<CharacterStats>().level.GetValue();
        ui.GetChild(3).GetComponent<TMP_Text>().SetText(level.ToString());

        ui.gameObject.SetActive(false);

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void LateUpdate()
    {
        if (ui != null) {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if (Time.time - lastMadeVisibleTime > visibleTime) {
                ui.gameObject.SetActive(false);
            }
        }
    }

    void OnHealthChanged(int maxHealth, int currentHealth) {
        if (ui != null) {
            ui.gameObject.SetActive(true);
            lastMadeVisibleTime = Time.time;

            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;

            ui.GetChild(2).GetComponent<TMP_Text>().SetText((healthPercent*100).ToString("f1") + "% - " + currentHealth.ToString());

            if (currentHealth <= 0) {
                Destroy(ui.gameObject);
            }
        }
    }
}
