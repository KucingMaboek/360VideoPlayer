using UnityEngine;
using UnityEngine.UI;

public class LoadingController : UIController
{
    private LevelLoader _levelLoader;

    [SerializeField] private Image loadingFill;
    [SerializeField] private Text loadingText;

    // Start is called before the first frame update
    void Start()
    {
        _levelLoader = GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        loadingFill.fillAmount = _levelLoader.Progress;
        loadingText.text = _levelLoader.Progress * 100 + "%";
    }
}