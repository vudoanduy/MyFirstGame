using UnityEngine;

public class ManageListMenu: MonoBehaviour
{
    ManageShopGUI manageShopGUI;
    
    //
    void Start()
    {
        manageShopGUI = GameObject.Find("ShopManager").GetComponent<ManageShopGUI>();
    }

    //
    public void Coin(){
        manageShopGUI.SetColor(0);
    }   
    public void Item(){
        manageShopGUI.SetColor(1);      
    }   
    public void Pet(){
        manageShopGUI.SetColor(2);
    }
    public void Pack(){
        manageShopGUI.SetColor(3);     
    } 
}
