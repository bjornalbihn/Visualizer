using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class VisualizerData 
{
	public int ID {get {return m_ID;}}
	public string Name {get {return m_name;}}
	public string Author {get {return m_author;}}
	public float Price {get {return m_price;}}
	public Sprite Icon {get {return m_icon;}}

	[SerializeField] private int m_ID;
	[SerializeField] private string m_name;
	[SerializeField] private Sprite m_icon;
	[SerializeField] private string m_author;
	[SerializeField] private float m_price;
}
