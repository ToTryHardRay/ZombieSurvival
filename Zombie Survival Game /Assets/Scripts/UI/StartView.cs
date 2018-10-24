using UnityEngine;
using UnityEngine.UI;

public class StartView : Viewbase {
    [Header("View References")]
    public Viewbase optionsView;
    public Button startButton;
    public Button optionsButton;
    public Button exitButton;
    public GameObject lobbyCam;
    public GameObject inGameUI;
    public GameObject mainUI;

    protected override void OnInit()
    {
        startButton.onClick.AddListener(() =>
        {
            lobbyCam.SetActive(false);
            mainUI.SetActive(false);
            inGameUI.SetActive(true);
            GameManager.instance.startGame();
        });

        optionsButton.onClick.AddListener(() =>
        {
            this.Hide();
            optionsView.Show();
        });

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    protected override void OnShow()
    {
        lobbyCam.SetActive(true);
        mainUI.SetActive(true);
        inGameUI.SetActive(false);
    }
}
