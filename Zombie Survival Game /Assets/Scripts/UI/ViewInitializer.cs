using UnityEngine;

public class ViewInitializer : MonoBehaviour {
    public Viewbase entryView;

    void Awake()
    {
        Viewbase[] views = GameObject.FindObjectsOfType<Viewbase>();

        for (int i = 0; i < views.Length; i++){
            Viewbase view = views[i];

            view.viewObject.SetActive(true);

            if(view == entryView){
                continue;
            }

            view.viewObject.SetActive(false);
        }
    }
}
