#pragma warning disable CS0649

using UnityEngine;

namespace Gameplay.Helpers.Pool
{
	public class PooledObject : MonoBehaviour
	{
		[SerializeField]
		private string _type;

		public string Type => _type;
	}
}
