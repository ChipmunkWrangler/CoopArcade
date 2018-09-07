using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class Submode : MonoBehaviour { 
	abstract public void StartSubmode (MoveShipRotateAndThrust moveShip);
	abstract public void StopSubmode(MoveShipRotateAndThrust _moveShip);
}
