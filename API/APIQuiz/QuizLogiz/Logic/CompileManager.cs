using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using Microsoft.CSharp;

namespace TestRunner.Logic
{
    class CompileManager
    {
        public ProcessResultModel CompileCode(string Code)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            var icc = codeProvider.CreateCompiler();

            string Output = "Out.exe";
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters()
            {
                GenerateExecutable = true,
                OutputAssembly = Output,

            };
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, Code);
            ProcessResultModel res = new ProcessResultModel();

            if (results.Errors.Count > 0)
            {
                string errors = "Compile is failed: ";
                foreach (CompilerError err in results.Errors)
                {
                    errors += "Line number " + err.Line + ", Error number " + err.ErrorNumber + " " + err.ErrorText +
                              "\n \n";
                }

                res.ExitCode = 1;
                res.Result = errors;
                return res;
            }

            res.ExitCode = 0;
            res.Result = "Success";
            return res;
        }

        public string RunExe(string parameters)
        {
            var arguments = string.Format("Out.exe") + " " + parameters;

            var cmd = new Process
            {
                StartInfo =
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.GetEncoding(866),
                    RedirectStandardError = true,
                    FileName = @"cmd.exe",
                    Arguments = @"/C " + arguments
                }
            };
            // Запуск
            cmd.Start();
            // Чекати на завершення
            cmd.WaitForExit();
            return cmd.StandardOutput.ReadToEnd();
        }

        public void DeleteFiles()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\" + "Out.exe"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\" + "Out.exe");
            }
        }
    }
}
