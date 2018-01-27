using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{

    MoneyTracker moneyTracker;
    public Text gMoney;
    public Text sMoney;
    public GameObject gPanel;
    public GameObject sPanel;
    private Tile selectedTile;

    // Use this for initialization
    void Start()
    {
        moneyTracker = GameObject.FindGameObjectWithTag("GameController").GetComponent<MoneyTracker>();

        gMoney = GameObject.Find("GMoneyVal").GetComponent<UnityEngine.UI.Text>();
        sMoney = GameObject.Find("GMoneyVal").GetComponent<UnityEngine.UI.Text>();
        sPanel.SetActive(false);
        gPanel.SetActive(true);
        gMoney.text = "0";
        sMoney.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        /* if{ //hetkel teeb pmst iga frame, voiks teha iga valik
             gPanel.SetActive(false);
             sPanel.SetActive(true);
             sMoney.text = ((int)moneyTracker.money).ToString();

         }
         else{
             sPanel.SetActive(false);
             gPanel.SetActive(true);
             gMoney.text = ((int)moneyTracker.money).ToString();
         }*/
    }

    void Clicked(GameObject tile)
    {
        if (tile != null)
        {
            gPanel.SetActive(false);
            sPanel.SetActive(true);
            sMoney.text = "tere";
        }
        else{
            sPanel.SetActive(false);
            gPanel.SetActive(true);
            gMoney.text = "tere2";
        }
    }
    void OnEnable()
    {
        Tile.OnClicked += Clicked;
    }

    void OnDisable()
    {
        Tile.OnClicked -= Clicked;
    }
}
