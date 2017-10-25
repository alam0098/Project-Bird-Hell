﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanelController : MonoBehaviour
{
	public PlayerInfo PlayerInfo = new PlayerInfo();
	public Color CursorColor;

	private LobbyController Lobby;
	private CursorController Cursor;
	private GameObject StartToJoinPanel;
	private GameObject JoinedGamePanel;

	public bool HasJoinedGame
	{
		get
		{
			return JoinedGamePanel.activeSelf;
		}
	}

	// Use this for initialization
	void Start()
	{
		Lobby = GetComponentInParent<LobbyController>();
		StartToJoinPanel = this.gameObject.transform.GetChild(0).gameObject;
		JoinedGamePanel = this.gameObject.transform.GetChild(1).gameObject;
	}

	// Update is called once per frame
	void Update()
	{
#if UNITY_EDITOR_WIN
		if (Input.GetKeyDown(PlayerInfo.JoystickButtonPrefix + 7))
#elif UNITY_EDITOR_OSX
		if (Input.GetKeyDown(PlayerInfo.JoystickButtonPrefix + 9))
#else
		if (false)
#endif
		{
			JoinGame();
		}

#if UNITY_EDITOR_WIN
		if (Input.GetKeyDown(PlayerInfo.JoystickButtonPrefix + 6))
#elif UNITY_EDITOR_OSX
		if (Input.GetKeyDown(PlayerInfo.JoystickButtonPrefix + 10))
#else
		if (false)
#endif
		{
			LeaveGame();
		}
	}

	void JoinGame()
	{
		if (HasJoinedGame) return;
		StartToJoinPanel.SetActive(false);
		JoinedGamePanel.SetActive(true);

		// Create the Cursor
		Cursor = Instantiate<CursorController>(Lobby.CursorPrefab, transform.position, transform.rotation);
		Cursor.transform.SetParent(Lobby.transform);
		Cursor.PlayerPanel = this;
		Cursor.CursorImage.color = CursorColor;
	}
	void LeaveGame()
	{
		if (!HasJoinedGame) return;
		StartToJoinPanel.SetActive(true);
		JoinedGamePanel.SetActive(false);

		// Destroy the Cursor
		Destroy(Cursor.gameObject);
	}
}