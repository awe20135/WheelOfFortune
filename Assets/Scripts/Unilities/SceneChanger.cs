using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int _sceneNumberToSwitch;
    [SerializeField] private float _delay = .5f; // seconds

    private Button _button;

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _button = GetComponent<Button>();
        if (!_button)
        {
            Debug.LogError($"Undefined Button component on {gameObject}");
        }
    }

    private void OnEnable()
    {
        if (!_button)
            InitializeComponents();

        _button.onClick.AddListener(SceneSwitch);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SceneSwitch);
    }

    private void SceneSwitch()
    {
        StartCoroutine(DelayedSceneSwitch(_sceneNumberToSwitch, _delay));
    }

    private IEnumerator DelayedSceneSwitch(int sceneToChange, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToChange);
    }
}
