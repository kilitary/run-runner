using System.Diagnostics;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

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
					return "Unable to get error code string from System - Error " + le;
				}

				string sRet = Marshal.PtrToStringAnsi(lpMsgBuf);

				// Free the buffer.
				lpMsgBuf = LocalFree(lpMsgBuf);
				return sRet;
			}
			catch(Exception e)
			{
				return "Unable to get error code string from System -> " + e;
			}
		}

		public static void Debug(string msg)
		{
			OutputDebugStringA($"#run-runner {msg}");
		}
/*
		public static string Jsonify(object obj, bool indent = false)
        {
            if(indent)
				return JsonConvert.SerializeObject(obj, Formatting.Indented);
            return JsonConvert.SerializeObject(obj);
        }*/

		public static long GetTimestamp()
		{
			DateTimeOffset now = DateTime.UtcNow;
			return now.ToUnixTimeSeconds();
		}

        public static String GetSystemUpTimeInfo()
        {
            try
            {
                var time = GetSystemUpTime();
                var upTime = $"{ (object) time.Hours:D2}h:{ (object) time.Minutes:D2}m:{ (object) time.Seconds:D2}s";//:{ (object) time.Milliseconds:D3}ms
				return $"{ (object) upTime}";
            }
            catch (Exception ex)
            {
                //handle the exception your way
                return String.Empty;
            }
        }

        public static TimeSpan GetSystemUpTime()
        {
            try
            {
                using (var uptime = new PerformanceCounter("System", "System Up Time"))
                {
                    uptime.NextValue();
                    return TimeSpan.FromSeconds(uptime.NextValue());
                }
            }
            catch (Exception ex)
            {
                //handle the exception your way
                return new TimeSpan(0, 0, 0, 0);
            }
        }
	}
}
