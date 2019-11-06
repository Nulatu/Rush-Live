using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}

		public int MovementRange = 50;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

	    private Vector3 m_StartPos;
        private bool m_UseX; // Toggle for using the x axis
        private bool m_UseY; // Toggle for using the Y axis
        private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
        private CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

        public void OnDrag(PointerEventData data)
		{
			Vector3 newPos = Vector3.zero;

            Vector2 direction = data.position - new Vector2(m_StartPos.x, m_StartPos.y);
            float distance = direction.magnitude;
            distance = Mathf.Clamp(distance, 0, MovementRange);
            newPos = direction.normalized * distance;

            Quaternion q = Quaternion.FromToRotation(-Vector3.right , direction);
            float angle = 360 - q.eulerAngles.z;

            if (distance >= MovementRange/2)
            {
                if(angle > 337 || angle < 22)
                    newPos = -Vector3.right * MovementRange;

                if (angle > 67 && angle < 112)
                    newPos = Vector3.up * MovementRange;

                if (angle > 157 && angle < 202)
                    newPos = Vector3.right * MovementRange;

                if (angle > 247 && angle < 292)
                    newPos = -Vector3.up * MovementRange;


                if (angle > 22 && angle < 67)
                    newPos = (-Vector3.right + Vector3.up) * MovementRange;

                if (angle > 112 && angle < 157)
                {
                    if (Time.timeScale <= 0.1f) // RoundSystem.In.CurrentRound == 1 // OneSecondTutorial // Better such a condition check than through Singolton. Faster.
                        Tutorial.In.FinishSecondStepTutorual();
                    newPos = (Vector3.right + Vector3.up) * MovementRange;
                }

                if (angle > 202 && angle < 247)
                    newPos = (Vector3.right - Vector3.up) * MovementRange;

                if (angle > 292 && angle < 337)               
                    newPos = (-Vector3.right - Vector3.up) * MovementRange;

                Move(newPos);
            }
          
            transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z); 

            UpdateVirtualAxes(transform.position);
		}

        public void OnPointerUp(PointerEventData data)
		{
            transform.position = m_StartPos;
			UpdateVirtualAxes(m_StartPos);
		}


		public void OnPointerDown(PointerEventData data) { }

        private void Start()
        {
            m_StartPos = transform.position;
        }

        private void Move(Vector3 direction)
        {
            RaycastHit hitInfo;
            Ray ray = new Ray(GameManager.In.PlayerObject.transform.position, new Vector3(direction.x, 0, direction.y));
            Physics.Raycast(ray, out hitInfo, 100, LayerMask.GetMask("Energy")); // current distation energy points ~ 5.6
            if (!hitInfo.transform) return; // We direct the cursor to a non-existent energy point
            GameManager.In.PlayerObject.MoveEnergyPoint(hitInfo.transform.GetComponent<EnergyPoint>(), hitInfo.distance);
        }

        private void UpdateVirtualAxes(Vector3 value)
        {
            var delta = m_StartPos - value;
            delta.y = -delta.y;
            delta /= MovementRange;
            if (m_UseX)
            {
                m_HorizontalVirtualAxis.Update(-delta.x);
            }

            if (m_UseY)
            {
                m_VerticalVirtualAxis.Update(delta.y);
            }
        }

        private void CreateVirtualAxes()
        {
            // set axes to use
            m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
            m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

            // create new axes based on axes to use
            if (m_UseX)
            {
                m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
                CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
            }
            if (m_UseY)
            {
                m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
                CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
            }
        }
    }
}