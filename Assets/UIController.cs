using UnityEngine;
using System.Collections;


	public class UIController : MonoBehaviour {



		//public members
		public bool bDisplayScore;
		public bool bDisplayEndScreen;
		public bool bDisplayStartScreen;
	

		// Use this for initialization
		void Start () {
            GameInfo.GameStateCtrl.GameStateChanged += onGameStateChanged;
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void OnGUI () 
        {

		GUI.skin.label.fontSize = 20;
            //update Gui objects
			if (bDisplayStartScreen) 
			{
				UpdateStartScreen();
                if(Input.anyKey)
				GameInfo.GameStateCtrl.TryToStartGame();
			}

			if (bDisplayScore)
				UpdateTime();

			if (bDisplayEndScreen)
            {
                UpdateEndScreen();
				if(Input.anyKey)
                    GameInfo.GameStateCtrl.TryToStartGame();
            }

			UpdateControls ();
            //update debug overlay
            //if(bDebugOverlay)
                //DebugGui.UpdateSliders();

		}

		private void onGameStateChanged(GameState newState, GameState oldState)
		{
			switch (oldState) 
			{
				case GameState.PLAYING:
					hidePlayingUI();
					break;
				case GameState.STARTSCREEN:
					hideStartScreenUI();
					break;
				case GameState.ENDSCREEN:
					hideEndScreenUI();
					break;
				case GameState.LOADING:
					hideLoadingUI();
					break;
				case GameState.UNINITLAIZED:
					break;
			}

			switch (newState) 
			{
				case GameState.PLAYING:
					showPlayingUI();
					break;
				case GameState.STARTSCREEN:
					showStartScreenUI();
					break;
				case GameState.ENDSCREEN:
					showEndScreenUI();
					break;
				case GameState.LOADING:
					showLoadingUI();
					break;
				case GameState.UNINITLAIZED:
					break;
			}


		}

		private void showPlayingUI()
		{
			bDisplayScore = true;
		}
		private void hidePlayingUI()
		{
			bDisplayScore = false;
		}
		private void showStartScreenUI()
		{
			bDisplayStartScreen = true;
		}
		private void hideStartScreenUI()
		{
			bDisplayStartScreen = false;
		}
		private void showEndScreenUI()
		{
			bDisplayEndScreen = true;
		}
		private void hideEndScreenUI()
		{
			bDisplayEndScreen = false;
		}
		private void showLoadingUI()
		{
		}
		private void hideLoadingUI()
		{
		}
		private void UpdateTime()
		{
            GUI.BeginGroup(new Rect(10,10,150, 50));
			GUI.Label (new Rect (0, 0, 100, 30), "Time Remaining");
			GUI.Label (new Rect (50, 20, 50, 30), GameInfo.GameStateCtrl.m_RemainingTime.ToString("F2"));
            GUI.EndGroup();
		}
		private void UpdateStartScreen()
		{
			GUI.Label (new Rect (50, 50, 150, 150), "Start Screen");
		}

		private void UpdateControls()
			{
			GUI.BeginGroup(new Rect(5,200,200, 100));
			GUI.Label (new Rect (0, 0, 200, 30), "Move: A, D, Left, and Right Arrow keys");
			GUI.Label (new Rect (0, 20, 200, 30), "Jump: Space bar");
			GUI.Label (new Rect (0, 40, 200, 30), "Shot: Mouse click");

			GUI.EndGroup();
		}

		private void UpdateEndScreen()
		{
			GUI.Label(new Rect (160,10,150,50), "End Screen");
		}
	}
