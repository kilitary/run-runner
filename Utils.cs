using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using static Vanara.PInvoke.Kernel32;

namespace run_runner
{
	public static class Utils
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr LocalFree(IntPtr hMem);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		static extern int GetLastError();

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern int FormatMessage(FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, uint nSize, IntPtr Arguments);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		static extern void OutputDebugStringA(string message);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		static extern void GetDllDirectoryA(int nBufferLength, string lpBuffer);

		[Flags]
		enum FormatMessageFlags : uint
		{
			FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100,
			FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200,
			FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000,
			FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000,
			FORMAT_MESSAGE_FROM_HMODULE = 0x00000800,
			FORMAT_MESSAGE_FROM_STRING = 0x00000400,
		}

		public static string GetSystemMessage(int errorCode = 0)
		{
			try
			{
				if(errorCode == 0)
					errorCode = GetLastError();

				IntPtr lpMsgBuf = IntPtr.Zero;

				int dwChars = FormatMessage(
					FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER | FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM | FormatMessageFlags.FORMAT_MESSAGE_IGNORE_INSERTS,
					IntPtr.Zero,
					(uint) errorCode,
					0, // Default language
					ref lpMsgBuf,
					0,
					IntPtr.Zero);
				if(dwChars == 0)
				{
					// Handle the error.
					int le = Marshal.GetLastWin32Error();
					return "Unable to get error code string from System - Error " + le.ToString();
				}

				string sRet = Marshal.PtrToStringAnsi(lpMsgBuf);

				// Free the buffer.
				lpMsgBuf = LocalFree(lpMsgBuf);
				return sRet;
			}
			catch(Exception e)
			{
				return "Unable to get error code string from System -> " + e.ToString();
			}
		}

		public static void Debug(string msg)
		{
			OutputDebugString($"#run-runner {msg}");
		}

		public static string Jsonify(object obj, bool indent = false)
		{
			if(indent)
				return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
			else
				return JsonConvert.SerializeObject(obj);
		}

		public static long GetTimestamp()
		{
			DateTimeOffset now = (DateTimeOffset) DateTime.UtcNow;
			return now.ToUnixTimeSeconds();
		}
	}
}
