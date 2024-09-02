using UnityEngine;

namespace PLAYERTWO.PlatformerProject
{
	[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "PLAYER TWO/Platformer Project/Player/New Player Stats")]
	public class PlayerStats : EntityStats<PlayerStats>
	{
		[Header("General Stats")]
		public float pushForce = 4f;
		public float snapForce = 15f;
		public float slideForce = 10f;
		public float rotationSpeed = 970f;
		public float gravity = 38f;
		public float fallGravity = 65f;
		public float gravityTopSpeed = 50f;

		[Header("Pick'n Throw Stats")]
		public bool canPickUp = true;
		public bool canPickUpOnAir = false;
		public bool canJumpWhileHolding = true;
		public float pickDistance = 0.5f;
		public float throwVelocityMultiplier = 1.5f;

		[Header("Motion Stats")]
		public bool applySlopeFactor = true;
		public float acceleration = 13f;
		public float deceleration = 28f;
		public float friction = 28f;
		public float slopeFriction = 18f;
		public float topSpeed = 6f;
		public float turningDrag = 28f;
		public float airAcceleration = 32f;
		public float brakeThreshold = -0.8f;
		public float slopeUpwardForce = 25f;
		public float slopeDownwardForce = 28f;

		[Header("Running Stats")]
		public bool canRun = true;
		public bool canRunOnAir = true;
		public float runningAcceleration = 16f;
		public float runningTopSpeed = 7.5f;
		public float runningTurningDrag = 14f;

		[Header("Jump Stats")]
		public int multiJumps = 1;
		public float coyoteJumpThreshold = 0.15f;
		public float maxJumpHeight = 17f;
		public float minJumpHeight = 10f;

		[Header("Crouch Stats")]
		public float crouchHeight = 1f;
		public float crouchFriction = 10f;

		[Header("Crawling Stats")]
		public float crawlingAcceleration = 8f;
		public float crawlingFriction = 32f;
		public float crawlingTopSpeed = 2.5f;
		public float crawlingTurningSpeed = 3f;

		[Header("Wall Drag Stats")]
		public bool canWallDrag = true;
		public bool wallJumpLockMovement = true;
		public float minGroundDistanceToDrag = 0.5f;
		public float minWallAngleToDrag = 60;
		public LayerMask wallDragLayers;
		public Vector3 wallDragSkinOffset;
		public float wallDragGravity = 12f;
		public float wallJumpDistance = 8f;
		public float wallJumpHeight = 15f;

		[Header("Pole Climb Stats")]
		[Tooltip("If true, the Player can climb poles when touching them.")]
		public bool canPoleClimb = true;

		[Tooltip("The offset applied to the Player skin when climbing a pole.")]
		public Vector3 poleClimbSkinOffset;

		[Tooltip("The acceleration speed applied to the Player when climbing up.")]
		public float climbUpAcceleration = 6f;

		[Tooltip("The maximum speed the Player can reach when climbing up.")]
		public float climbUpTopSpeed = 3f;

		[Tooltip("The acceleration speed applied to the Player when climbing down.")]
		public float climbDownAcceleration = 20f;

		[Tooltip("The maximum speed the Player can reach when climbing down.")]
		public float climbDownTopSpeed = 8f;

		[Tooltip("The speed at which the Player decelerates when releasing the inputs.")]
		public float climbFriction = 15f;

		[Tooltip("The maximum speed the Player can reach when rotating around the pole.")]
		public float climbRotationTopSpeed = 2f;

		[Tooltip("The acceleration speed applied to the Player when rotating around the pole.")]
		public float climbRotationAcceleration = 5f;

		[Tooltip("The jump force applied away from the pole when jumping from it.")]
		public float poleJumpDistance = 8f;

		[Tooltip("The jump force applied upwards when jumping from the pole.")]
		public float poleJumpHeight = 15f;

		[Header("Swimming Stats")]
		[Tooltip("When entering the enter, the Player's vertical speed will be clamped to this value.")]
		public float waterMaxVerticalSpeedOnEnter = 20f;

		[Tooltip("When entering the water, the Player's velocity will be multiplied by this value.")]
		[Range(0, 1)]
		public float waterConversion = 0.35f;

		[Tooltip("The rotation speed of the Player, in degrees per second, when swimming.")]
		public float waterRotationSpeed = 360f;

		[Tooltip("An upward force applied to the Player when underwater.")]
		public float waterUpwardsForce = 8f;

		[Tooltip("The jump force applied to the Player when jumping out of the water.")]
		public float waterJumpHeight = 15f;

		[Tooltip("The drag speed applied to the Player when turning in water. More drag means quicker turning.")]
		public float waterTurningDrag = 2.5f;

		[Tooltip("The acceleration speed applied to the Player when swimming forward.")]
		public float swimAcceleration = 4f;

		[Tooltip("The maximum speed of the Player when swimming forward.")]
		public float swimTopSpeed = 4f;

		[Tooltip("The acceleration speed applied to the Player when swimming upwards.")]
		public float swimUpwardsAcceleration = 6f;

		[Tooltip("The maximum speed of the Player when swimming upwards.")]
		public float swimUpwardsTopSpeed = 10f;

		[Tooltip("The acceleration speed applied to the Player when swimming downwards.")]
		public float swimDownwardsAcceleration = 6f;

		[Tooltip("The maximum speed of the Player when swimming downwards.")]
		public float swimDownwardsTopSpeed = 10f;

		[Tooltip("The deceleration speed applied to the Player when it's not swimming.")]
		public float swimDeceleration = 3f;

		[Header("Spin Stats")]
		public bool canSpin = true;
		public bool canAirSpin = true;
		public float spinDuration = 0.5f;
		public float airSpinUpwardForce = 10f;
		public int allowedAirSpins = 1;

		[Header("Hurt Stats")]
		public float hurtUpwardForce = 10f;
		public float hurtBackwardsForce = 5f;

		[Header("Air Dive Stats")]
		public bool canAirDive = true;
		public bool applyDiveSlopeFactor = true;
		public float airDiveForwardForce = 16f;
		public float airDiveFriction = 32f;
		public float airDiveSlopeFriction = 12f;
		public float airDiveSlopeUpwardForce = 35f;
		public float airDiveSlopeDownwardForce = 40f;
		public float airDiveGroundLeapHeight = 10f;
		public float airDiveRotationSpeed = 45f;

		[Header("Stomp Attack Stats")]
		public bool canStompAttack = true;
		public float stompDownwardForce = 20f;
		public float stompAirTime = 0.8f;
		public float stompGroundTime = 0.5f;
		public float stompGroundLeapHeight = 10f;

		[Header("Ledge Hanging Stats")]
		[Tooltip("If true, the Player can hang from ledges when touching them.")]
		public bool canLedgeHang = true;

		[Tooltip("The layers of the colliders that the Player can hang from.")]
		public LayerMask ledgeHangingLayers;

		[Tooltip("The offset applied to the Player skin when hanging from a ledge.")]
		public Vector3 ledgeHangingSkinOffset;

		[Tooltip("The maximum forward distance the ledge can be from the Player to be detected.")]
		public float ledgeMaxForwardDistance = 0.1f;

		[Tooltip("The maximum downward distance the ledge can be from the Player to be detected.")]
		public float ledgeMaxDownwardDistance = 0.25f;

		[Tooltip("The maximum distance from the Player's side to detected walls from a ledge.")]
		public float ledgeSideMaxDistance = 0.5f;

		[Tooltip("An offset applied vertically to the Player's ledge wall detection.")]
		public float ledgeSideHeightOffset = 0.15f;

		[Tooltip("The radius of the sphere casted to detect walls from a ledge.")]
		public float ledgeSideCollisionRadius = 0.25f;

		[Tooltip("The acceleration speed applied to the Player when moving on a ledge.")]
		public float ledgeMovementAcceleration = 3f;

		[Tooltip("The maximum speed the Player can reach when moving on a ledge.")]
		public float ledgeMovementTopSpeed = 1.5f;

		[Tooltip("The deceleration speed applied to the Player when releasing the inputs on a ledge.")]
		public float ledgeMovementFriction = 10f;

		[Header("Ledge Climbing Stats")]
		[Tooltip("If true, the Player can climb ledges when hanging from them.")]
		public bool canClimbLedges = true;

		[Tooltip("The layers of the colliders that the Player can climb.")]
		public LayerMask ledgeClimbingLayers;

		[Tooltip("The offset applied to the Player skin when climbing a ledge.")]
		public Vector3 ledgeClimbingSkinOffset;

		[Tooltip("The duration it takes for the Player to climb a ledge. Try syncing this with the animation speed.")]
		public float ledgeClimbingDuration = 1f;

		[Header("Backflip Stats")]
		public bool canBackflip = true;
		public bool backflipLockMovement = true;
		public float backflipAirAcceleration = 12f;
		public float backflipTurningDrag = 2.5f;
		public float backflipTopSpeed = 7.5f;
		public float backflipJumpHeight = 23f;
		public float backflipGravity = 35f;
		public float backflipBackwardForce = 4f;
		public float backflipBackwardTurnForce = 8f;

		[Header("Gliding Stats")]
		[Tooltip("If true, the Player can enter the glide state.")]
		public bool canGlide = true;

		[Tooltip("An downwards force applied to the Player when gliding.")]
		public float glidingGravity = 10f;

		[Tooltip("The maximum downwards speed the Player can reach when gliding.")]
		public float glidingMaxFallSpeed = 2f;

		[Tooltip("The drag speed applied to the Player when turning while gliding. Greater values mean quicker turning.")]
		public float glidingTurningDrag = 8f;

		[Tooltip("The rotation speed of the Player, in degrees per second, when gliding.")]
		public float glidingRotationSpeed = 180f;

		[Header("Dash Stats")]
		public bool canAirDash = true;
		public bool canGroundDash = true;
		public float dashForce = 25f;
		public float dashDuration = 0.3f;
		public float groundDashCoolDown = 0.5f;
		public float allowedAirDashes = 1;

		[Header("Rail Grinding Stats")]
		public bool useCustomCollision = true;
		public float grindRadiusOffset = 0.26f;
		public float minGrindInitialSpeed = 10f;
		public float minGrindSpeed = 5f;
		public float grindTopSpeed = 25f;
		public float grindDownSlopeForce = 40f;
		public float grindUpSlopeForce = 30f;

		[Header("Rail Grinding Brake")]
		public bool canGrindBrake = true;
		public float grindBrakeDeceleration = 10;

		[Header("Rail Grinding Dash Stats")]
		public bool canGrindDash = true;
		public bool applyGrindingSlopeFactor = true;
		public float grindDashCoolDown = 0.5f;
		public float grindDashForce = 25f;
	}
}
