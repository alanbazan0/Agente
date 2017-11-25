namespace CallCenterNET.Manager.Event
{
	/// <summary>
	/// An OriginateSuccessEvent is triggered when the execution of an OriginateAction succeeded.
	/// </summary>
	/// <seealso cref="Manager.Action.OriginateAction" />
	public class OriginateSuccessEvent : OriginateResponseEvent
	{
		public OriginateSuccessEvent(ManagerConnection source)
			: base(source)
		{
		}
	}
}