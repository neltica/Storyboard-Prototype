using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProjectTest
{
    class PrototypeMake
    {
        public string str;
        Process process;

        public PrototypeMake(int act)
        {
            try
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\prototype");

                if (!di.Exists)
                {
                    di.Create();
                    Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine("Exists");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            ProcessStartInfo cmd = new ProcessStartInfo();
            process = new Process();
            cmd.FileName = @"cmd";
            cmd.WindowStyle = ProcessWindowStyle.Hidden;             // cmd창이 숨겨지도록 하기
            cmd.CreateNoWindow = true;                               // cmd창을 띄우지 안도록 하기

            cmd.UseShellExecute = false;
            cmd.RedirectStandardOutput = true;        // cmd창에서 데이터를 가져오기
            cmd.RedirectStandardInput = true;          // cmd창으로 데이터 보내기
            cmd.RedirectStandardError = true;          // cmd창에서 오류 내용 가져오기
            process.EnableRaisingEvents = false;
            process.StartInfo = cmd;
            process.Start();
            if (act == 0)
            {
                process.StandardInput.Write(@"Storyboard.exe 0" + Environment.NewLine);
            }
            else if (act == 1)
            {
                process.StandardInput.Write(@"Storyboard.exe 1" + Environment.NewLine);
            }
            process.StandardInput.Close();

            str = process.StandardOutput.ReadToEnd();


            process.WaitForExit();
            process.Close();
            process = null;

            Console.WriteLine(str);

        }

        public int result()
        {
            if (process != null)
            {
                return -2;   //대기신호
            }
            if (str.IndexOf("<success>") != -1)
            {
                return 0;  //성공신호
            }
            else
            {
                return -1;   //에러
            }
        }
    }
}
