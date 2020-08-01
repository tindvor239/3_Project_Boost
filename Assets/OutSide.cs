using UnityEngine;
using UnityEngine.SceneManagement;
public class OutSide : MonoBehaviour
{
    public void LoadShopLevel()
    {
        Invoke("LoadShop", 1f);
    }

    public void LoadPlayLevel()
    {
        Invoke("LoadPlay", 1f);
    }

    private void LoadShop()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadPlay()
    {
        SceneManager.LoadScene(0);
    }
}
