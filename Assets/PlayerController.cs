using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Pawn m_PawnScript;

	public delegate void PlayerDeathEventHandler();
	public delegate void PlayerSpawnedEventHander();
	
	public event PlayerDeathEventHandler PlayerDeathEvent;
	public event PlayerSpawnedEventHander PlayerSpawnedEvent;

	public Transform ControlledPawnPrefab;

	public int SelectedPawnIndex = 0;
	public int LastSelectedPawnIndex = -1;

	void onPlayerDeath() { if (PlayerDeathEvent != null) PlayerDeathEvent(); }
	void onPlayerSpawned() { if (PlayerSpawnedEvent != null) PlayerSpawnedEvent(); }


	public GameObject CurrentPawnGameObject;
	public GameObject[] SpawnPoints;
	public GameObject[] PawnGameObjects;

	public int alivePawns;

	public Record[] CachedRecord_0;
	public Record[] CachedRecord_1;
	public Record[] CachedRecord_2;
	public Record[] CachedRecord_3;

	public Pawn PawnScript
	{
		get
		{
			if( m_PawnScript ==  null)
			{
				m_PawnScript = CurrentPawnGameObject.GetComponent<Pawn>();
			}

			return m_PawnScript;
		}

	}
	void Awake()
	{
		GameInfo.GameStateCtrl.GameStartEvent += onGameStarted;
		GameInfo.GameStateCtrl.GameEndEvent += onGameEnded;

	}
	// Use this for initialization
	void Start () {

		for (int i = 0; i < SpawnPoints.Length; i++)	
		{
			Record[] cr = new Record[] {new Record(Vector3.zero, false, Vector3.zero, false)};
			SetCachedRecord(i, cr);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}



	public Record[] GetCachedRecord(int index)
	{
		switch (index) 
		{
		case 0:
			return CachedRecord_0;
		case 1:
			return CachedRecord_1;
		case 2:
			return CachedRecord_2;
		case 3:
			return CachedRecord_3;
		default:
			return null;
		}
	}

	public void SetCachedRecord(int index, Record[] newRecord)
	{
		switch (index) 
		{
		case 0:
			CachedRecord_0 = newRecord;
			return;
		case 1:
			CachedRecord_1 = newRecord;
			return;
		case 2:
			CachedRecord_2 = newRecord;
			return;
		case 3:
			CachedRecord_3 = newRecord;
			return;
		default:
			return;
		}
	}

	public bool ReadyToStartGame()
	{
		//todo: make sure all copies are spawned
		return true;
	}

	public void SelectPawn(int index)
	{
		for (int i = 0; i < PawnGameObjects.Length; i++) 
		{
			if(i == index)
			{
				PawnGameObjects[i].AddComponent<ControlledInput>();
				PawnGameObjects[i].AddComponent<InputRecorder>();

				Pawn p = PawnGameObjects[i].GetComponent<Pawn>();
				p.SetColor(Color.red);

				CurrentPawnGameObject = PawnGameObjects[i];

			}
			else
			{
				RecordedInput recordedInput = PawnGameObjects[i].AddComponent<RecordedInput>() as RecordedInput;
				recordedInput.SetRecord(GetCachedRecord(i));
				Pawn p = PawnGameObjects[i].GetComponent<Pawn>();
				p.SetColor(Color.blue);
			}

		
		}
		//CurrentPawnGameObject = GameObject.FindGameObjectWithTag ("Player");
	}

	public bool SpawnPawns()
	{
		PawnGameObjects = new GameObject[SpawnPoints.Length]; 
		alivePawns = 0;

		for (int i = 0; i < SpawnPoints.Length; i++) 
		{
			Transform t = Instantiate(ControlledPawnPrefab, SpawnPoints[i].transform.position,  Quaternion.identity) as Transform;
			PawnGameObjects[i] = t.gameObject; 
			alivePawns++;
		}

		return true;
		//TODO: spawn each pawn
	}

	void onGameStarted()
	{
		SelectPawn (SelectedPawnIndex);

		InputRecorder recorder = CurrentPawnGameObject.GetComponent<InputRecorder> () as InputRecorder;
		recorder.StartRecording ();

		for (int i = 0; i < SpawnPoints.Length; i++) 
		{
			if( i == SelectedPawnIndex)
				continue;

			PlatformInputController controller = PawnGameObjects[i].GetComponent<PlatformInputController>() as PlatformInputController;
			controller.IsRecordedInput = true;

			RecordedInput recordedInput = PawnGameObjects[i].GetComponent<RecordedInput>() as RecordedInput;
			recordedInput.bPlaying = true;
		}
	}

	void onGameEnded()
	{
		StopRecorder ();

		LastSelectedPawnIndex = SelectedPawnIndex;

		SelectedPawnIndex++;

		if (SelectedPawnIndex >= SpawnPoints.Length)
			SelectedPawnIndex = 0;


		foreach( GameObject go in GameObject.FindGameObjectsWithTag("Pawn"))
        {
			KillPawnrObject(go);
		}

		foreach( GameObject go in GameObject.FindGameObjectsWithTag("Pawn"))
		{
			if(go != null)
				Destroy(go);
		}

		foreach( GameObject go in GameObject.FindGameObjectsWithTag("bullet"))
		{
			Destroy(go);
		}
	}
	private void StopRecorder()
	{
		InputRecorder recorder = CurrentPawnGameObject.GetComponent<InputRecorder> () as InputRecorder;
		SetCachedRecord (SelectedPawnIndex, recorder.StopRecording ());
	}
	public void PawnKilled(GameObject gObject)
	{
		alivePawns--;

		if (gObject == CurrentPawnGameObject) 
		{
			StopRecorder ();
		}


		KillPawnrObject (gObject);

		if (alivePawns <= 1) 
		{
			GameInfo.GameStateCtrl.EndGame();
		}
	}

	private void KillPawnrObject(GameObject go)
	{
		if (go == CurrentPawnGameObject)
			StopRecorder ();

		Destroy (go);
	}

}
