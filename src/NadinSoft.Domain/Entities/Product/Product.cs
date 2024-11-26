using FluentResults;
using NadinSoft.Domain.Primitives;

namespace NadinSoft.Domain.Entities.Product
{
    public class Product : BaseEntity
    {
        public Product() { }
        public string Name { get; private set; }
        public DateTime ProductDate { get; private set; }
        public string ManufactureEmail { get; private set; }
        public string? ManufacturePhone { get;  private set; }
        public bool IsAvailable { get; private set; }
        public Guid UserId { get; init; }

        private Product(Guid id, string name, string manufactureEmail, string manufacturePhone, Guid userId) : base(id)
        {
            Name = name;
            ManufactureEmail = manufactureEmail;
            ManufacturePhone = manufacturePhone;
            ProductDate = DateTime.UtcNow;
            IsAvailable = true;
            UserId = userId;
        }

        public static Result<Product> Create(string name, string manufactureEmail, string manufacturePhone, Guid userId)
        {
            var product = new Product(Guid.NewGuid(), name, manufactureEmail, manufacturePhone, userId);
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
