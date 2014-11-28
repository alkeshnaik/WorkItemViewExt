﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YL.Timeline.Interaction
{
	public sealed class Changeset
	{
		public bool? IsAdded { get; set; }

		public Uri Uri { get; set; }

		public string Comment { get; set; }
	}
}
