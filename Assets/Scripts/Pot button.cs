using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotButton : MonoBehaviour
{
    private string Dishtype;
    [SerializeField] private Pot pot;
    
    [SerializeField] private Dish dish;

    private Outline outline;

    private void Start()
    {
       
        
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }


    public void MakeDish()
    {
        
        //Apple Pie     1
        if (pot.ingredients.Contains("apple") && pot.ingredients.Contains("water") && pot.ingredients.Contains("flour"))
        {
            dish.dishType = "ApplePie";
        }

        //Beef Stew     2
        else if (pot.ingredients.Contains("beef") && pot.ingredients.Contains("greenonion") && pot.ingredients.Contains("water") && pot.ingredients.Contains("salt"))
        {
            dish.dishType = "BeefStew";

        }

        //Tomato Rice   3
        else if (pot.ingredients.Contains("tomato") && pot.ingredients.Contains("rice") && pot.ingredients.Contains("chilipepper"))
        {
            dish.dishType = "TomatoRice";
        }

        //Beef Steak    4
        else if (pot.ingredients.Contains("beef") && pot.ingredients.Contains("salt"))
        {
            dish.dishType = "BeefSteak";
        }

        //Chili Cookie 5
        else if (pot.ingredients.Contains("chilipepper") && pot.ingredients.Contains("water") && pot.ingredients.Contains("flour"))
        {
            dish.dishType = "ChiliCookie";
        }

        //Rice Cake     6
        else if (pot.ingredients.Contains("rice") && pot.ingredients.Contains("water") && pot.ingredients.Contains("salt"))
        {
            dish.dishType = "RiceCake";
        }

        //Apple Salad   7
        else if (pot.ingredients.Contains("apple slice") && pot.ingredients.Contains("salt"))
        {
            dish.dishType = "RiceCake";
        }

        //Cube Steak    8
        else if (pot.ingredients.Contains("beef slice") && pot.ingredients.Contains("salt") && pot.ingredients.Contains("chilipepper slice"))
        {
            dish.dishType = "CubeSteak";
        }

        //Tomato Steak
        else if (pot.ingredients.Contains("beef") && pot.ingredients.Contains("tomato slice") && pot.ingredients.Contains("salt"))
        {
            dish.dishType = "TomatoSteak";
        }

        //Crazy soup 10
        else if (pot.ingredients.Contains("tomato") && pot.ingredients.Contains("greenonion") && pot.ingredients.Contains("chilipepper slice") && pot.ingredients.Contains("salt") && pot.ingredients.Contains("flour") && pot.ingredients.Contains("rice") && pot.ingredients.Contains("beef slice") && pot.ingredients.Contains("water"))
        {
            dish.dishType = "CrazySoup";
        }

        else
        {
            dish.dishType = "None";
        }

    }

    //만들면 pot 리스트 내용 초기화
}