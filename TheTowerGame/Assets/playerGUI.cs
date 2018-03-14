using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerGUI : MonoBehaviour {

	// Use this for initialization

	public GameObject player;
	public Image healthbar;
	public Text healthnumber;
	public Image energybar;
	public Text energynumber;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.fillAmount = player.GetComponent<character_controller> ().health / 100f;
		healthnumber.text = player.GetComponent<character_controller> ().health.ToString();
		energybar.fillAmount = player.GetComponent<character_controller> ().energy / 100f;
		energynumber.text = player.GetComponent<character_controller> ().energy.ToString();

	}
}
