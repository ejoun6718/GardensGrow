﻿using UnityEngine;
using System.Collections;

public class RoomRotater : MonoBehaviour {

	public GameObject NorthRoom;
	public GameObject NorthEastRoom;
	public GameObject EastRoom;
	public GameObject SouthEastRoom;
	public GameObject SouthRoom;
	public GameObject SouthWestRoom;
	public GameObject WestRoom;
	public GameObject NorthWestRoom;

	GameObject tempRoom1;
	GameObject tempRoom2;

	private Vector3 moveRight = new Vector3(14, 0, 0);
	private Vector3 moveDown = new Vector3(0, -10, 0);
	private Vector3 moveLeft = new Vector3(-14, 0, 0);
	private Vector3 moveUp = new Vector3(0, 10, 0);

	//Algorithm is basically a rotate clockwise, each room should be given a new name
	public void Toggle () {
		tempRoom1 = NorthEastRoom;
		NorthEastRoom = NorthRoom;
		NorthEastRoom.transform.position += moveRight;

		tempRoom2 = tempRoom1;
		tempRoom1 = EastRoom;
		EastRoom = tempRoom2;
		EastRoom.transform.position += moveDown;

		tempRoom2 = tempRoom1;
		tempRoom1 = SouthEastRoom;
		SouthEastRoom = tempRoom2;
		SouthEastRoom.transform.position += moveDown;

		tempRoom2 = tempRoom1;
		tempRoom1 = SouthRoom;
		SouthRoom = tempRoom2;
		SouthRoom.transform.position += moveLeft;

		tempRoom2 = tempRoom1;
		tempRoom1 = SouthWestRoom;
		SouthWestRoom = tempRoom2;
		SouthWestRoom.transform.position += moveLeft;

		tempRoom2 = tempRoom1;
		tempRoom1 = WestRoom;
		WestRoom = tempRoom2;
		WestRoom.transform.position += moveUp;

		tempRoom2 = tempRoom1;
		tempRoom1 = NorthWestRoom;
		NorthWestRoom = tempRoom2;
		NorthWestRoom.transform.position += moveUp;

		tempRoom2 = tempRoom1;
		NorthRoom = tempRoom2;
		NorthRoom.transform.position += moveRight;
	}
}
