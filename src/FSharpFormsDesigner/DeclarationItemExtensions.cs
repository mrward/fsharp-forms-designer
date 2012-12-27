// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.NRefactory;
using Microsoft.FSharp.Compiler.SourceCodeServices;

namespace ICSharpCode.FSharpFormsDesigner
{
	public static class DeclarationItemExtensions
	{
		public static Location GetRangeStart(this DeclarationItem item)
		{
			return ToLocation(item.Range.Item1);
		}
		
		static Location ToLocation(Tuple<int, int> tuple)
		{
			return new Location(tuple.Item1 + 1, tuple.Item2);
		}
		
		public static Location GetBodyRangeEnd(this DeclarationItem item)
		{
			return ToLocation(item.BodyRange.Item2);
		}
	}
}
