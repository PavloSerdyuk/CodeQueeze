using System;
using System.IO;
using TestRunner.Models;
using System.Text.RegularExpressions;

namespace TestRunner.Logic
{
    
    public class TaskManager : IBlInterface
    {

        public string[] TestValues;
        public string[] TestResults;
        
        private CodeCompiler Compiler;
        
        // Running test on users code
        public ProcessResultModel RunTest(string testValues, string expectation,string programPath)
        {                
            var runExe = Compiler.RunExe(programPath, testValues);

            ProcessResultModel result = new ProcessResultModel() { Result = runExe.Result.Trim()};

            if (result.Result.CompareTo(expectation) == 0)
            {
                result.ExitCode = 0;
                result.Result = " Test values: " + testValues + " Test Passed\n" + "Result was: " + runExe.Result + "\n";
            }
            else
            {
                result.ExitCode = 1;
                result.Result = " Test Values: " + testValues + " Test Hadn`t been passed \n" + "Result was: " + runExe.Result + "\n";             
            }

            return result;
        }
        
        public IQuizTask GetTask(int id, ConfigurationPaths paths)
        {
            var path = paths.FolderPath;
            path += "\\" + id +"\\";

            IQuizTask task = new QuizTask() { Id = id };

            if(!Directory.Exists(path) || !File.Exists(path + "Name.txt") || !File.Exists(path + "ShortDescription.txt") || !File.Exists(path + "Description.txt"))
            {
                return null;
            }
            else
            {
                task.Name = System.IO.File.ReadAllText(path + "Name.txt");
                task.ShortDescription = System.IO.File.ReadAllText(path + "ShortDescription.txt");
                task.FullDescription = System.IO.File.ReadAllText(path + "Description.txt");
                return task;
            }
        }

        //Reading test and expeced result values from file

        private void SetTests(int id, ConfigurationPaths paths)
        {
            var path = paths.FolderPath;

            path += "\\" + id.ToString() + "\\";

            if(!File.Exists(path + "Tests.txt") || !File.Exists(path + "Results.txt"))
            {
                throw new Exception("Test file doesn`t exist");
            }
            else
            {
                TestValues = System.IO.File.ReadAllLines(path + "Tests.txt");
                TestResults = System.IO.File.ReadAllLines(path + "Results.txt");
            }
        }

        public CheckTaskResponse CheckCode(CheckTaskRequest request, ConfigurationPaths paths)
        {
            CheckTaskResponse answer = new CheckTaskResponse() { Id = request.Id, Result = true };
     
            if (request.Code == null)
            {
                answer.Message = "Your code is empty, please, enter code";
                answer.Result = false;
                return answer;
            }
           
            Compiler = new CodeCompiler();

            try
            {
                Compiler.CreateCs(paths.CsFilePath, "test", request.Code);
                ProcessResultModel result = Compiler.CompileProgram(paths.CompilerPath, paths.CsFilePath);

                if (result.ExitCode != 0)
                {
                    answer.Result = false;
                    answer.Message = result.Result.Substring(370, result.Result.Length - 370);
                    return answer;
                }

                SetTests(request.Id, paths);
                answer.Message = "Running tests: \n";

                for (int i = 0; i < TestValues.Length && i < TestResults.Length; i++)
                {
                    var res = RunTest(TestValues[i], TestResults[i], paths.CsFilePath);
                    answer.Message += "Test № " + i.ToString();
                    if (res.ExitCode != 0)
                    {
                        answer.Result = false;
                    }
                    answer.Message += res.Result + "\n";
                }

                Compiler.DeleteFiles(paths.CsFilePath);

            }
            catch (Exception e)
            {
                answer.Result = false;
                answer.Message = e.Message;
                return answer;
            }

            return answer;

        }
    }
}
