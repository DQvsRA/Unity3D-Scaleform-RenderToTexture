package  {
	
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.external.ExternalInterface;
	
	
	public class Menu extends MovieClip {
		
		public function Menu() {
			// constructor code
			addEventListener(Event.ENTER_FRAME, Initialize);
		}
		
		public function Initialize(e:Event) {
			removeEventListener(Event.ENTER_FRAME, Initialize);
            ExternalInterface.call("OnRegisterSWFCallback", this);
		}
	}
	
}
