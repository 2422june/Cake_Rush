using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{
    enum CursorType
    {
        Default,
        Enemy,
        Team,
        Attack
    }

    [SerializeField] CursorType type;
    Texture2D[] cursorTextures = new Texture2D[4];
    int mask = 1 << (int) Define.Layer.Ground;

    private void Start()
    {
        string []textureNames = System.Enum.GetNames(typeof(CursorType));

        for(int i = 0; i < cursorTextures.Length; i++)
        {
            cursorTextures[i] = Managers.Resource.Load<Texture2D>($"Textures/MouseCursor/{textureNames[i]}Cursor");
        }
    }

    private void Update()
    {
        UpdateCursor();
    }

    private void UpdateCursor()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            type = CursorType.Default;
            Debug.Log("Check");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Ground)
            {
                Cursor.SetCursor(cursorTextures[(int)CursorType.Default], Vector2.zero, CursorMode.Auto);
            }
            else if(hit.collider.gameObject.layer == (int)Define.Layer.Ally)
            {
                Cursor.SetCursor(cursorTextures[(int)CursorType.Team], Vector2.zero, CursorMode.Auto);
            }
            else if (hit.collider.gameObject.layer == (int)Define.Layer.Enemy)
            {
                Cursor.SetCursor(cursorTextures[(int)CursorType.Enemy], Vector2.zero, CursorMode.Auto);
            }
        }
    }
}
