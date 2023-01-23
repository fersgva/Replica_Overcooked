using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTable : MonoBehaviour, IActionable
{
    [SerializeField] GameObject progressBar;
    //Slider slider;
    private void Awake()
    {
        //slider = progressBar.GetComponentInChildren<Slider>();
    }

    public IEnumerator TriggerAction(float duration)
    {

        //progressBar.SetActive(true);

        ////float initValue = slider.value;
        //float finalValue = 1f;
        //float timer = 0;
        //while (timer < duration)
        //{
        //    //slider.value = Mathf.Lerp(initValue, finalValue, timer / duration);
        //    timer += Time.deltaTime;
        //    yield return null;
        //}
        yield return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
