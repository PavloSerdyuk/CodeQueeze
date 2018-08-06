using System.Diagnostics;
using System.IO;
using System.Text;

namespace TestRunner.Logic
{
    public class ProcessResultModel
    {
        // Структура для повернення результатів з консолі
        public int ExitCode; // якщо 0 то все гаразд,  якщо інакше значення то ні
        public string Result; // текст виводу програми, чи помилки
    }

    public class CodeCompiler
    {
        private string ProgramName;
        
        internal void CreateCs(string programPath, string programName, string programCode)
        {
            // Створюєм .cs
            ProgramName = programName;
            using (var fs = File.Create(programPath + "/CS/" + programName + ".cs"))
            {
                var info = new UTF8Encoding(true).GetBytes(programCode);
                fs.Write(info, 0, info.Length);
            }
        }

        private static ProcessResultModel GetProcessResult(string arguments)
        {
            // Метод повертає результат з консолі
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
            return new ProcessResultModel
            {
                ExitCode = cmd.ExitCode,
                Result = cmd.StandardOutput.ReadToEnd()
            };
        }

        internal ProcessResultModel CompileProgram(string pathToVsvars32, string programPath)
        {
            // Для того, щоб працювало у всіх папках треба слеш д
            // Без нього тільки в папці Project прожка працює
            var arguments = string.Format(
                "cd /d {0} &" +
                "VsDevCmd.bat" + "&" +
                "cd /d {1} &" +
                "csc  /t:exe {2}.cs",
                pathToVsvars32, programPath, ProgramName);
            return GetProcessResult(arguments);
        }

        internal ProcessResultModel RunExe( string argStrings)
        {
            // Результат виконання прожки
            var arguments = string.Format("{0}.exe", ProgramName) + " " + argStrings;
            return GetProcessResult(arguments);
        }

        internal void DeleteFiles(string programPathCs)
        {
            // Чистим не потрібні файли
            if (File.Exists(programPathCs + "/" + ProgramName + ".cs"))
            {
                File.Delete(programPathCs + "/" + ProgramName + ".cs");
            }
            if (File.Exists(Directory.GetCurrentDirectory() + "/" + ProgramName + ".exe"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "/" + ProgramName + ".exe");
            }
        }
    }
}
