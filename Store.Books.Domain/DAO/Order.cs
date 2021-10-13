using Store.Books.Domain.Enums;
using Store.Books.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Books.Domain
{
    public class Order : BaseDateEntity
    {
        [Display(Name = "Пользователь")]
        public string UserName { get; set; }
        [Display(Name = "Статус")]
        public OrderStatusEnum Status { get; set; }
        [Display(Name = "К оплате")]
        public decimal Total { get; set; }
    }
}
