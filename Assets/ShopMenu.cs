using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] Save save;
    [SerializeField] Text moneyText;
    [SerializeField] Text[] itemText;
    public Text shopkeeperTalking;
    public GameObject shopkeeperTextContent;
    float levelDelay = 1f;
    #region
    public static ShopMenu Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        for (int index = 0; index < itemText.Length; index++)
        {
            if (itemText[index] != null)
            {
                Item item = itemText[index].GetComponentInChildren<Item>();
                itemText[index].text = item.name + ": " + item.amount.ToString() + " / " + item.cost.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money:" + save.money.ToString();
    }

    public void Close()
    {
        Invoke("LoadOutSideLevel", levelDelay);
    }

    private void LoadOutSideLevel()
    {
        SceneManager.LoadScene(2); //todo allow for 
    }
}
