﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Books.Domain.DTO
{
    public class KkbPayment
    {
        public string Email { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
