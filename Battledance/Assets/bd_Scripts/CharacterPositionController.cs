using UnityEngine;
using System.Collections;

public class CharacterPositionController : MonoBehaviour {

	SpriteRenderer mySpriteRenderer;

	bool fadingOut;
	bool fadingIn;
	bool fading;

	// TODO: use enums instead?
	const int ANIM_STATE_FADING_OUT = 1;
	const int ANIM_STATE_FADING_IN = 2;

	// TODO: ACTION QUEUE
	//  pop and push?

	int animationState;


	/*
	 * First, fade the player out. Then, fade them in at the new position.
	 **/
	void TeleportPlayerToThisPosition(){
		FadeOut ();
	}

	/* 
	 * Nicely translate the player to a new position
	 **/
	//void MovePlayerToThisPosition

	/*
	 * Fade self out
	 **/
	public void FadeOut () {
		fading = true;
		fadingOut = true;
	}

	/*
	 * Fade self in
	 **/
	public void FadeIn () {
		fading = true;
		fadingIn = true;
	}

	void FixedUpdate() {

		// check state
		// based on state, perform a step

		if (fading == true && fadingOut == true && mySpriteRenderer.color.a > 0) {
			FadeOutStep();
		}
		if (fading == true && fadingIn == true && mySpriteRenderer.color.a < 1) {
			FadeInStep();
		}		
	}

	// Use this for initialization
	void Start () {
		mySpriteRenderer = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FadeOutStep() { //TODO change this to be handled properly with timesteps
		mySpriteRenderer.color = new Color(1f, 1f, 1f, mySpriteRenderer.color.a - Constants.FADE_SPEED);
	}

	void FadeInStep() { //TODO change this to be handled properly with timesteps
		mySpriteRenderer.color = new Color(1f, 1f, 1f, mySpriteRenderer.color.a + Constants.FADE_SPEED);
	}


}

// CLIMB PLAYER CODE FOR REFERENCE

//package org.aqualuft.climb.active
//{
//	import org.aqualuft.climb.object.SpringPlatform;
//	import org.aqualuft.climb.object.SpringSidePlatform;
//	import org.flixel.FlxSprite;
//	import org.flixel.FlxObject;
//	import org.flixel.plugin.photonstorm.FlxControl;
//	import org.flixel.plugin.photonstorm.FlxControlHandler;
//	import org.aqualuft.climb.Globals;
//	import org.flixel.FlxG;
//	import org.flixel.plugin.photonstorm.FlxDelay;
//	import org.aqualuft.climb.audio.Audio;
//	
//	/**
//	 * Represents the playable main character sprite, as well all of its
//	 * physics and control rules
//	 * 
//	 * 6/27/2012: Added sidesidesprings
//	 * 6/27/2012: Added _obstaclesContacted list
//	 * 7/01/2012: Spring boosts now rely on height rather than timers.
//	 */
//public class Player extends FlxSprite 
//{
//	// CONSTANTS
//	
//	// Significant player states related to motion
//	public static const IDLE:uint = 0;
//	public static const RUNNING:uint = 1;
//	public static const PENDULUM:uint = 2;
//	public static const JUMPING:uint = 3;
//	public static const FALLING:uint = 4;
//	public static const DYING:uint = 5;
//	public static const FLYING:uint = 6;
//	public static const LADDER:uint = 7;
//	public static const LANDING:uint = 8;
//	public static const SPRING:uint = 9;
//	//public static const SIDESPRING:uint = 10;
//	public static const BUTT_STOMP:uint = 11;
//	public static const MIDAIR_HANG:uint = 12;
//	public static const RIDING_UP:uint = 13;
//	
//	protected var _buttStompCooldown:FlxDelay = new FlxDelay(200);
//	protected var _buttStompHangTimer:FlxDelay = new FlxDelay(300);
//	
//	
//	//protected var _glowColor1:uint = 0xff0000;
//	//protected var _glowColor2:uint = 0xff0000;
//	protected var red:uint = 0xff0000;
//	protected var yellow:uint = 0xffff00;
//	protected var orange:uint = 0xff8800;
//	protected var green:uint = 0x00ff00;
//	protected var blue:uint = 0x0000ff;
//	protected var colorChange:uint = 0;
//	
//	
//	protected var _noGlowColor:uint = 16777215;
//	
//	
//	// Graphics
//	[Embed(source="../../../../../resources/sprites/kingsheet_shaded-fs8.png")]
//	public static const Sprite:Class;
//	
//	// Dimensions
//	protected static const GRAPHIC_WIDTH:Number = 64;
//	protected static const GRAPHIC_HEIGHT:Number = 64;
//	
//	protected static const WIDTH:uint = 24;
//	protected static const HEIGHT:uint = 64;
//	
//	protected var hangFrame:uint = 11;
//	protected var crouchFrame:uint = 0;
//	protected var upFrame:uint = 1;
//	protected var hang2Frame:uint = 2;
//	protected var downFrame:uint = 3;
//	
//	// Movement rules
//	private static const XACCEL:Number = 40; //was 40
//	private static const YACCEL:Number = 0;
//	private static const XVEL_MAX:Number = 240;
//	private static const YVEL_MAX:Number = 490;
//	private static const XDECEL:Number = 20;
//	private static const YDECEL:Number = 0;
//	//animation velocity benchmk
//	protected static const JUMP_HANG_VELOCITY:Number = 200;
//	private static const SPRING_VELOCITY:Number = -600;
//	private static const SPRING_BOOST_TIME:uint = 900;
//	
//	private static const SPRING_SIDE_VELOCITY:Number = -600;
//	private static const SPRING_SIDE_BOOST_TIME:uint =  700;
//	
//	//private static const FLYING_ACCELERATION:Number = -50;
//	private static const FLYING_ACCELERATION:Number = -400;
//	private static const FLYING_VELOCITY:Number = -800;
//	private static const LANDING_TIME:uint = 140;
//	private static const RISING_SPEED:Number = 200;
//	
//	
//	// VARIABLES
//	protected var _state:uint; 
//	protected var _killed:Boolean = false;
//	protected var _flying:Boolean = false;
//	
//	public var _springHit:Boolean = false;
//	public var _springReact:Boolean = false;
//	public var _springTimer:FlxDelay = new FlxDelay(SPRING_BOOST_TIME);
//	public var _springBoostHeight:uint;
//	public var _springOriginalPosition:uint;
//	
//	public var _springSideHit:Boolean = false;
//	public var _springSideReact:Boolean = false;
//	public var _springSidePushLeft:Boolean = false;
//	public var _springSideTimer:FlxDelay = new FlxDelay(SPRING_SIDE_BOOST_TIME);
//	public var _springSideBoostLength:uint;
//	
//	public var bossBounce:Boolean = false;
//	public var bossBounceLeft:Boolean = false;
//	
//	public var bossBumpUp:Boolean = false;
//	public var bossBumpUpHeight:Number = 175; // between 2*75 and 3*75
//	public var bossBumpUpPositionY:Number;
//	
//	protected var _landed:Boolean = false;
//	protected var _pushDirection:int = NONE;
//	protected var _pushed:Boolean = false;
//	
//	protected var _speed:Number = 0;
//	
//	protected var _obstaclesLanded:Vector.<FlxObject> = 
//		new Vector.<FlxObject>();
//	
//	protected var _obstaclesContacted:Vector.<FlxObject> = 
//		new Vector.<FlxObject>();
//	
//	
//	// for jumping mechanics
//	protected var _jumpTime:Number = 0; //tracks time of player jump
//	protected var _secondJumpTime:Number = 0; //tracks time of player jump
//	protected var _allowNewJump:Boolean = true; //whether player is allowed to jump again
//	protected var _allowNewStomp:Boolean = true; //whether player is allowed to stomp again
//	
//	// for jumping graphics
//	protected var _crouchTimer:FlxDelay = new FlxDelay(LANDING_TIME);
//	
//	
//	/**
//		 * Create new instance of player
//		 * @param	X	starting X position of Player
//		 * @param	Y	starting Y position of Player
//		 * 
//		 * 6/9/2012 Added animations, changed player sprite width to the "forgiving"
//		 * width which accomodates the space between his feet
//		 */
//	public function Player(X:Number, Y:Number) 
//	{
//		super(X, Y);
//		
//		this.setAppearance();
//		
//		this._buttStompCooldown.start();
//		
//		// Initialize control plugin
//		if(FlxG.getPlugin(FlxControl) == null)
//			FlxG.addPlugin(new FlxControl);
//	}
//	
//	public function setAppearance():void
//	{
//		// Load graphics for Player
//		this.loadGraphic(Sprite, true, true, GRAPHIC_WIDTH, GRAPHIC_HEIGHT);
//		
//		// Add animations for king
//		addAnimation("jump", [0, 1, 2, 3, 4], 5, false); //not used, for ref
//		// addAnimation("walk", [5, 6, 7, 8, 9, 10], 7, true);
//		addAnimation("run", [5, 6, 7, 8, 9, 10], 13, true);
//		addAnimation("dying", [11, 12, 13, 14, 15], 10, true); 
//		addAnimation("idle", [16, 17, 18, 19, 20, 21, 22], 5, true);  //removed frames 23 and 24
//		addAnimation("flying", [23, 24, 25, 26, 27, 28], 10, true); //removed frames 29 and 30, shifting right 2
//		addAnimation("landing", [4], 0, false);
//		addAnimation("falling", [3], 0, true);
//		
//		this.width = WIDTH;      // this makes the width of the player
//		this.centerOffsets(); // line up with his feet (precision landing)
//		
//	}
//	
//	override public function update():void
//	{
//		//trace("@@ FlxG.Elapsed " + FlxG.elapsed);
//		
//		super.update();
//		readInput();
//		updateDynamics();
//		analyzeTouched();
//		updateState();
//		
//		switch (_state) {
//		case IDLE:
//			updateIdle();
//			break;
//		case BUTT_STOMP:
//			updateButtStomp();
//			break;
//		case MIDAIR_HANG:
//			updateMidairHang();
//			break;
//		case RUNNING:
//			updateRunning();
//			break;
//		case PENDULUM:
//			updatePendulum();
//			break;
//		case JUMPING:
//			updateJumping();
//			break;
//		case FALLING:
//			updateFalling();
//			break;
//		case DYING:
//			updateDying();
//			break;
//		case FLYING:
//			updateFlying();
//			break;
//		case LADDER:
//			updateLadder();
//			break;
//		case LANDING:
//			updateLanding();
//			break;
//		case SPRING:
//			updateSpring();
//			break;
//		case RIDING_UP:
//			updateRiding();
//			//case SIDESPRING:
//			//updateSideSpring();
//			//break;
//		default:
//			// trace ("@@ Player is occupying invalid state");
//		}
//	}
//	
//	/**
//		 * Convert keystrokes to meaningful variables
//		 */
//	protected function readInput():void 
//	{
//		// Allow a new jump if the controller released the space bar.
//		if ( (FlxG.keys.justReleased("SPACE") || FlxG.keys.justReleased("UP") || FlxG.keys.justReleased("W")) )
//		{	_allowNewJump = true; /*_allowNewStomp = true;*/}
//		
//		// Allow a new stomp if the controller released the down or shift key.
//		//if ( (FlxG.keys.justReleased("SHIFT") /*|| FlxG.keys.justReleased("DOWN")*/ ) )
//		//	_allowNewStomp = true;
//		
//		
//		// If both left and right are requested, go neither
//		if (  ((FlxG.keys.pressed("LEFT") == true) || (FlxG.keys.pressed("A"))) &&
//		    ((FlxG.keys.pressed("RIGHT") == true) || (FlxG.keys.pressed("D"))) )
//		{
//			_pushed = false;
//			_pushDirection = NONE;
//		}
//		else if ( (FlxG.keys.pressed("LEFT") == true) || (FlxG.keys.pressed("A")) )
//		{
//			_pushed = true;
//			_pushDirection = LEFT;
//		}
//		else if ( (FlxG.keys.pressed("RIGHT") == true) || (FlxG.keys.pressed("D")) )
//		{
//			_pushed = true;
//			_pushDirection = RIGHT;
//		}
//		else
//		{
//			_pushed = false;
//			_pushDirection = NONE;
//		}
//	}
//	
//	/**
//		 * Update the motion rules based on control, gravity, and other factors.
//		 * These are generic rules that can be superseded by specific
//		 * update states
//		 */
//	public function updateDynamics():void
//	{
//		var currentDirection:int;
//		var nextDirection:int;
//		
//		// Accelerate the Player if it is being guided by the control
//		if (_pushed == true)
//		{
//			_speed += XACCEL;
//			//trace("@@ _speed is " + _speed );
//			// Put cap on maximum speed
//			if (_speed > XVEL_MAX)
//				_speed = XVEL_MAX;
//		}
//		// Otherwise, player decelerates (friction)
//		else
//		{
//			_speed -= XDECEL;
//			// Player cannot slow to below zero
//			if (_speed < 0)
//				_speed = 0;
//		}
//		
//		// Compute current direction
//		if (this.velocity.x > 0)
//			currentDirection = RIGHT;
//		else if (this.velocity.x < 0)
//			currentDirection = LEFT;
//		else
//			currentDirection = NONE;
//		
//		// Determine what the nexte direction should be. If pushed by
//		// control, travel in direction of push
//		if (_pushed == true)
//			nextDirection = _pushDirection;
//		else
//			nextDirection = currentDirection;
//		
//		// Use direction to translate speed to velocity
//		if (nextDirection == LEFT)
//		{
//			this.velocity.x = -_speed;
//			this.facing = LEFT;
//		}
//		else if (nextDirection == RIGHT)
//		{
//			this.velocity.x = _speed;
//			this.facing = RIGHT;
//		}
//		else
//			this.velocity.x = 0;
//		
//		// Player always attempt to accelerate downward from gravity
//		this.acceleration.y = Globals.GRAVITY;
//	}
//	
//	/**
//		 * Scan the obstacles that the player is currently touching, making
//		 * decisions about internal values depending on what obstacles
//		 * are being touched
//		 */
//	private function analyzeTouched():void 
//	{
//		var obstacle:FlxObject;
//		
//		// Set default values for obstacle-releated parameters
//		_springHit = false;
//		_springSideHit = false;
//		
//		
//		// Scan throuch each obstacle that has been contacted
//		for each (obstacle in _obstaclesContacted)
//		{			
//			if (obstacle is SpringSidePlatform)	
//			{	
//				_springSideHit = true;
//				if ( SpringSidePlatform(obstacle).springPointsLeft )
//				{
//					_springSidePushLeft = true;
//				}
//				else { _springSidePushLeft = false; }
//			}
//		}
//		
//		// Scan throuch each obstacle that has been landed
//		for each (obstacle in _obstaclesLanded)
//		{
//			if (obstacle is SpringPlatform)
//			{
//				_springBoostHeight = SpringPlatform(obstacle)._springBoostHeight;
//				_springOriginalPosition = SpringPlatform(obstacle).y;
//				_springHit = true;
//			}
//		}
//		
//		if (bossBounce == true)
//		{
//			_springSideHit = true;
//			if (bossBounceLeft == true)
//				_springSidePushLeft = true;
//			else
//				_springSidePushLeft = false;
//			bossBounce = false;
//			
//		}
//		
//		if ( bossBumpUp == true && this.isTouching(FLOOR) && (bossBumpUpPositionY-this.y < 70) ) //TODO maybe make less jankey
//		{
//			_springBoostHeight = bossBumpUpHeight;
//			_springOriginalPosition = this.y;
//			_springHit = true;
//			
//			bossBumpUp = false;
//		}
//		else if ( bossBumpUp == true && !(this.isTouching(FLOOR)))
//		{
//			bossBumpUp = false;
//		}
//		
//	}
//	
//	/**
//		 * Kill the player
//		 */
//	public function tryKill():void
//	{
//		if (this.state != Player.FLYING) //player invincible when flying
//		{
//			this.kill();
//		}
//	}
//	
//	override public function kill():void
//	{
//		if (!killed)
//		{
//			switch(Globals._currentLevel)
//			{
//			case 1:
//				Globals._level1Deaths++;
//				break;
//			case 2:
//				Globals._level2Deaths++;
//				break;
//			case 3:
//				Globals._level3Deaths++;
//				break;
//			case 4:
//				Globals._level4Deaths++;
//				break;
//			default:
//				break;
//			}
//		}
//		_killed = true;
//	}
//	
//	/**
//		 * Reset player by reviving at a given position
//		 * @param	X	x-position at which Player will be regenerated
//		 * @param	Y	y-position at which Player will be regenerated
//		 */
//	override public function reset(X:Number, Y:Number):void
//	{
//		// Player can move again
//		this.immovable = false;
//		
//		// Player is no longer see-through
//		this.alpha = 1;
//		
//		// Reset player's state
//		resetState();
//		
//		// Typical reset reviving, repositioning, etc.
//		super.reset(X, Y);			
//	}
//	
//	/**
//		 * Reset all state-related variables for the Player. Good if restoring
//		 * level from another checkpoint
//		 */
//	public function resetState():void
//	{
//		// Set state variable to something innocuous
//		_state = IDLE;
//		
//		// Clear the values of a bunch of state-keeping variables
//		_killed = false;
//		_flying= false;
//		_springHit = false;
//		_springSideHit = false;
//		_springReact = false;
//		_springSideReact = false;
//		_landed = false;
//		_pushDirection = NONE;
//		_pushed = false;
//		_speed = 0;
//		_obstaclesLanded = new Vector.<FlxObject>();
//		_obstaclesContacted = new Vector.<FlxObject>();			
//		_jumpTime = 0;
//		_allowNewJump = true;
//		_allowNewStomp = false;
//		_crouchTimer.abort();
//		_buttStompCooldown.start();
//	}
//	
//	/**
//		 * End the players reaction to spring
//		 */
//	private function endSpringReact():void
//	{
//		_springReact = false;
//	}
//	
//	/**
//		 * End the players reaction to side spring
//		 */
//	private function endSpringSideReact():void
//	{
//		_springSideReact = false;
//	}
//	
//	/**
//		 * Get the pixel X-position of the right side of this sprite
//		 */
//	public function get right():Number
//	{
//		var rightX:Number;
//		
//		rightX = this.x + this.width;
//		
//		return rightX;
//	}
//	
//	/**
//		 * Set the x-position of the Player sprite based on a desired
//		 * position for the 'right' side of the player
//		 */
//	public function set right(newRight:Number):void
//	{
//		var newX:Number;
//		
//		newX = newRight - this.width;
//		
//		this.x = newX;
//	}
//	
//	/**
//		 * Simple setter method to translate desired 'left' side of player
//		 * to new x-position for the player
//		 */
//	public function set left(newLeft:Number):void
//	{
//		this.x = newLeft;
//	}
//	
//	/**
//		 * Get the pixel X-position of the left side of this sprite
//		 */
//	public function get left():Number
//	{
//		var leftX:Number;
//		
//		leftX = this.x;
//		
//		return leftX;
//	}
//	
//	/**
//		 * Get the pixel location of the bottom of this sprite
//		 * @return	pixel location of bottom of sprite
//		 */
//	public function get bottom():Number
//	{
//		var bottomY:Number;
//		
//		bottomY = this.y + this.height;
//		
//		return bottomY;
//	}
//	
//	/**
//		 * Find the last value for 'bottom' for the Player
//		 */
//	public function get lastBottom():Number 
//	{
//		var lastBottomY:Number;
//		
//		lastBottomY = this.last.y + this.height;
//		
//		return lastBottomY;
//	}
//	
//	/**
//		 * Find the last value for 'top' (y) for the Player
//		 */
//	public function get lastY():Number
//	{
//		var lastY:Number;
//		
//		lastY = this.last.y;
//		
//		return lastY;
//	}
//	
//	/**
//		 * Find player's state
//		 */
//	public function get state():uint
//	{
//		var state:uint;
//		
//		state = this._state + 0;
//		
//		return this._state;
//	}
//	
//	/**
//		 * Set the list of obstacles which the Player is currently landed on
//		 */
//	public function set obstaclesLanded(value:Vector.<FlxObject>):void
//	{
//		_obstaclesLanded = value;
//	}
//	
//	/**
//		 * Set the list of obstacles which the Player is currently contacting
//		 */
//	public function set obstaclesContacted(value:Vector.<FlxObject>):void
//	{
//		_obstaclesContacted = value;
//	}
//	
//	/**
//		 * Set whether the player should be flying. Accessed by CheckpointLogic
//		 */
//	public function set flying(value:Boolean):void 
//	{
//		_flying = value;
//	}
//	
//	/**
//		 * Set whether the player has just landed on a platform.
//		 */
//	public function set landed(value:Boolean):void 
//	{
//		_landed = value;
//	}
//	
//	/**
//		 * Find out whether the player has been killed
//		 */
//	public function get killed():Boolean 
//	{
//		return _killed;
//	}
//	
//	/**
//		 * Update the current state of the Player (state logic), mostly this is
//		 * just based around controls and motion, e.g. detecting a fall, or a
//		 * transition to the running state, etc.
//		 */
//	protected function updateState():void
//	{	
//		
//		//if (this.isTouching(FLOOR) == true)
//		//	_allowNewStomp = false;
//		
//		//trace("@@ this.velocity.y = " + this.velocity.y );
//		//trace("@@ _allowNewStomp = " + _allowNewStomp);
//		//trace("@@ FlxG.keys.space = " + FlxG.keys.SPACE);
//		
//		var oldState:uint = _state;
//		// Determine next state
//		// Note: priority order of the following state transitions is
//		// very important!
//		if (_killed == true)
//			_state = DYING;
//		else if (_flying == true)
//			_state = FLYING;
//		else if (_springHit == true || _springReact == true || _springSideHit == true || _springSideReact == true)
//			_state = SPRING; 			// Landing: don't re-land if you 'landed' by running sideways
//		else if ( (this.isTouching(FLOOR) != true)
//		         && ( (_state == BUTT_STOMP) || (_state == MIDAIR_HANG && _buttStompHangTimer.isRunning == false) ) )
//			_state = BUTT_STOMP;
//		//else if ( (FlxG.keys.SPACE) && (_allowNewStomp) )
//		//{
//		//	_state = MIDAIR_HANG;
//		//}				
//		//else if ( (this.isTouching(FLOOR) != true) 
//		//        && ( (_state == MIDAIR_HANG) || ((_state == JUMPING || _state == FALLING) 
//		//		&& ( /*(FlxG.keys.DOWN) ||*/ (FlxG.keys.SHIFT)) /*&& _allowNewStomp (_buttStompCooldown.isRunning != true)*/ ) ) )
//		//	_state = MIDAIR_HANG;
//		//else if ( this.isTouching(FLOOR) && _state == BUTT_STOMP )
//		//{
//		//trace("@@ I'm inside the bounce conditional");
//		//_allowNewJump = true;
//		//_allowNewStomp = true;
//		//	_state = JUMPING;
//		//}
//		else if ( oldState != LANDING && ((oldState == FALLING) && (this.isTouching(FLOOR))) /*&& _state != RUNNING*/)
//			_state = LANDING;
//		else if ( ((FlxG.keys.SPACE || FlxG.keys.UP || FlxG.keys.pressed("W")) && (_allowNewJump)) || this.velocity.y < 0)
//			_state = JUMPING;
//		else if (this.isTouching(FLOOR) && Math.abs(this.velocity.x) != 0)
//			_state = RUNNING;
//		else if (this.isTouching(FLOOR) == true)
//			_state = IDLE;
//		else if ( (this.velocity.y > 0) && (FlxG.keys.SPACE || FlxG.keys.UP || FlxG.keys.pressed("W")) && (_allowNewStomp == true) )
//			_state = BUTT_STOMP;
//		else if (this.velocity.y > 0)
//			_state = FALLING;
//		
//		//if (_state != oldState)
//		//	trace("@@ oldState was:  " + oldState + "    _state is:  " + _state + "   _landed is:  " +_landed);
//	}
//	
//	/**
//		 * Special update procedures for IDLE
//		 */
//	private function updateIdle():void
//	{
//		// If crouch time hasn't expired, play landing animation
//		if (_crouchTimer.hasExpired == false)
//			this.play("landing");
//		// Otherwise, play idle animation
//		else
//			this.play("idle");
//	}
//	
//	private function updateMidairHang():void
//	{
//		this.frame = this.hangFrame;
//		this.velocity.y = 0;
//		this.acceleration.y = 0;
//		this.velocity.x = this.velocity.x/8;
//		
//		if (_buttStompCooldown.isRunning != true)
//		{
//			_allowNewStomp = false;
//			_buttStompCooldown.start();
//		}
//		
//		if (_buttStompHangTimer.isRunning != true)
//		{
//			_buttStompHangTimer.start();
//			//_buttStompHangTimer.callback = buttStompFall;
//			// when the timer stops, update state will move to BUTT_STOMP state
//		}
//		
//		
//	}
//	
//	
//	/**
//		 * Special update procedures for BUTT_STOMP
//		 */
//	private function updateButtStomp():void
//	{
//		//this.velocity.x = this.velocity.x/2;
//		this.velocity.y = 700;
//		this.frame = this.hangFrame;
//	}
//	
//	//private function buttStompFall():void
//	//{
//	//	this.velocity.x = 0;
//	//	this.velocity.y = 500;
//	//}
//	
//	/**
//		 * Special update conditions if JUMPING
//		 */
//	protected function updateJumping():void
//	{
//		// 0.4 seconds is 200 pixels of jump
//		// 0 seconds is almost 100 pixels of jump
//		
//		const MAX_JUMP_TIME:Number = 0.43; //was 0.40 //was 0.46
//		const MIN_JUMP_TIME:Number = 0.119; // was 0.115
//		
//		const START_JUMP_TIME:Number = 0.065;
//		const START_JUMP_YVEL:Number = -400;
//		const YACCEL:Number = 100;
//		
//		// ANIMATION
//		// TODO: move midair animations somewhere else
//		// crouching before jump
//		if (_jumpTime > 0 && _jumpTime < 0.2)
//			this.frame = this.crouchFrame;
//		//moving up thru air
//		else if (this.velocity.y < -JUMP_HANG_VELOCITY)
//			this.frame = this.upFrame; 
//		//hanging in midair
//		else if (this.velocity.y >= -JUMP_HANG_VELOCITY && 
//		         this.velocity.y <= JUMP_HANG_VELOCITY)
//			this.frame = this.hang2Frame;
//		//falling
//		else if (this.velocity.y > JUMP_HANG_VELOCITY)
//			this.frame = this.downFrame; 
//		
//		// MOVEMENT
//		
//		// At start of jump, set jump timer to 0
//		if (this.isTouching(FlxObject.FLOOR))
//			_jumpTime = 0;
//		
//		//trace("@@ Jumping is updating. _allowNewJump = " + _allowNewJump + "    _jumptime = " + _jumpTime + "     MIN_JUMP_TIME = " + MIN_JUMP_TIME);
//		// this provides the functionality of the short hop, you can't stop
//		// a jump until .13s passes
//		if ((_jumpTime >= 0) && ( (FlxG.keys.SPACE || FlxG.keys.UP || FlxG.keys.pressed("W")) || _jumpTime < MIN_JUMP_TIME) &&
//		    _allowNewJump == true)
//		{
//			//trace("@@ I'm hopping");
//			_jumpTime += FlxG.elapsed;
//			if (_jumpTime > MAX_JUMP_TIME) {
//				_jumpTime = -1;
//				_allowNewJump = false;
//			}
//		}
//		// this provides functionality of a long jump, you can continue a 
//		// jump for .4s
//		else if((_jumpTime > 0) && (FlxG.keys.SPACE || FlxG.keys.UP || FlxG.keys.pressed("W")) && _allowNewJump)
//		{ 
//			_jumpTime += FlxG.elapsed;
//			if (_jumpTime > MAX_JUMP_TIME) {
//				_jumpTime = -1; //You can't jump for more than 0.4 seconds
//				_allowNewJump = false;
//			}
//		}
//		else _jumpTime = -1;
//		
//		if (_jumpTime > 0)
//		{
//			if(_jumpTime < START_JUMP_TIME)
//				velocity.y = START_JUMP_YVEL; //This is the minimum speed of the jump
//			else
//				acceleration.y = YACCEL; //The general acceleration of the jump
//		}
//		
//		//if (_jumpTime == -1)
//		//{
//		//	FlxG.keys.SPACE = false; //to boot you out of jump state
//		//	FlxG.keys.UP = false;
//		//}
//		
//	}
//	
//	/**
//		 * Special update conditions for FALLING
//		 */
//	private function updateFalling():void
//	{
//		this.play("falling");
//		//this.color = this._noGlowColor;
//	}
//	
//	/**
//		 * Special update conditions for RUNNING
//		 */
//	private function updateRunning():void
//	{
//		const RUN_SPEED:Number = 1;
//		
//		if (Math.abs(this.velocity.x) > RUN_SPEED)
//			this.play("run");
//		else
//			this.play("idle");
//		
//		//this._jumpTime = 0; //make sure you're able to jump
//		//this._allowNewJump = true; //fixes ceiling jump cancel bug
//		this._allowNewStomp = false;
//	}
//	
//	/**
//		 * Special update conditions for DYING
//		 */
//	private function updateDying():void
//	{
//		const FADE_AMOUNT:Number = .01;
//		
//		// Play the dying animation
//		this.play("dying");
//		
//		// Slowly fade out the player
//		this.alpha = this.alpha - FADE_AMOUNT;
//		
//		// Stop all motion related to the player
//		this.immovable = true;
//		this.velocity.y = 0;
//		this.velocity.x = 0;
//	}
//	
//	/**
//		 * Special update conditions for FLYING
//		 */
//	private function updateFlying():void
//	{
//		var yVelocity:Number;
//		var yVelocityNew:Number;
//		
//		yVelocity = this.velocity.y;
//		yVelocityNew = yVelocity;
//		
//		
//		
//		/*this.colorChange++;
//			
//			if (this.colorChange < 5)
//				this.color = green;
//			else if(this.colorChange < 10)
//				this.color = red;
//			else if(this.colorChange < 15)
//				this.color = yellow;
//			else if(this.colorChange < 20)
//				this.color = blue;
//			else if(this.colorChange < 25)
//				this.color = orange;
//			else if(this.colorChange > 25)
//				this.colorChange = 0;
//			*/
//		
//		
//		// Accelerate to maximum flying velocity
//		if (yVelocity > FLYING_VELOCITY)
//		{
//			this.acceleration.y = FLYING_ACCELERATION;
//			//yVelocityNew = yVelocity += FLYING_ACCELERATION;
//		}
//		
//		// Make sure we haven't surpassed the maximum velocity
//		if (yVelocityNew < FLYING_VELOCITY)
//		{
//			this.velocity.y = FLYING_VELOCITY;
//			//yVelocityNew = FLYING_VELOCITY;
//		}
//		
//		// Update the player's velocity
//		//this.velocity.y = yVelocityNew;
//		
//		// Set the flying animation
//		this.play("flying");
//	}
//	
//	/**
//		 * Special update conditions for LADDER
//		 */
//	private function updateLadder():void
//	{
//		
//	}
//	
//	/**
//		 * Special update conditions for PENDULUM
//		 */
//	private function updatePendulum():void
//	{
//		
//	}
//	
//	/**
//		 * Specidal update procedures for LANDING
//		 */
//	private function updateLanding():void
//	{
//		// Restart the crouch timer
//		_crouchTimer.abort();
//		_crouchTimer.start();
//		Audio.play("land");
//		
//		// Set the frame to show crouching
//		this.play("landing");
//	}
//	
//	/**
//		 * Update procedures for SPRING
//		 * 
//		 * 6/30/2012: I don't think it's a good idea to declare a FlxDelay inside an update function....
//		 */
//	private function updateSpring():void
//	{	
//		//var springTimer:FlxDelay;
//		
//		if (this.y < (_springOriginalPosition - _springBoostHeight))
//		{
//			this.endSpringReact();
//		}
//		
//		// If we just hit the spring, do some initializations
//		if (_springHit == true && _springTimer.isRunning == false)
//		{
//			_springReact = true;
//			
//			// Set callback of our springTimer so Player starts falling
//			// after a certain amount of time in the air
//			
//			// Why do this INSIDE update? Seems dangeous.
//			
//			_springTimer.callback = endSpringReact;
//			_springTimer.start();
//		}
//		
//		// To react to spring, hold the value of the player velocity
//		// to something high that moves upwards
//		if (_springReact == true)
//		{
//			// Set velocity to shoot the player upword
//			this.velocity.y = SPRING_VELOCITY;
//		}
//		
//		//var springSideTimer:FlxDelay;
//		
//		// If we just hit the spring, do some initializations
//		if (_springSideHit == true)
//		{
//			_springSideReact = true;
//			
//			// Set callback of our springTimer so Player starts falling
//			// after a certain amount of time in the air
//			
//			//springSideTimer = new FlxDelay(SPRING_SIDE_BOOST_TIME);
//			
//			_springSideTimer.callback = endSpringSideReact;
//			_springSideTimer.start();
//		}
//		
//		// To react to spring, hold the value of the player velocity
//		// to something high that moves upwards
//		if (_springSideReact == true)
//		{
//			// Set velocity to shoot the player sideways
//			if ( _springSidePushLeft == true ) {
//				this.velocity.x = SPRING_SIDE_VELOCITY;
//			}
//			else { this.velocity.x = -SPRING_SIDE_VELOCITY;}
//		}
//	}
//	
//	/**
//		 * Update procedures for RIDING_UP
//		 */
//	private function updateRiding():void
//	{	
//		this.y -= RISING_SPEED;
//	}
//	
//}
//
//}
