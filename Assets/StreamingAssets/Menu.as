package  {
	
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.external.ExternalInterface;
	import flash.text.TextField;
	
	
	public class Menu extends MovieClip {
		
		private var _healthCounter:uint = 0;
		
		public function Menu() {
			// constructor code
			addEventListener(Event.ENTER_FRAME, Initialize);
		}
		
		public function Initialize(e:Event) {
			removeEventListener(Event.ENTER_FRAME, Initialize);
			tf_menu.addEventListener(MouseEvent.CLICK, Handler_Mouse_Click);
            ExternalInterface.call("OnRegisterSWFCallback", this);
		}
		
		private function Handler_Mouse_Click(e:MouseEvent):void {
			if (e.currentTarget is TextField) {
				tf_health.text = "health: " + (++_healthCounter);
				ExternalInterface.call("OnMouseClickHandler");
			}
		}
	}
	
}
