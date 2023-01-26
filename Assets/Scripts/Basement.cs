using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement : LimitedCompany
{

    private Dictionary<Item, int> resources=new();
    [SerializeField] int walletToSet;
    [SerializeField] private string recipeToSet;
    [SerializeField] private GameObject factoryPrefab;

    // Start is called before the first frame update
    void Start()
    {
        walletToSet = wallet;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        recipe = Recipe.GetRecipe(recipeToSet);
        if (recipe == null)
        {
            print("Recipe is not chosen");
            return;
        }
        market.Buy(this, recipe.factory, 1, true);

        if (!(factoryNumber >= 1 || Recipe.GetRecipe("assembling-machine-0").invented
              || recipe == Recipe.GetRecipe("iron-plate") || recipe == Recipe.GetRecipe("stone-furnace")
              || recipe == Recipe.GetRecipe("engine-unit") || recipe == Recipe.GetRecipe("automation-science-pack")
              || recipe == Recipe.GetRecipe("iron-ore") || recipe == Recipe.GetRecipe("stone")
              || recipe == Recipe.GetRecipe("burner-mining-drill") || recipe == Recipe.GetRecipe("iron-gear-wheel")
           ))
        {
            print("Factory is not installed");
            return;
        }

        Factory newFactory = Instantiate<GameObject>(factoryPrefab, this.transform.position, Quaternion.Euler(Vector3.zero)).GetComponent<Factory>();
        newFactory.InstallRecipe(recipe);
        //newFactory.wallet = wallet; TODO
        if (newFactory.factory != recipe.factory)
        {
            print("ERROR! Creation new factory failed. Destroying object");
            Destroy(newFactory.gameObject);
            return;
        } else
        {
            print("Factory for "+recipe.recipeName+" was sucsessesfully created");
            Destroy(this.gameObject);
        }
        

    }

    internal override void ConfirmBuy(Item item, int number, int transaction, bool addFactory)
    {
        DecreaseWallet(transaction);

        if (addFactory && item == factory)
            factoryNumber += number;
        else
            print("ERROR!! Trying buy not factory for Basement");
    }
}
