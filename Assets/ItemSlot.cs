using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Save save;
    [SerializeField] ShopMenu menu;
    private void Start()
    {
        menu = ShopMenu.Instance;
    }
    public void Buy()
    {
        if (save.money >= item.cost)
        {
            save.money -= item.cost;
            save.fuel += item.amount;
        }
        else
        {
            NotEnoughMoney();
        }
    }

    private void NotEnoughMoney()
    {
        menu.shopkeeperTextContent.SetActive(true);
        int moneyMore = item.cost - save.money;
        menu.shopkeeperTalking.text = "sorry boy your only have " + save.money + " left to buy " + item.name + " you need "+ moneyMore + " more. ";
        Invoke("TextInvisible", 3f);
    }

    private void TextInvisible()
    {
        menu.shopkeeperTextContent.SetActive(false);
    }
}
