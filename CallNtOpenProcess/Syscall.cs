using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using static CallNtOpenProcess.Native;
using System.ComponentModel;


namespace CallNtOpenProcess
{
    class Syscall
    {
        static byte[] bNtOpenProcess =
        {
            0x4C, 0x8B, 0xD1,               // mov r10, rcx
            0xB8, 0x26, 0x00, 0x00, 0x00,   // mov eax, 0x26 (NtOpenProcess Syscall)
            0x0F, 0x05,                     // syscall
            0xC3                            // ret
        };

        public static NTSTATUS NtOpenProcess(ref IntPtr ProcessHandle, UInt32 AccessMask, ref OBJECT_ATTRIBUTES ObjectAttributes, ref CLIENT_ID ClientId)
        {
            byte[] syscall = bNtOpenProcess;

            unsafe
            {
                fixed (byte* p = syscall)
                {
                    IntPtr memoryAddress = (IntPtr)p;

                    if(!VirtualProtect(memoryAddress, (UIntPtr)syscall.Length, 0x00000040, out uint lpflOldProtect))
                    {
                        throw new Win32Exception();
                    }

                    Delegates.NtOpenProcess assembledFunction = (Delegates.NtOpenProcess)Marshal.GetDelegateForFunctionPointer(memoryAddress, typeof(Delegates.NtOpenProcess));

                    return assembledFunction(ref ProcessHandle, AccessMask, ref ObjectAttributes, ref ClientId);
                }
            }
        }

        public struct Delegates
        {
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NTSTATUS NtOpenProcess(ref IntPtr ProcessHandle, UInt32 AccessMask, ref OBJECT_ATTRIBUTES ObjectAttributes, ref CLIENT_ID ClientId);
        }

    }
}
