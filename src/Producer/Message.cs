﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Producer
{
	public class Message
	{
		public string To { get; set; }
		public string From { get; set; }
		public string Body { get; set; }
	}
}
