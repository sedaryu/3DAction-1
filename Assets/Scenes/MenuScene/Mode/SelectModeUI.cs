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
    protected CameraController cameraController;
    public UnityAction onClicking;
    public UnityAction onClicked;

    protected Text headlineText;
    protected Text sublineText;
    public List<Collider> colliders = new List<Collider>();

    void Start()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        GameObject canvas = GameObject.Find("Canvas");
        headlineText = canvas.transform.Find("HeadlineText").GetComponent<Text>();
        sublineText = canvas.transform.Find("SublineText").GetComponent<Text>();
    }

    public void SetVisible()
    {
        GetComponent<Text>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void SetInvisible()
    {
        GetComponent<Text>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public async void OnPointerClick(PointerEventData eventData)
    {
        onClicking?.Invoke();
        headlineText.enabled = false;
        sublineText.enabled = false;
        Task task = await cameraController.MoveCamera(position, rotation, 3000);
        onClicked?.Invoke();
        headlineText.enabled = true;
        sublineText.enabled = true;
        headlineText.text = gameObject.name;
        colliders.ForEach(x => x.enabled = true);
    }
}
