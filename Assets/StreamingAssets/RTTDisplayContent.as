package  {
	
	import fl.motion.easing.Linear;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import fl.transitions.TweenEvent;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	import flash.utils.setTimeout;
	
	import flash.display.MovieClip;
	import flash.display.Shape;
	import flash.events.Event;
	import flash.external.ExternalInterface;
	import flash.geom.ColorTransform;
	
	public class RTTDisplayContent extends MovieClip {
		
		private static const 		SPEED		:Number = 150;
		
		private var 	_tweenX				:Tween;
		private var 	_tweenY				:Tween;
		
		private var 	_isTweenXReady		:Boolean = false;
		private var 	_isTweenYReady		:Boolean = false;
		
		
		private var 	_time				:Number = 0;
		private var 	_positionX			:Number = 0;
		private var 	_positionY			:Number = 0;
		
		public function RTTDisplayContent() {
			// constructor code
			addEventListener(Event.ENTER_FRAME /* ADDED_TO_STAGE */, Initialize); 
		} 
		
		public function Initialize(e:Event) {
			removeEventListener(Event.ENTER_FRAME /* ADDED_TO_STAGE */, Initialize); 
			
			_positionX = stage.stageWidth * (1 + Math.random()) * 0.5;
			_positionY = stage.stageHeight * (1 + Math.random()) * 0.5;
			_time =  CalculateTime(_positionX, _positionY);
			
			_tweenX = new Tween(mc_ball, "x", Linear.easeOut, mc_ball.x, _positionX, _time, true);
			_tweenX.addEventListener(TweenEvent.MOTION_FINISH, Handler_Tween_OnFinish);
			_tweenX.start();
			
			_tweenY = new Tween(mc_ball, "y", Linear.easeOut, mc_ball.y, _positionY, _time, true);
			_tweenY.addEventListener(TweenEvent.MOTION_FINISH, Handler_Tween_OnFinish);
			_tweenY.start();
			
			ExternalInterface.call("OnRegisterSWFCallback", this);
			//stage.addEventListener(KeyboardEvent.KEY_DOWN, function (e:KeyboardEvent):void {
				//if (e.keyCode == Keyboard.SPACE) Regenerate();
			//})
		}
		
		public function Regenerate():void {
			StopTweening();
			
			RecolorBall();
			RepositionBall(_tweenX.position, _tweenY.position);
			RepositionTween();
		}
		
		private function Handler_Tween_OnFinish(e:TweenEvent):void {
			var target:Tween = e.target as Tween;
			if (target.prop == "x") _isTweenXReady = true;
			else _isTweenYReady = true;
			
			if (_isTweenYReady && _isTweenXReady) {
				_isTweenYReady = _isTweenXReady = false;
				
				RepositionTween();
			}
		}
		
		private function RecolorBall():void {
			var shp:Shape = (mc_ball.getChildAt(0) as Shape);
			var clrt:ColorTransform = new ColorTransform();
			clrt.color = Math.random() * 0xFFFFFF;
			shp.transform.colorTransform = clrt;
		}
		
		private function RepositionBall(xp:Number, yp:Number):void {
			mc_ball.x = xp;
			mc_ball.y = yp;
		}
		
		private function RepositionTween():void {
			var random:Number = Math.random();
			_isTweenYReady = _isTweenXReady = false;
			
			_positionX 			= stage.stageWidth * random;
			_positionY 			= stage.stageHeight * (random = Math.random());
			_time 				= CalculateTime(_positionX, _positionY);
			
			_tweenX.continueTo(_positionX, _time);
			_tweenY.continueTo(_positionY, _time);
			
			_tweenX.time = _tweenY.time = 0;
		}
		
		private function StopTweening():void {
			_tweenX.stop();
			_tweenY.stop();
		}
		
		private function CalculateTime(x:Number, y:Number):Number {
			var deltaX			:Number = mc_ball.x - x;
			var deltaY			:Number = mc_ball.y - y;
			var deltaXSquare	:Number = deltaX * deltaX;
			var deltaYSquare	:Number = deltaY * deltaY;
			var summa			:Number = deltaYSquare + deltaXSquare;
			var result			:Number = Math.sqrt(summa);
			return result / SPEED;
		}
	}
	
}
