namespace CallCenterNET.FastAGI.Scripts
{
	class AGINoAction : AGIScript
	{
		public override void Service(AGIRequest request, AGIChannel channel)
		{
			base.Hangup();
		}
	}
}
