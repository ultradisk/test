// Copyright (c) AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory.TypeSystem;

namespace ICSharpCode.SharpDevelop.Refactoring
{
	/*
	public sealed class TypeGraphNode
	{
		internal readonly ITypeDefinition typeDef;
		readonly List<TypeGraphNode> baseTypes = new List<TypeGraphNode>();
		readonly List<TypeGraphNode> derivedTypes = new List<TypeGraphNode>();
		
		public TypeGraphNode(ITypeDefinition typeDef)
		{
			this.typeDef = typeDef;
		}
		
		public ITypeDefinition TypeDefinition {
			get { return typeDef; }
		}
		
		public IList<TypeGraphNode> DerivedTypes {
			get { return derivedTypes; }
		}
		
		public IList<TypeGraphNode> BaseTypes {
			get { return baseTypes; }
		}
		
		public ITreeNode<ITypeDefinition> ConvertToBaseTypeTree()
		{
			return TreeNode<ITypeDefinition>.FromGraph(this, n => n.baseTypes, n => n.typeDef);
		}
		
		public ITreeNode<ITypeDefinition> ConvertToDerivedTypeTree()
		{
			return TreeNode<ITypeDefinition>.FromGraph(this, n => n.derivedTypes, n => n.typeDef);
		}
	}*/
}
