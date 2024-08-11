﻿using FluentResults;
using NadinSoft.Domain.Primitives;

namespace NadinSoft.Domain.Entities.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public DateTime ProductDate { get; private set; }
        public string ManufactureEmail { get; private set; }
        public string ManufacturePhone { get;  private set; }
        public bool IsAvailable { get; private set; }

        private Product(Guid id, string name, string manufactureEmail, string manufacturePhone) : base(id)
        {
            Name = name;
            ManufactureEmail = manufactureEmail;
            ManufacturePhone = manufacturePhone;
            ProductDate = DateTime.UtcNow;
            IsAvailable = true;
        }

        public static Result<Product> Create(string name, string manufactureEmail, string manufacturePhone)
        {
            var product = new Product(Guid.NewGuid(), name, manufactureEmail, manufacturePhone);
            return Result.Ok(product);
        }

        public Result<Product> Update(string name, string manufactureEmail, string manufacturePhone)
        {
            Name = name;
            ManufactureEmail = manufactureEmail;
            ManufacturePhone = manufacturePhone;
            return Result.Ok(this);
        }

        public Result<Product> SoftDelete()
        {
            IsAvailable = false;
            return Result.Ok(this);
        }
    }
}
