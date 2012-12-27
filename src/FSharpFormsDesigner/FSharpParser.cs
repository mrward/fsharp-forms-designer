// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.IO;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Project;

namespace ICSharpCode.FSharpFormsDesigner
{
	public class FSharpParser : IParser
	{
		string[] lexerTags = new string[0];
		
		public string[] LexerTags {
			get { return lexerTags; }
			set { lexerTags = value; }
		}
		
		public LanguageProperties Language {
			get { return LanguageProperties.None; }
		}
		
		public IExpressionFinder CreateExpressionFinder(string fileName)
		{
			return null;
		}
		
		public bool CanParse(string fileName)
		{
			string extension = Path.GetExtension(fileName);
			if (extension != null) {
				return extension.ToLower() == ".fs";
			}
			return false;
		}
		
		public bool CanParse(IProject project)
		{
			return true;
		}
		
		public ICompilationUnit Parse(IProjectContent projectContent, string fileName, ITextBuffer fileContent)
		{
			try {
				
				var parser = new FSharpClassParser(projectContent, fileName, fileContent.Text);
				return parser.Parse();
				
			} catch (Exception ex) {
				LoggingService.Error("Parse failed", ex);
				return new DefaultCompilationUnit(projectContent);
			}
		}
		
		public IResolver CreateResolver()
		{
			return null;
		}
	}
}
