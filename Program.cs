using System;

namespace Taskflow
{
    class Program
    {
        static TaskService service = new TaskService();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("--- TASKFLOW - MENÚ INTERACTIVO ---");
                Console.WriteLine("1. Crear tarea");
                Console.WriteLine("2. Listar tareas");
                Console.WriteLine("3. Actualizar estado");
                Console.WriteLine("4. Cambiar responsable");
                Console.WriteLine("5. Eliminar tarea");
                Console.WriteLine("6. Salir");
                Console.Write("\nSeleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1": UI_Create(); break;
                    case "2": UI_List(); break;
                    case "3": UI_UpdateStatus(); break;
                    case "4": UI_UpdateResponsible(); break;
                    case "5": UI_Delete(); break;
                    case "6": exit = true; break;
                }
            }
        }

        static void UI_Create()
        {
            Console.Write("Título (Obligatorio): ");
            string title = Console.ReadLine();
            if (string.IsNullOrEmpty(title)) return;

            Console.Write("Descripción: ");
            string desc = Console.ReadLine();
            Console.Write("Responsable: ");
            string resp = Console.ReadLine();

            service.CreateTask(title, desc, resp);
            Console.WriteLine("¡Tarea creada!");
            Console.ReadKey();
        }

        static void UI_List()
        {
            Console.WriteLine("\n1. Todas | 2. Pendientes | 3. En Progreso | 4. Completadas");
            string op = Console.ReadLine();

            var tasks = op switch
            {
                "2" => service.ListTasks(TaskStatus.Pendiente),
                "3" => service.ListTasks(TaskStatus.EnProgreso),
                "4" => service.ListTasks(TaskStatus.Completada),
                _ => service.ListTasks()
            };

            Console.WriteLine("\nID | Título | Responsable | Estado | Creado");
            foreach (var t in tasks)
                Console.WriteLine($"{t.Id} | {t.Title} | {t.Responsible} | {t.Status} | {t.CreatedAt}");

            Console.ReadKey();
        }

        static void UI_UpdateStatus()
        {
            Console.Write("ID de tarea: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("0. Pendiente | 1. En Progreso | 2. Completada");
            TaskStatus status = (TaskStatus)int.Parse(Console.ReadLine());

            if (service.UpdateStatus(id, status)) Console.WriteLine("Estado actualizado.");
            else Console.WriteLine("No se encontró la tarea.");
            Console.ReadKey();
        }

        static void UI_UpdateResponsible()
        {
            Console.Write("ID de tarea: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nuevo Responsable: ");
            string resp = Console.ReadLine();

            if (service.UpdateResponsible(id, resp)) Console.WriteLine("Responsable actualizado.");
            else Console.WriteLine("No se encontró la tarea.");
            Console.ReadKey();
        }

        static void UI_Delete()
        {
            Console.Write("ID de tarea a eliminar: ");
            int id = int.Parse(Console.ReadLine());
            if (service.DeleteTask(id)) Console.WriteLine("Tarea eliminada.");
            else Console.WriteLine("No encontrada.");
            Console.ReadKey();
        }
    }
}