namespace CallCenterNET.Manager.Event
{

	/// <summary>
	/// An OriginateFailureEvent is triggered when the execution of an OriginateAction failed.
	/// </summary>
	/// <seealso cref="Manager.Action.OriginateAction"/>
	public class OriginateFailureEvent : OriginateResponseEvent
	{
        public OriginateFailureEvent(ManagerConnection source)
			: base(source)
		{
		}
	}
}