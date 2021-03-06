// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
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

#pragma warning disable 108, 1591 

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Debugger.Interop.CorPublish
{
	[ComImport, TypeLibType((short) 2), ClassInterface((short) 0), Guid("047A9A40-657E-11D3-8D5B-00104B35E7EF")]
	public class CorpubPublishClass : ICorPublish, CorpubPublish, ICorPublishProcess, ICorPublishAppDomain, ICorPublishProcessEnum, ICorPublishAppDomainEnum
	{		
	    // Methods
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __Clone([MarshalAs(UnmanagedType.Interface)] out ICorPublishEnum ppEnum);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __EnumAppDomains([MarshalAs(UnmanagedType.Interface)] out ICorPublishAppDomainEnum ppEnum);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __EnumProcesses([In, ComAliasName("CorpubProcessLib.COR_PUB_ENUMPROCESS")] COR_PUB_ENUMPROCESS Type, [MarshalAs(UnmanagedType.Interface)] out ICorPublishProcessEnum ppIEnum);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __GetCount(out uint pcelt);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __GetDisplayName([In] uint cchName, out uint pcchName, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder szName);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __GetID(out uint puId);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __GetName([In] uint cchName, out uint pcchName, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder szName);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __GetProcess([In] uint pid, [MarshalAs(UnmanagedType.Interface)] out ICorPublishProcess ppProcess);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __GetProcessID(out uint pid);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __ICorPublishAppDomainEnum_Clone([MarshalAs(UnmanagedType.Interface)] out ICorPublishEnum ppEnum);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __ICorPublishAppDomainEnum_GetCount(out uint pcelt);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __ICorPublishAppDomainEnum_Reset();
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __ICorPublishAppDomainEnum_Skip([In] uint celt);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __IsManaged(out int pbManaged);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out ICorPublishAppDomain objects, out uint pceltFetched);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out ICorPublishProcess objects, out uint pceltFetched);
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __Reset();
	    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
	    public virtual extern void __Skip([In] uint celt);
	}

	[ComImport, CoClass(typeof(CorpubPublishClass)), Guid("9613A0E7-5A68-11D3-8F84-00A0C9B4D50C")]
	public interface CorpubPublish : ICorPublish
	{
	}

	public enum COR_PUB_ENUMPROCESS
	{
		COR_PUB_MANAGEDONLY = 1
	}
 
	[ComImport, Guid("9613A0E7-5A68-11D3-8F84-00A0C9B4D50C"), InterfaceType((short) 1)]
	public interface ICorPublish
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __EnumProcesses([In, ComAliasName("CorpubProcessLib.COR_PUB_ENUMPROCESS")] COR_PUB_ENUMPROCESS Type, [MarshalAs(UnmanagedType.Interface)] out ICorPublishProcessEnum ppIEnum);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __GetProcess([In] uint pid, [MarshalAs(UnmanagedType.Interface)] out ICorPublishProcess ppProcess);
	}

	[ComImport, Guid("D6315C8F-5A6A-11D3-8F84-00A0C9B4D50C"), InterfaceType((short) 1)]
	public interface ICorPublishAppDomain
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __GetID(out uint puId);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __GetName([In] uint cchName, out uint pcchName, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder szName);
	}

	[ComImport, Guid("9F0C98F5-5A6A-11D3-8F84-00A0C9B4D50C"), InterfaceType((short) 1),]
	public interface ICorPublishAppDomainEnum : ICorPublishEnum
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Skip([In] uint celt);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Reset();
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Clone([MarshalAs(UnmanagedType.Interface)] out ICorPublishEnum ppEnum);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __GetCount(out uint pcelt);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out ICorPublishAppDomain objects, out uint pceltFetched);
	}

	[ComImport, Guid("C0B22967-5A69-11D3-8F84-00A0C9B4D50C"), InterfaceType((short) 1)]
	public interface ICorPublishEnum
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Skip([In] uint celt);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Reset();
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Clone([MarshalAs(UnmanagedType.Interface)] out ICorPublishEnum ppEnum);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __GetCount(out uint pcelt);
	}

	[ComImport, Guid("18D87AF1-5A6A-11D3-8F84-00A0C9B4D50C"), InterfaceType((short) 1)]
	public interface ICorPublishProcess
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __IsManaged(out int pbManaged);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __EnumAppDomains([MarshalAs(UnmanagedType.Interface)] out ICorPublishAppDomainEnum ppEnum);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __GetProcessID(out uint pid);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __GetDisplayName([In] uint cchName, out uint pcchName, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder szName);
	}

	[ComImport, Guid("A37FBD41-5A69-11D3-8F84-00A0C9B4D50C"), InterfaceType((short) 1)]
	public interface ICorPublishProcessEnum : ICorPublishEnum
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Skip([In] uint celt);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Reset();
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Clone([MarshalAs(UnmanagedType.Interface)] out ICorPublishEnum ppEnum);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __GetCount(out uint pcelt);
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void __Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out ICorPublishProcess objects, out uint pceltFetched);
	}
}
