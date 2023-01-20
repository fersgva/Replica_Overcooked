 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    //public IngredientType type;
    public List<IngredientType> stackIngredients = new List<IngredientType>();

    private void Awake()
    {
        //stackIngredients.Add(this.type);
    }
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

        //Primario con pan
        Buns_CoockedSteak,
        Buns_Lettuce,
        Buns_Tomato,
        Buns_Onion,
        Buns_Cheese,

        //Pan + carne + otro.
        Buns_CoockedSteak_Lettuce,
        Buns_CoockedSteak_Tomato,
        Buns_CoockedSteak_Onion,
        Buns_CoockedSteak_Cheese,

        //Pan + lechuga + otro.
        Buns_Lettuce_Tomato,
        Buns_Lettuce_Onion,
        Buns_Lettuce_Cheese,

        //Pan + tomate + otro
        Buns_Tomato_Onion,
        Buns_Tomato_Cheese,

        //Pan + cebolla + otro
        Buns_Onion_Cheese,

        //Sin pan
        CoockedSteak_Lettuce,
        CoockedSteak_Tomato,
        CoockedSteak_Onion,
        CoockedSteak_Cheese,

        Lettuce_Tomato,
        Lettuce_Onion,
        Lettuce_Cheese,

        Tomato_Onion,
        Tomato_Cheese,

        Onion_Cheese,

        //Finales.
        SimpleBurger, //Pan y carne.
        CheeseBurger, //Pan, carne y queso.
        RedBurger, //Pan, carne, queso y tomate.
        GreenBurger //Pan, carne, cebolla, tomate y lechuga.

                

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
