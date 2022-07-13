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

	private UiManager UIMng;

	void Awake()
	{
		cost = new int[3];
		defaultCursor = Resources.Load<Texture2D>("Textures/MouseCursor/DefaultCursor");
		attackCursor = Resources.Load<Texture2D>("Textures/MouseCursor/AttackCursor");
		teamCursor = Resources.Load<Texture2D>("Textures/MouseCursor/TeamCursor");
		enemyCursor = Resources.Load<Texture2D>("Textures/MouseCursor/EnemyCursor");

		Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);

		GameObject clickEffectObject = Instantiate(clickEffectPrefab);
		clickParticle = clickEffectObject.GetComponent<ParticleSystem>();
		//Find Team Camera
		teamCamera = Camera.main;

		UIMng = GameManager.instance.UIManager;

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

		UIMng.ChangeCost(cost[0], cost[1], cost[2]);
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
					UIMng.ShowInGameDynamicPanel(UiManager.inGameUIs.main);
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
		if(selectedEntity.gameObject.tag.Contains("Other") || selectedEntity is UnitBase || selectedEntity is MobBase || selectedEntity is BuildBase)
		{
			UIMng.ShowInGameDynamicPanel(UiManager.inGameUIs.other);
			return;
		}
		if (selectedEntity is PlayerController)
		{
			UIMng.ShowInGameDynamicPanel(UiManager.inGameUIs.player);
			return;
		}

	}


	/*
		public EntityBase selectedEnemyEntity;
		public List<UnitBase> selectedUnitList = new List<UnitBase>();
		public List<BuildBase> selectedBuildList = new List<BuildBase>();
		[SerializeField] RectTransform dragRectangle;
		private Rect dragRect;

		void Click()
		{
			// When there is an object hitting the ray (= clicking on the unit)
			if ( Input.GetMouseButtonDown(0) )
			{
				Ray	ray = teamCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				// When there is an object hitting the ray (= clicking on the unit)
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerSelectable))
				{
					if (hit.transform.gameObject.GetComponent<EntityBase>() == null)
						return;

					if(hit.transform.gameObject.CompareTag("Unit"))
					{
						selectedBuildList.Clear();
						if (Input.GetKey(KeyCode.LeftShift))
						{
							ShiftClickSelectUnit(hit.transform.gameObject.GetComponent<UnitBase>());
						}
						else
						{
							ClickSelectUnit(hit.transform.gameObject.GetComponent<UnitBase>());
						}
					}
					else if(hit.transform.gameObject.CompareTag("Build"))
					{
						selectedUnitList.Clear();

						if (Input.GetKey(KeyCode.LeftShift))
						{
							ShiftClickSelectUnit(hit.transform.gameObject.GetComponent<BuildBase>());
						}
						else
						{
							ClickSelectUnit(hit.transform.gameObject.GetComponent<BuildBase>());
						}
					}

					Debug.DrawLine(teamCamera.transform.position, hit.point, Color.red, 1f);
					return;
				}
					//When ray is hitting ground.
				else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
				{
					DeselectAllUnit();

					Debug.DrawLine(teamCamera.transform.position, hit.point, Color.red, 1f);
				}

			}
			// move units by right-clicking
			if (Input.GetMouseButtonDown(1))
			{
				RaycastHit	hit;
				Ray	ray = teamCamera.ScreenPointToRay(Input.mousePosition);
				// When the unit object (layerUnit) is clicked
				if ( Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
				{
					//MoveSelectedUnits(hit.point);
					Debug.DrawLine(teamCamera.transform.position, hit.point, Color.red, 1f);
					return;
				}
				else
				{
					Debug.DrawLine(teamCamera.transform.position, hit.point, Color.red, 1f);
				}
			}
		}

		void Drag()
		{
			if (Input.GetMouseButtonDown(0))
			{
				start = Input.mousePosition;
				dragRect = new Rect();
			}

			if ( Input.GetMouseButton(0) )
			{
				end = Input.mousePosition;

				// Represents the drag range as an image while dragging with the mouse clicked
				DrawDragRectangle();

				Ray ray = teamCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if ( Physics.Raycast(ray, out hit, Mathf.Infinity))
					Debug.DrawLine(teamCamera.transform.position, hit.point, Color.green, 1f);
			}

			if (Input.GetMouseButtonUp(0))
			{
				// Select the unit within the drag range when the mouse click ends
				CalculateDragRect();
				SelectUnits();

				// Make the drag range invisible when you end the mouse click
				// Set the start and end positions to (0, 0) and draw the drag range
				start = end = Vector2.zero;
				DrawDragRectangle();
			}
		}

		/// Called when a unit is selected with a mouse click
		public void ClickSelectUnit<T>(T newEntity) where T : EntityBase
		{
			// Remove all units selected in the zone
			DeselectAllUnit();
			SelectUnit<T>(newEntity);
		}

		/// Invoked when selecting a unit with Shift+mouse click
		public void ShiftClickSelectUnit<T>(T newEntity)
		{
			if(typeof(T).GetType() == typeof(UnitBase))
			{
				if ( selectedUnitList.Contains(newEntity as UnitBase))
				{
					DeselectUnit(newEntity as UnitBase);
				}
				// If you choose a new unit
				else
				{
					SelectUnit(newEntity as UnitBase);
				}
			}
			else if(typeof(T).GetType() == typeof(BuildBase))
			{
				if ( selectedBuildList.Contains(newEntity as BuildBase) )
				{
					DeselectUnit(newEntity as BuildBase);
				}
				// If you choose a new unit
				else
				{
					SelectUnit(newEntity as BuildBase);
				}
			}
			// If you select a previously selected unit
			// if ( selectedUnitList.Contains(newEntity) )
			// {
			// 	DeselectUnit(newEntity);
			// }
			// // If you choose a new unit
			// else
			// {
			// 	SelectUnit(newEntity);
			// }
		}

		/// Called when selecting a unit by mouse dragging
		public void DragSelectUnit<T>(T newEntity) where T : EntityBase
		{
			// If you choose a new unit
			if (newEntity.GetType() == typeof(UnitBase))
			{
				if (!selectedUnitList.Contains(newEntity as UnitBase))
				{
					SelectUnit(newEntity);
				}
			}
			else if(newEntity.GetType() == typeof(BuildBase))
			{
				if (!selectedBuildList.Contains(newEntity as BuildBase))
				{
					SelectUnit(newEntity);
				}
			}
		}

		/// Called to move all selected units
		public void MoveSelectedUnits(Vector3 end)
		{
			for ( int i = 0; i < selectedUnitList.Count; ++ i )
			{
				selectedUnitList[i].Move(end);		
			}
		}

		/// Called when all units are deselected
		public void DeselectAllUnit()
		{
			for ( int i = 0; i < selectedUnitList.Count; ++ i )
			{
				selectedUnitList[i].Deselect();
			}
			for ( int j = 0; j < selectedBuildList.Count; ++ j )
			{
				selectedBuildList[j].Deselect();
			}
			selectedUnitList.Clear();
			selectedBuildList.Clear();
		}

		/// NewUnit selection set received as a parameter
		private void SelectUnit<T>(T newEntity) where T : EntityBase
		{
			// Method to be called when a unit is selected
			newEntity.Select();
			// Save the selected unit information to the list
			if(typeof(T).Name == "UnitBase")
			{
				selectedUnitList.Add(newEntity.gameObject.GetComponent<UnitBase>());
			}
			else if(typeof(T).Name == "BuildBase")
			{
				selectedBuildList.Add(newEntity.gameObject.GetComponent<BuildBase>());
			}
		}

		/// Set deselection of newEntity received as a parameter
		private void DeselectUnit<T>(T newEntity) where T : EntityBase
		{
			// Method called when unit is released
			newEntity.Deselect();
			// Delete the selected unit information from the list

			if(newEntity.GetType() == typeof(UnitBase))
			{
				selectedUnitList.Remove(newEntity as UnitBase);
			}
			else if(newEntity.GetType() == typeof(BuildBase))
			{
				selectedBuildList.Remove(newEntity as BuildBase);
			}
		}

		private void DrawDragRectangle()
		{
			// Position of Image UI indicating drag range
			dragRectangle.position	= (start + end) * 0.5f;
			// Size of Image UI indicating drag range
			dragRectangle.sizeDelta	= new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
		}

		private void CalculateDragRect()
		{
			if ( Input.mousePosition.x < start.x )
			{
				dragRect.xMin = Input.mousePosition.x;
				dragRect.xMax = start.x;
			}
			else
			{
				dragRect.xMin = start.x;
				dragRect.xMax = Input.mousePosition.x;
			}

			if ( Input.mousePosition.y < start.y )
			{
				dragRect.yMin = Input.mousePosition.y;
				dragRect.yMax = start.y;
			}
			else
			{
				dragRect.yMin = start.y;
				dragRect.yMax = Input.mousePosition.y;
			}
		}

		private void SelectUnits()
		{
			// check all units
			foreach (UnitBase unit in unitList)
			{
				// Converts the unit's world coordinates to screen coordinates to check if they are within the drag range
				if ( dragRect.Contains(teamCamera.WorldToScreenPoint(unit.transform.position)) == true)
				{
					DragSelectUnit(unit);
				}
			}
			foreach (BuildBase build in buildList)
			{
				// Converts the unit's world coordinates to screen coordinates to check if they are within the drag range
				if ( dragRect.Contains(teamCamera.WorldToScreenPoint(build.transform.position)) == true)
				{
					DragSelectUnit(build);
				}
			}
		}
		*/
}