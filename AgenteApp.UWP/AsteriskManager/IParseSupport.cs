using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenterNET
{
	internal interface IParseSupport
	{
		Dictionary<string, string> Attributes
		{
			get;
		}
		bool Parse(string key, string value);
		Dictionary<string, string> ParseSpecial(Dictionary<string, string> attributes);
	}
}
