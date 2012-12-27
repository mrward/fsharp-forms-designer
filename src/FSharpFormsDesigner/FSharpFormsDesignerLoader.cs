// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Security.Permissions;
using ICSharpCode.Scripting;

namespace ICSharpCode.FSharpFormsDesigner
{
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class FSharpFormsDesignerLoader : ScriptingDesignerLoader
	{
		public FSharpFormsDesignerLoader(IScriptingDesignerGenerator generator)
			: base(generator)
		{
		}
		
		protected override IComponentWalker CreateComponentWalker(IComponentCreator componentCreator)
		{
			return new FSharpComponentWalker(componentCreator);
		}
	}
}
