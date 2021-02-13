using System;
using System.IO;
using System.Text;

//use filestreams to read and write byte/array of bytes
namespace HexDump
{
    class Program
    {
        static void Main(string[] args)
        {
            //make path to text file
            string textFilePath;
            if (args.Length > 0)
            {
                 textFilePath = args[0];
            }
            else
            {
                textFilePath = "/Users/davidkim/Projects/HexDump/HexDump/bin/Debug/netcoreapp3.1/HexDump.dll";
            }
            byte[] buffer = new byte[16];

            using (BinaryReader reader = new BinaryReader(File.Open(textFilePath, FileMode.Open)))
            {
                int offset = 0;
                while (offset<reader.BaseStream.Length)
                {
                    if (offset % 16 == 0)
                    {
                        Console.Write("{0:X8}    ", offset);
                    }
                    int data = (int)reader.ReadByte();
                    if (char.IsLetterOrDigit((char)data)||char.IsSymbol((char)data))
                    {
                        buffer[offset % 16] = (byte)data;
                    }
                    else
                    {
                        buffer[offset % 16] = (byte)'.';
                    }
                    Console.Write("{0:X2}", data);
                    if (offset%16==7)
                    {
                        Console.Write("-");
                    }
                    else if (offset%16==15)
                    {
                        string converted = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        
                        Console.WriteLine("   {0}", converted);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    offset++;
                }


                if(offset%16!=0)
                {
                    do
                    {
                        buffer[offset % 16] = (byte)' ';
                        Console.Write("   ");
                        offset++;
                    }
                    while (offset % 16 != 0);
                        string converted = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

                        Console.WriteLine("  {0}", converted);
                }
            }
        }
    }
}
