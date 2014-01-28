using UnityEngine;
using System.Collections;

	public enum Platform {COMPUTER, MOBILE};

	public class GameInfo : MonoBehaviour {
	
		//private members
		private static GameStateController m_GameStateCtrl;
	private static PlayerController m_PlayerCtrl;

		//public members
		public static Platform CurrentPlatform;
       
		public static GameStateController GameStateCtrl 
		{
			get 
			{
				if (m_GameStateCtrl == null)
						m_GameStateCtrl = GameObject.FindGameObjectWithTag ("Controllers").GetComponent<GameStateController> () as GameStateController;

				return m_GameStateCtrl;
			}

		}
        
		public static PlayerController PlayerCtrl
		{
			get 
			{
				if(m_PlayerCtrl == null)
					m_PlayerCtrl = GameObject.FindGameObjectWithTag("Controllers").GetComponent<PlayerController>();
				
				return m_PlayerCtrl;
			}
		}

        // Use this for initialization
		void Start () {
            GameStateCtrl.GameStateChanged += onGameStateChanged;
			InitiateForCurrentDevice();
		}
		
		// Update is called once per frame
		void Update () {
		
			if (GameStateCtrl.CurrentGameState == GameState.UNINITLAIZED)
			{
				GameStateCtrl.StartLoading();
			}

		}

        void InitiateForCurrentDevice()
        {
            switch (Application.platform)
            {
            case RuntimePlatform.Android:
                return;
            case RuntimePlatform.IPhonePlayer:
                CurrentPlatform = Platform.MOBILE;
                return;
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsWebPlayer:
                CurrentPlatform = Platform.COMPUTER;
                return;
            default:
                return;
            }
        }
	

        private void onGameStateChanged(GameState newState, GameState oldState)
        {
            switch (newState) 
            {
                case GameState.PLAYING:
                    break;
                case GameState.STARTSCREEN:
                    break;
                case GameState.ENDSCREEN:
                    break;
                case GameState.LOADING:
                    break;
                case GameState.UNINITLAIZED:
                    break;
            }

	    }

    }

