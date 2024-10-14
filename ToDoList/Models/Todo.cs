using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва обов'язкова")]
        [StringLength(100, ErrorMessage = "Назва повинна бути не більше ніж 100 символів")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Опис обов'язковий")]
        [StringLength(500, ErrorMessage = "Опис повинен бути не більше ніж 500 символів")]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
