using UnityEngine;
using UnityEngine.UI;

public class OptionsView : Viewbase {
    [Header("View References")]
    public Viewbase startView;
    public Button backToStartButton;

    protected override void OnInit(){
        backToStartButton.onClick.AddListener(() =>
        {
            this.Hide();
            startView.Show();
        });
    }
}
