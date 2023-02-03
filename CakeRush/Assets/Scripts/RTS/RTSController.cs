using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 게임 내 Entity를 관리하는 시스템 클래스
public class RTSController : MonoBehaviour
{
	[SerializeField] private GameObject clickEffectPrefab;
	ParticleSystem clickParticle;

	private Texture2D defaultCursor;
	private Texture2D enemyCursor;
	private Texture2D teamCursor;
	private Texture2D attackCursor;
	enum CursourType { None, Default, Enemy, Team, Attack };
	CursourType Curcursor;

	public List<UnitBase> unitList = new List<UnitBase>();
    public List<BuildBase> buildList = new List<BuildBase>();
	public EntityBase selectedEntity = null;
	private Camera teamCamera;
	public int maxUnit = 10;
    
	public LayerMask layerGround = 1 << 6;
    public LayerMask layerSelectable = 1 << 7;


	private Vector2 start = Vector2.zero;
	private Vector2 end = Vector2.zero;

	//sugar chocolate, wheat
	public int[] cost;
	public bool isSkill;

	void Awake()
	{
		cost = new int[3];
		unitList.Clear();
		defaultCursor = Resources.Load<Texture2D>("Textures/MouseCursor/DefaultCursor");
		attackCursor = Resources.Load<Texture2D>("Textures/MouseCursor/AttackCursor");
		teamCursor = Resources.Load<Texture2D>("Textures/MouseCursor/TeamCursor");
		enemyCursor = Resources.Load<Texture2D>("Textures/MouseCursor/EnemyCursor");

		Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);

		GameObject clickEffectObject = Instantiate(clickEffectPrefab);
		clickParticle = clickEffectObject.GetComponent<ParticleSystem>();
		//Find Team Camera
		teamCamera = Camera.main;

		ChangeCost(100, 100, 100);
	}

    private void Update() 
	{
        Click();
	}

	private void LateUpdate()
	{
		ChangeCursor();
	}

	public void ChangeCost(int cho, int sug, int dou)
	{
		cost[0] += cho;
		cost[1] += sug;
		cost[2] += dou;

		Managers.instance._ui.ChangeCost(cost[0], cost[1], cost[2]);
    }

	private void Click()
	{
		// When there is an object hitting the ray (= clicking on the unit)
		if (!IsPointerOverUIObject() && Input.GetMouseButtonDown(0))
		{
			Ray	ray = teamCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// When there is an object hitting the ray (= clicking on the unit)
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerSelectable))
			{
				if (hit.transform.gameObject.GetComponent<EntityBase>() == null)
					return;
				if (!GameProgress.instance.inGameStart)
					return;
				if (selectedEntity != null)
					selectedEntity.Deselect();

				selectedEntity = hit.transform.gameObject.GetComponent<EntityBase>();
				ShowUI();
				selectedEntity.Select();
			}
			//When ray is hitting ground.
			else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
			{
				if(selectedEntity != null)
				{
					Managers.instance._ui.ShowInGameDynamicPanel(UIManager.inGameUIs.main);
					selectedEntity.Deselect();	
					selectedEntity = null;
				}
			}
			Debug.DrawLine(teamCamera.transform.position, hit.point, Color.red, 1f);
			return;
		}
		// move units by right-clicking
		else if (!IsPointerOverUIObject() && Input.GetMouseButtonDown(1) && selectedEntity != null && isSkill == false)
		{
			if (!GameProgress.instance.inGameStart)
				return;
			RaycastHit	hit;
			Ray	ray = teamCamera.ScreenPointToRay(Input.mousePosition);
			// When the unit object (layerUnit) is clicked
			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				clickParticle.gameObject.transform.position = hit.point;
				clickParticle.Play();
			}
			Debug.DrawLine(teamCamera.transform.position, hit.point, Color.red, 1f);
		}
	}

	private bool IsPointerOverUIObject()
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}

	private void ChangeCursor()
	{
		Ray ray = teamCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerSelectable))
		{
			if (selectedEntity != null)
			{
				// if hit == ourTeam
				if (hit.collider.gameObject == selectedEntity.gameObject)
				{
					Curcursor = CursourType.Team;

				}
				//if hit == enemy
				else if (hit.collider.gameObject != selectedEntity.gameObject)
				{
					Curcursor = CursourType.Enemy;
				}
			}
			//if hit == enemy
			if (selectedEntity == null)
			{
				Curcursor = CursourType.Enemy;
			}
		}
		else Curcursor = CursourType.Default;

		if (Curcursor == CursourType.None) return;

		if (Curcursor == CursourType.Attack)
		{
			Cursor.SetCursor(teamCursor, Vector2.zero, CursorMode.Auto);
			Curcursor = CursourType.None;
		}
		else if (Curcursor == CursourType.Enemy)
		{
			Cursor.SetCursor(enemyCursor, Vector2.zero, CursorMode.Auto);
			Curcursor = CursourType.None;
		}
		else if (Curcursor == CursourType.Team)
		{
			Cursor.SetCursor(teamCursor, Vector2.zero, CursorMode.Auto);
			Curcursor = CursourType.None;
		}
		else if (Curcursor == CursourType.Default)
		{
			Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
			Curcursor = CursourType.None;
		}
	}

	private void ShowUI()
	{
		if (selectedEntity is PlayerController && selectedEntity.gameObject.tag.Contains("Me"))
		{
			Managers.instance._ui.ShowInGameDynamicPanel(UIManager.inGameUIs.player);
			return;
		}

		if (selectedEntity.gameObject.tag.Contains("Other") || selectedEntity is UnitBase || selectedEntity is MobBase || selectedEntity is BuildBase)
		{
			Managers.instance._ui.ShowInGameDynamicPanel(UIManager.inGameUIs.other);
			return;
		}
	}
}