using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestRunner
{
    
    public class TaskManager
    {
        public string UserCode;

        public string[] TestValues;
        public string[] TestResults;

        public List<string> results = new List<string>();

        private CodeCompiler Compiler;

        public bool SetCode(string code)
        {
            if (code != null)
            {
                UserCode = code;
                return true;
            }
            return false;
        }

        public bool RunTest(string testValues, string expectation)
        {
            
            // Потрібно розібратись як закидувати в компілятор код 
            // Та дивитись чи ці тести виконались правильно                      
            var runExe = Compiler.RunExe(testValues);
            
            results.Add(runExe.Result);
            return expectation.CompareTo(runExe.Result) == 0 ? true : false;

        }

        public bool RunAll()
        {

            var programPath = Directory.GetCurrentDirectory();
            const string programName = "test";
            string Log = " ";
            Compiler = new CodeCompiler();
            Compiler.CreateCs(programPath, programName, UserCode);
            const string pathToVsvars32 = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools";
            ProcessResultModel result = Compiler.CompileProgram(pathToVsvars32, Directory.GetCurrentDirectory());
            if (result.ExitCode != 0)
            {
                throw new Exception(" Cannot compile this code ");
            }
            
            for (int i = 0; i < TestValues.Length; i++)
            {
                var str = RunTest(TestValues[i], TestResults[i]);
                Log += str;
            }

            Compiler.DeleteFiles(programPath);
            return false;
        }
        
    }
}
