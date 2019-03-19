using DesignPatterns.Examples.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Examples.Specification
{
    public class Example1
    {
        public static void Demo()
        {
            var priceSpecification = new ExpressionSpecification<Product>(p => p.Price >= 250 && p.Price <= 100000);
            var brandsSpecification = new ExpressionSpecification<Product>(p=> 
                p.Brand.EqualsIC("Samsung") || p.Brand.EqualsIC("Microsoft"));
            var bmwSpecification = new ExpressionSpecification<Product>(p => p.Brand == "BMW");

            var productRepository = new ProductRepository();
            var products = productRepository.Get(priceSpecification.And(brandsSpecification).Not(bmwSpecification));

            Console.WriteLine("Products by Samsung and Microsoft except BMW with the price between 250 and 100000 $:");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
        ISpecification<T> And(ISpecification<T> specification);
        ISpecification<T> Or(ISpecification<T> specification);
        ISpecification<T> Not(ISpecification<T> specification);
    }

    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);

        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }
        public ISpecification<T> Or(ISpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }
        public ISpecification<T> Not(ISpecification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }
    }

    public class AndSpecification<T> : CompositeSpecification<T>
    {
        readonly ISpecification<T> _leftSpecification;
        readonly ISpecification<T> _rightSpecification;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _leftSpecification = left;
            _rightSpecification = right;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return _leftSpecification.IsSatisfiedBy(entity) && _rightSpecification.IsSatisfiedBy(entity);
        }
    }

    public class OrSpecification<T> : CompositeSpecification<T>
    {
        readonly ISpecification<T> _leftSpecification;
        readonly ISpecification<T> _rightSpecification;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _leftSpecification = left;
            _rightSpecification = right;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return _leftSpecification.IsSatisfiedBy(entity)
                   || _rightSpecification.IsSatisfiedBy(entity);
        }
    }

    public class NotSpecification<T> : CompositeSpecification<T>
    {
        readonly ISpecification<T> _specification;

        public NotSpecification(ISpecification<T> spec)
        {
            _specification = spec;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return !_specification.IsSatisfiedBy(entity);
        }
    }

    public class ExpressionSpecification<T> : CompositeSpecification<T>
    {
        private readonly Func<T, bool> _expression;

        public ExpressionSpecification(Func<T, bool> expression)
        {
            _expression = expression;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return _expression(entity);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Id = {Id}, Name = {Name}, Brand = {Brand}, Price = {Price:C}";
        }
    }

    public class ProductRepository
    {
        private readonly IEnumerable<Product> _allProducts = new List<Product>
        {
            new Product { Id = 1, Name = "Galaxy S7", Brand = "Samsung", Price = 499 },
            new Product { Id = 1, Name = "Galaxy S8", Brand = "Samsung", Price = 599 },
            new Product { Id = 1, Name = "Galaxy S9", Brand = "Samsung", Price = 799 },
            new Product { Id = 1, Name = "Galaxy S10+", Brand = "Samsung", Price = 899 },
            new Product { Id = 2, Name = "MSDN Subscription", Brand = "Microsoft", Price = 350 },
            new Product { Id = 2, Name = "Office 365 Subscription", Brand = "Microsoft", Price = 150 },
            new Product { Id = 2, Name = "Visual Studio 2017", Brand = "Microsoft", Price = 250 },
            new Product { Id = 2, Name = "MS SQL Server 2017", Brand = "Microsoft", Price = 200 },
            new Product { Id = 3, Name = "2019 BMW X7", Brand = "BMW", Price = 73900 }
        };

        public IEnumerable<Product> Get(ISpecification<Product> specification)
        {
            return _allProducts.Where(specification.IsSatisfiedBy);
        }
    }
}
