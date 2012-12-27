// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using Microsoft.FSharp.Compiler.SourceCodeServices;
using Microsoft.FSharp.Core;

namespace ICSharpCode.FSharpFormsDesigner
{
	/// <summary>
	/// Description of FSharpNavigationItemsParser.
	/// </summary>
	public class FSharpNavigationItemsParser
	{
		InteractiveChecker checker;
		
		public FSharpNavigationItemsParser()
		{
			FSharpFunc<string, Unit> dirty = FuncConvert.ToFSharpFunc<string>(FileIsDirtyCheck);
			checker = InteractiveChecker.Create(dirty);
		}
		
		static void FileIsDirtyCheck(string file)
		{
			Console.WriteLine("FileIsDirtyCheck: " + file);
		}
		
		public NavigationItems GetNavigationItems(string fileName, string source)
		{
			CheckOptions options = checker.GetCheckOptionsFromScriptRoot(fileName, source);
			UntypedParseInfo parseInfo = checker.UntypedParse(fileName, source, options);
			return parseInfo.GetNavigationItems();
		}
	}
}
