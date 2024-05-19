using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApplication.Domain.Entities
{
    public class Order : Entity
    {
        [Required(ErrorMessage = "Поле 'ФИО' обязательно для заполнения")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Поле 'Адрес' обязательно для заполнения")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат адреса электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Контактный номер' обязательно для заполнения")]
        [Phone(ErrorMessage = "Некорректный формат номера телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Поле 'План питания' обязательно для заполнения")]
        public string MealPlan { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }

        // Конструктор по умолчанию для Entity Framework Core
        public Order()
        {
            CreationDate = DateTime.Now; // Установка текущей даты при создании заявки
        }

        public Order(string fullName, string address, string email, string phoneNumber, string mealPlan)
        {
            FullName = fullName;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
            MealPlan = mealPlan;
            Status = OrderStatus.Pending; // По умолчанию статус заявки "в ожидании"
            CreationDate = DateTime.Now; // Установка текущей даты при создании заявки
        }
    }

    public enum OrderStatus
    {
        Pending, // В ожидании
        InProgress, // В процессе
        Completed, // Выполнено
        Rejected // Отклонено
    }
}
