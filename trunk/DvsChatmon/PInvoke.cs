/*
 *  This file is part of DvsParse.
 * 
 *  DvsParse is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 * 
 *  DvsParse is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 * 
 *  You should have received a copy of the GNU General Public License
 *  along with DvsParse; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA  
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DvsChatmon
{
	[Flags]
	public enum MemoryRegionProtection : uint
	{
		NoAccess = 0x01,
		ReadOnly = 0x02,
		ReadWrite = 0x04,
		WriteCopy = 0x08,
		Execute = 0x10,
		ExecuteRead = 0x20,
		ExecuteReadWrite = 0x40,
		ExecuteWriteCopy = 0x80,
		Guard = 0x100,
		NoCache = 0x200,
		WriteCombine = 0x400
	}

	public enum MemoryRegionState : uint
	{
		Commit = 0x1000,
		Free = 0x10000,
		Reserve = 0x2000
	}

	[Flags]
	public enum MemoryRegionType : uint
	{
		Image = 0x1000000,
		Mapped = 0x40000,
		Private = 0x20000
	}

	[Flags]
	public enum FileMappingProtection : int
	{
		Page_Readonly = 0x02,
		Page_ReadWrite = 0x04,
		Page_WriteCopy = 0x08,
		Page_ExecuteRead = 0x20,
		Page_ExecuteReadWrite = 0x40,
		Section_Image = 0x1000000,
		Section_Commit = 0x8000000,
		Section_Reserve = 0x4000000,
		Section_NoCache = 0x10000000
	}

	public enum FileMappingAccess : int
	{
		Copy = 0x0001,
		Write = 0x0002,
		Read = 0x0004,
		Execute = 0x0020
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct MemoryInformation
	{
		public IntPtr BaseAddress;
		public IntPtr AllocationBase;
		public MemoryRegionProtection AllocationProtect;
		public uint RegionSize;
		public MemoryRegionState State;
		public MemoryRegionProtection Protect;
		public MemoryRegionType Type;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal class SECURITY_ATTRIBUTES
	{
		uint	StructLength;
		IntPtr	SecurityDescriptor;
		bool	InheritHandle;
	}

	public static class PInvoke
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CreateFileMapping(IntPtr FileHandle, SECURITY_ATTRIBUTES Attributes, uint Protection, uint MaxSizeHigh, uint MaxSizeLow, string Name);

		[DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern bool ReadProcessMemory(IntPtr ProcessHandle, IntPtr Address, IntPtr OutputBuffer, UIntPtr nBufferSize, out UIntPtr lpNumberOfBytesRead);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr MapViewOfFile(IntPtr FileMappingHandle, uint DesiredAccess, uint FileOffsetHigh, uint FileOffsetLow, uint NumBytesToMap);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint="UnmapViewOfFile", SetLastError = true)]
		private static extern bool _UnmapViewOfFile(IntPtr ViewHandle);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "CloseHandle", SetLastError = true)]
		private static extern bool _CloseHandle(IntPtr Handle);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "MoveMemory", SetLastError = true)]
		private static extern bool _MoveMemory(IntPtr Destination, IntPtr Source, int Length);

		public static void MoveMemory(IntPtr Destination, IntPtr Source, int Length)
		{
			_MoveMemory(Destination, Source, Length);
		}

		public static void UnmapViewOfFile(IntPtr ViewHandle)
		{
			_UnmapViewOfFile(ViewHandle);
		}

		public static void CloseHandle(IntPtr Handle)
		{
			_CloseHandle(Handle);
		}

		public static IntPtr CreateFileMapping(IntPtr FileHandle, FileMappingProtection Protection, long MaxMappingSize, string MappingName)
		{
			uint MappingLow = (uint)(MaxMappingSize & 0xFFFFFFFF);
			uint MappingHigh = (uint)(MaxMappingSize >> 32);
			IntPtr Result = CreateFileMapping(FileHandle, null, (uint)Protection, MappingHigh, MappingLow, MappingName);
			if (Result == IntPtr.Zero)
				throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			return Result;
		}

		public static IntPtr MapViewOfFile(IntPtr FileMappingHandle, FileMappingAccess Access, long FileOffset, uint NumBytesToMap)
		{
			uint FileOffsetLow = (uint)(FileOffset & 0xFFFFFFFF);
			uint FileOffsetHigh = (uint)(FileOffset >> 32);
			IntPtr Result = MapViewOfFile(FileMappingHandle, (uint)Access, FileOffsetHigh, FileOffsetLow, NumBytesToMap);
			if (Result == IntPtr.Zero)
				throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
			return Result;
		}

		public static IntPtr ReadProcessMemory(IntPtr ProcessHandle, IntPtr Address, uint nBytesToRead)
		{
			IntPtr Buffer = Marshal.AllocHGlobal((int)nBytesToRead);
			UIntPtr BytesRead = UIntPtr.Zero;
			UIntPtr BytesToRead = (UIntPtr)nBytesToRead;
			if (!ReadProcessMemory(ProcessHandle, Address, Buffer, BytesToRead, out BytesRead))
			{
				int Error = Marshal.GetLastWin32Error();
				if (Error == 299)	//ERROR_PARTIAL_COPY
					return Buffer;
				Marshal.FreeHGlobal(Buffer);
				return IntPtr.Zero;
			}
			return Buffer;
		}
	}
}
