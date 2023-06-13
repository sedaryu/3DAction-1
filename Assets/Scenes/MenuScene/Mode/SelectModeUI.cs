using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SelectModeUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;
    protected string initialText;

    //output
    protected Text headlineText;
    protected Text sublineText;

    //input
    private Text modeText;
    private BoxCollider2D modeCollider;
    public List<Collider> colliders = new List<Collider>();
    public UnityAction onClicking;
    public UnityAction onClicked;

    //camera
    protected CameraController cameraController;
    

    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        GameObject canvas = GameObject.Find("Canvas");
        headlineText = canvas.transform.Find("HeadlineText").GetComponent<Text>();
        sublineText = canvas.transform.Find("SublineText").GetComponent<Text>();
        modeText = GetComponent<Text>();
        modeCollider = GetComponent<BoxCollider2D>();
    }

    public void SetVisible()
    {
        modeText.enabled = true;
        modeCollider.enabled = true;
    }

    public void SetInvisible()
    {
        modeText.enabled = false;
        modeCollider.enabled = false;
    }

    public async void OnPointerClick(PointerEventData eventData)
    {
        onClicking?.Invoke();
        headlineText.enabled = false;
        sublineText.enabled = false;
        Task task = await cameraController.MoveCamera(position, rotation, 3000);

        headlineText.enabled = true;
        sublineText.enabled = true;
        headlineText.text = gameObject.name;
        sublineText.text = initialText;
        onClicked?.Invoke();
        colliders.ForEach(x => x.enabled = true);
    }
}
