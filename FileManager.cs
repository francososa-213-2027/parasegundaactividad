using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Taskflow
{
    public static class FileManager
    {
        private static readonly string FolderPath = "data";
        private static readonly string FilePath = Path.Combine(FolderPath, "tasks.json");

        public static void SaveTasks(List<TaskItem> tasks)
        {
            try
            {
                if (!Directory.Exists(FolderPath)) Directory.CreateDirectory(FolderPath);
                string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar: {ex.Message}");
            }
        }

        public static List<TaskItem> LoadTasks()
        {
            try
            {
                if (!File.Exists(FilePath)) return new List<TaskItem>();
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar: {ex.Message}");
                return new List<TaskItem>();
            }
        }
    }
}
