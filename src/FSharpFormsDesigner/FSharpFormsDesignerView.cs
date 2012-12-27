// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.FormsDesigner;
using ICSharpCode.SharpDevelop.Gui;

namespace ICSharpCode.FSharpFormsDesigner
{
	public class FSharpFormsDesignerView : FormsDesignerViewContent
	{
		public FSharpFormsDesignerView(
			IViewContent view,
			FSharpFormsDesignerLoaderProvider designerLoaderProvider,
			FSharpFormsDesignerGenerator designerGenerator)
			: base(view, designerLoaderProvider, designerGenerator)
		{
		}
	}
}
