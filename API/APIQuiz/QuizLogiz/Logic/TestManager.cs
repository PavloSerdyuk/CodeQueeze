using System;
using System.Collections.Generic;
using System.IO;
using TestRunner.Models;
//using System.Threading.Tasks;

namespace TestRunner.Logic
{
    
    public class TaskManager : IBlInterface
    {

        public string[] TestValues;
        public string[] TestResults;
        
        private CodeCompiler Compiler;
        
        public ProcessResultModel RunTest(string testValues, string expectation,string programPath)
        {
            
            // Потрібно розібратись як закидувати в компілятор код 
            // Та дивитись чи ці тести виконались правильно                      
            var runExe = Compiler.RunExe(programPath, testValues);
            ProcessResultModel result = new ProcessResultModel();
            result.Result = runExe.Result.Trim();
            if (result.Result.CompareTo(expectation) == 0)
            {
                result.ExitCode = 0;
            }
            else
            {
                result.ExitCode = 1;
            }

            
            return result;
        }
        
        public IQuizTask GetTask(int id, ConfigurationPaths paths)
        {
            var path = paths.FolderPath;
            path += "\\" + id +"\\";

            if (!Directory.Exists(path))
                return null;

            IQuizTask task = new QuizTask();
            task.Id = id;
            task.Name = System.IO.File.ReadAllText(path + "Name.txt");
            task.Description = System.IO.File.ReadAllText(path + "Description.txt");
            return task;
        }

        private void SetTests(int id, ConfigurationPaths paths)
        {
            var path = paths.FolderPath;
            path += "\\" + id.ToString() + "\\";
            TestValues = System.IO.File.ReadAllLines(path + "Tests.txt");
            TestResults = System.IO.File.ReadAllLines(path + "Results.txt");
        }
        public CheckTaskResponse CheckCode(CheckTaskRequest request, ConfigurationPaths paths)
        {
            var programPath = Directory.GetCurrentDirectory();
            CheckTaskResponse answer = new CheckTaskResponse();
            answer.Id = request.Id;
            answer.Result = true;
            Compiler = new CodeCompiler();
            Compiler.CreateCs(paths.CsFilePath, "test", request.Code);
            ProcessResultModel result = Compiler.CompileProgram(paths.CompilerPath, paths.CsFilePath);
            if (result.ExitCode != 0){
                answer.Result = false;
                answer.Message = " Cannot compile this code: " + result.Result;
                return answer;
            }
            SetTests(request.Id, paths);
            answer.Result = true;
            //var r = RunTest("", "hello");
            for (int i = 0; i < TestValues.Length; i++)
            {
                var res = RunTest(TestValues[i], TestResults[i], paths.CsFilePath);
                if (res.ExitCode != 0)
                {
                    answer.Result = false;
                }
                answer.Message += res.Result;
            }

            Compiler.DeleteFiles(paths.CsFilePath);
            return answer;
        }
    }
}
