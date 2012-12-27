// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Linq;
using ICSharpCode.FSharpFormsDesigner;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Dom;
using NUnit.Framework;
using Rhino.Mocks;

namespace FSharpFormsDesigner.Tests
{
	[TestFixture]
	public class FSharpParserTests
	{
		ICompilationUnit unit;
		IProjectContent fakeProjectContent;
		
		void Parse(string code)
		{
			string fileName = @"c:\projects\test\test.fs";
			fakeProjectContent = MockRepository.GenerateStub<IProjectContent>();
			ITextBuffer buffer = CreateTextBuffer(code);
			
			var parser = new FSharpParser();
			unit = parser.Parse(fakeProjectContent, fileName, buffer);
		}
		
		ITextBuffer CreateTextBuffer(string code)
		{
			var buffer = MockRepository.GenerateStub<ITextBuffer>();
			buffer.Stub(b => b.Text).Return(code);
			return buffer;
		}
		
		[Test]
		public void Parse_OneClass_ReturnsCompilationUnitWithOneClass()
		{
			string code =
				"type Customer() = \r\n" +
				"    class\r\n" +
				"    end\r\n" +
				"\r\n";
			
			Parse(code);
			
			IClass c = unit.Classes.FirstOrDefault();
			Assert.AreEqual(1, unit.Classes.Count);
			Assert.AreEqual("Customer", c.Name);
		}
		
		[Test]
		public void Parse_OneClass_CompilationUnitClassHasClassRegion()
		{
			string code =
				"type Customer() = \r\n" +
				"    class\r\n" +
				"    end\r\n" +
				"\r\n";
			var expectedRegion = new DomRegion(1, 1, 1, 16);
			
			Parse(code);
			
			IClass c = unit.Classes.FirstOrDefault();
			Assert.AreEqual(expectedRegion, c.Region);
		}
		
		[Test]
		public void Parse_OneClassWithOneMethod_ReturnsOneClassMethod()
		{
			string code =
				"type Customer() as this =\r\n" +
				"    member this.GetName() = \r\n" +
				"        return \"Joe\"\r\n" +
				"\r\n";
			
			Parse(code);
			
			IClass c = unit.Classes.FirstOrDefault();
			Assert.AreEqual(1, c.Methods.Count);
			Assert.AreEqual("GetName", c.Methods[0].Name);
		}
		
		[Test]
		public void Parse_OneClassWithOneMethod_MethodHasRegions()
		{
			string code =
				"type Customer() as this =\r\n" +
				"    member this.GetName() =\r\n" +
				"        return \"Joe\"\r\n" +
				"\r\n";
			
			var expectedRegion = new DomRegion(2, 1, 2, 26);
			var expectedBodyRegion = new DomRegion(2, 26, 3, 21);
			Parse(code);
			
			IClass c = unit.Classes.FirstOrDefault();
			IMethod method = c.Methods.FirstOrDefault();
			Assert.AreEqual(expectedRegion, method.Region);
			Assert.AreEqual(expectedBodyRegion, method.BodyRegion);
		}
		
		[Test]
		public void Parse_OneClassWithOneMethod_ClassHasRegions()
		{
			string code =
				"type Customer() as this =\r\n" +
				"    member this.GetName() =\r\n" +
				"        return \"Joe\"\r\n" +
				"\r\n";
			
			var expectedRegion = new DomRegion(1, 1, 1, 16);
			var expectedBodyRegion = new DomRegion(1, 16, 3, 21);
			Parse(code);
			
			IClass c = unit.Classes.FirstOrDefault();
			Assert.AreEqual(expectedRegion, c.Region);
			Assert.AreEqual(expectedBodyRegion, c.BodyRegion);
		}
	}
}