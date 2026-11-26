using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(SphereCollider))]
public class FoodButton : MonoBehaviour
{
    public event EventHandler OnUsed;

    [SerializeField] private Material m_material;
    [SerializeField] private Material m_materialPushed;

    private MeshRenderer buttonMeshRenderer;
    private Transform buttonTransform;
    private bool canUseButton;

    private void Awake() {
        buttonMeshRenderer = GetComponent<MeshRenderer>();
        canUseButton = true;
    }


    private void Start() {
        ResetButton();
    }


    public bool CanUseButton(){ return canUseButton; }

    public void UseButton() {
        if (canUseButton) {
            buttonMeshRenderer.material = m_materialPushed;
            buttonTransform.localScale = new Vector3(.5f, .2f, .5f);
            canUseButton = false;

            OnUsed?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetButton() {
        buttonMeshRenderer.material = m_material;
        buttonTransform.localScale = new Vector3(.5f, .5f, .5f);

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, UnityEngine.Random.Range(-2.0f, +2.0f));

        canUseButton = true;
    }
}
