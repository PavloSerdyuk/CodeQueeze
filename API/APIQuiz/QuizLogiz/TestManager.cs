using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;
using APIQuiz.Models;

namespace TestRunner
{
    
    public class TaskManager : IBlInterface
    {
        private QuizTask myTask;

        public string[] TestValues;
        public string[] TestResults;

        public List<string> results = new List<string>();

        private CodeCompiler Compiler;

        public bool setTests()
        {
            // Пошук таски по файлах і встановлення тестів та очікуваних результатів
           
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
        
        public System.Threading.Tasks.Task GetTask(int id)
        {
            throw new NotImplementedException();
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
            const string pathToVsvars32 = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools";
            ProcessResultModel result = Compiler.CompileProgram(pathToVsvars32, Directory.GetCurrentDirectory());
            if (result.ExitCode != 0)
            {
                answer.Result = false;
                answer.Message = " Cannot compile this code: " + result.Result;
                return answer;
            }

            for (int i = 0; i < TestValues.Length; i++)
            {
                var str = RunTest(TestValues[i], TestResults[i]);
                answer.Message += str;
            }

            Compiler.DeleteFiles(programPath);
            return null;
        }
    }
}
