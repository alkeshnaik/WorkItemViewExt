﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YL.Timeline.Interaction
{
	public class RevisionLoader
	{
		public delegate RevisionChanges LoadInfoDelegate(int workItemId, int rev);
	}
}
