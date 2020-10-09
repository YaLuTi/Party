using UnityEngine;
using System.Collections;

namespace AnimFollow
{
	public class BulletHitInfo_AF
	{
		public Transform hitTransform;
        public bool IsShot = false;
		public Vector3 hitPoint = Vector3.zero;
		public Vector3 bulletForce = Vector3.zero;
		public Vector3 hitNormal = Vector3.zero;
        
	}
}
