 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    //public IngredientType type;
    public List<IngredientType> stackIngredients = new List<IngredientType>();

    [Header("Only if required")]
    [SerializeField] GameObject progressBarCanvas;
    [SerializeField] GameObject ingredientAfterAction;
    Slider slider;

    public enum IngredientType
    {

        //Primarios
        Steak,
        CoockedSteak,
        Lettuce,
        Tomato,
        Onion,
        Cheese,
        Buns,

        ////Primario con pan
        //Buns_CoockedSteak,
        //Buns_Lettuce,
        //Buns_Tomato,
        //Buns_Onion,
        //Buns_Cheese,

        ////Pan + carne + otro.
        //Buns_CoockedSteak_Lettuce,
        //Buns_CoockedSteak_Tomato,
        //Buns_CoockedSteak_Onion,
        //Buns_CoockedSteak_Cheese,

        ////Pan + lechuga + otro.
        //Buns_Lettuce_Tomato,
        //Buns_Lettuce_Onion,
        //Buns_Lettuce_Cheese,

        ////Pan + tomate + otro
        //Buns_Tomato_Onion,
        //Buns_Tomato_Cheese,

        ////Pan + cebolla + otro
        //Buns_Onion_Cheese,

        ////Sin pan
        //CoockedSteak_Lettuce,
        //CoockedSteak_Tomato,
        //CoockedSteak_Onion,
        //CoockedSteak_Cheese,

        //Lettuce_Tomato,
        //Lettuce_Onion,
        //Lettuce_Cheese,

        //Tomato_Onion,
        //Tomato_Cheese,

        //Onion_Cheese,

        ////Finales.
        //SimpleBurger, //Pan y carne.
        //CheeseBurger, //Pan, carne y queso.
        //RedBurger, //Pan, carne, queso y tomate.
        //GreenBurger, //Pan, carne, cebolla, tomate y lechuga.


        //Utensilios
        Pan,
        Plate,

        //Cortados
        Sliced_Steak,
        Sliced_Lettuce,
        Sliced_Tomato,
        Sliced_Onion,
        Sliced_Cheese


    }

    private void Awake()
    {
        if (progressBarCanvas != null) //No todos van a llevarlo
        {
            slider = progressBarCanvas.GetComponentInChildren<Slider>();
        }
    }

    public IEnumerator TriggerAction(Player pl, PlayerDetections plDetections, float duration)
    {

        progressBarCanvas.SetActive(true);

        float initValue = slider.value;
        float finalValue = 1f;
        float timer = 0;
        while (timer < duration)
        {
            slider.value = Mathf.Lerp(initValue, finalValue, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        //Mejor con Destroy e Instantiate.
        GameObject newIngredient = Instantiate(ingredientAfterAction, transform.position, Quaternion.identity);

        //Importante: Tenemos que actualizar la lista.
        //plDetections.closePickables.Add(newIngredient);
        plDetections.closePickables.Remove(gameObject);

        pl.anim.SetBool("chopping", false);
        Destroy(gameObject);
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
