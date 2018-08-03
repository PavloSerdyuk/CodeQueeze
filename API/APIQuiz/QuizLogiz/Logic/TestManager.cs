using System;
using System.Collections.Generic;
using System.IO;
using TestRunner.Models;
//using System.Threading.Tasks;

namespace TestRunner.Logic
{
    
    public class TaskManager : IBlInterface
    {
        private IQuizTask myTask;

        public string[] TestValues;
        public string[] TestResults;

        public List<string> results = new List<string>();

        private CodeCompiler Compiler;

        public bool setTests(int id)
        {
            // Пошук таски по файлах і встановлення тестів та очікуваних результатів
           
            return false;
        }

        

        public ProcessResultModel RunTest(string testValues, string expectation)
        {
            
            // Потрібно розібратись як закидувати в компілятор код 
            // Та дивитись чи ці тести виконались правильно                      
            var runExe = Compiler.RunExe(testValues);
            ProcessResultModel result = new ProcessResultModel();
            if (runExe.Result == expectation)
            {
                result.ExitCode = 0;
            }
            else
            {
                result.ExitCode = 1;
            }

            result.Result = runExe.Result;
            return result;
        }
        
        public IQuizTask GetTask(int id)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["FoldersPath"];
            path += "/" + id.ToString() +"/";
            IQuizTask task = new QuizTask();
            task.Id = id;
            task.Name = System.IO.File.ReadAllText(path + "Name.txt");
            task.Description = System.IO.File.ReadAllText(path + "Description.txt");
            return task;
        }

        private void SetTests(int id)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["FoldersPath"];
            path += "/" + id.ToString() + "/";
            TestValues = System.IO.File.ReadAllLines(path + "Tests.txt");
            TestResults = System.IO.File.ReadAllLines(path + "Results.txt");
        }
        public CheckTaskResponse CheckCode(CheckTaskRequest request)
        {
            myTask = new QuizTask(){Id = request.Id};
            var programPath = Directory.GetCurrentDirectory();
            CheckTaskResponse answer = new CheckTaskResponse();
            answer.Id = request.Id;
            const string programName = "test";
            answer.Result = true;
            Compiler = new CodeCompiler();
            Compiler.CreateCs(programPath, programName, request.Code);
            string pathToVsvars32 = System.Configuration.ConfigurationManager.AppSettings["CompilerPath"];
            ProcessResultModel result = Compiler.CompileProgram(pathToVsvars32, Directory.GetCurrentDirectory());
            if (result.ExitCode != 0)
            {
                answer.Result = false;
                answer.Message = " Cannot compile this code: " + result.Result;
                return answer;
            }
            SetTests(request.Id);
            answer.Result = true;
            for (int i = 0; i < TestValues.Length; i++)
            {
                var res = RunTest(TestValues[i], TestResults[i]);
                if (res.ExitCode != 0)
                {
                    answer.Result = false;
                }
                answer.Message += res.Result;
            }

            Compiler.DeleteFiles(programPath);
            return null;
        }
    }
}
