using System;
namespace CallCenterNET.FastAGI.Command
{
	public class AnswerCommand : AGICommand
	{
		public AnswerCommand()
		{
		}
		public override string BuildCommand()
		{
			return "ANSWER";
		}
	}
}