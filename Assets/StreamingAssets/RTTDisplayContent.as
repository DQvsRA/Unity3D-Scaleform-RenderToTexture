package  {
	
	import fl.motion.easing.Linear;
	import fl.transitions.Tween;
	import fl.transitions.easing.*;
	import fl.transitions.TweenEvent;
	
	import flash.display.MovieClip;
	import flash.display.Shape;
	import flash.events.Event;
	import flash.external.ExternalInterface;
	import flash.geom.ColorTransform;
	
	public class RTTDisplayContent extends MovieClip {
		private var tweenX:Tween;
		private var tweenY:Tween;
		
		private var tweenXready:Boolean = false;
		private var tweenYready:Boolean = false;
		
		private var speed:Number = 150;
		private var time:Number = 0;
		private var positionX:Number = 0;
		private var positionY:Number = 0;
		
		public function RTTDisplayContent() {
			// constructor code
			addEventListener(Event.ENTER_FRAME, Initialize);
		}
		
		public function Initialize(e:Event) {
			removeEventListener(Event.ENTER_FRAME, Initialize);
            ExternalInterface.call("OnRegisterSWFCallback", this);
			Regenerate();
		}
		
		public function Regenerate() {
			mc_ball.x = stage.stageWidth * Math.random() * 0.5;
			mc_ball.y = stage.stageHeight * Math.random() * 0.5;
			var shp:Shape = (mc_ball.getChildAt(0) as Shape);
			var clrt:ColorTransform = new ColorTransform();
			clrt.color = Math.random() * 0xFFFFFF;
			shp.transform.colorTransform = clrt;
			
			GenerateTween();
		}
		
		private function GenerateTween():void {
			tweenYready = tweenXready = false;
			
			if (tweenX && tweenX.hasEventListener(TweenEvent.MOTION_FINISH)) {
				tweenX.stop();
				tweenX.removeEventListener(TweenEvent.MOTION_FINISH, Handler_Tween_OnFinish);
			}
			positionX = stage.stageWidth * (1 + Math.random()) * 0.5;
			positionY = stage.stageHeight * (1 + Math.random()) * 0.5;
			time = Math.sqrt(Math.pow(mc_ball.x - positionX, 2) + Math.pow(mc_ball.y - positionY, 2)) / speed;
			
			tweenX = new Tween(mc_ball, "x", Linear.easeOut, mc_ball.x, positionX, time, true);
			tweenX.addEventListener(TweenEvent.MOTION_FINISH, Handler_Tween_OnFinish);
			tweenX.start();
			
			if (tweenY && tweenY.hasEventListener(TweenEvent.MOTION_FINISH)) {
				tweenY.stop();
				tweenY.removeEventListener(TweenEvent.MOTION_FINISH, Handler_Tween_OnFinish);
			}
			tweenY = new Tween(mc_ball, "y", Linear.easeOut, mc_ball.y, positionY, time, true);
			tweenY.addEventListener(TweenEvent.MOTION_FINISH, Handler_Tween_OnFinish);
			tweenY.start();
		}
		
		private function Handler_Tween_OnFinish(e:TweenEvent):void {
			var target:Tween = e.target as Tween;
			if (target.prop == "x") tweenXready = true;
			else tweenYready = true;
			
			if (tweenYready && tweenXready) {
				tweenYready = tweenXready = false;
				
				positionX = stage.stageWidth * Math.random();
				positionY = stage.stageHeight * Math.random();
				time = Math.sqrt(Math.pow(mc_ball.x - positionX, 2) + Math.pow(mc_ball.y - positionY, 2)) / speed;
				
				tweenX.continueTo(positionX, time);
				tweenY.continueTo(positionY, time);
			}
			
		}
	}
	
}
