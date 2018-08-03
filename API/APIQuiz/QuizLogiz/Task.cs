using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using APIQuiz.Models;

namespace TestRunner
{
    public class Task : ITask
    {
        

        public Task(int id)
        {
            // потрібно зчитати тести та їх результати необхідні для заданого завдання. Обрахувати до якого по айді
            //айді неправильне - викидуємо помилку
            if (id > 20)
            {
                throw new Exception("Not valid task");
            }
        }

        private void SetTools()
        {
            // Зчитування з файлів необхідних параметрів для тестів
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
