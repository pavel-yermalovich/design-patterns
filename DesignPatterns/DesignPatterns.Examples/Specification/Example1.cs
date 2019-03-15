using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DesignPatterns.Examples.Specification
{
    public class Example1
    {
        public static void Demo()
        {
            var priceSpecification = new PriceRangeSpecification(250, 500);
            var brandsSpecification = new BrandsSpecification(new [] {"Samsung", "Microsoft"} );

            var productRepository = new ProductRepository();
            var products = productRepository.Get(priceSpecification.And(brandsSpecification));

            Console.WriteLine("Products by Samsung and Microsoft with the price between 250 and 500 $:");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }
    }

    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public Specification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }
    }

    public class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;
        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
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

    public class PriceRangeSpecification : Specification<Product>
    {
        private readonly decimal _from;
        private readonly decimal _to;

        public PriceRangeSpecification(decimal from, decimal to)
        {
            _from = from;
            _to = to;
        }

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.Price >= _from && product.Price <= _to;
        }
    }

    public class BrandsSpecification : Specification<Product>
    {
        private readonly string[] _brands;

        public BrandsSpecification(string[] brands)
        {
            _brands = brands;
        }

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => _brands.Any(b => string.Equals(b, product.Brand, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class ProductRepository
    {
        private readonly IEnumerable<Product> _allProducts = new List<Product>
        {
            new Product { Id = 1, Name = "Galaxy S10+", Brand = "Samsung", Price = 899 },
            new Product { Id = 2, Name = "MSDN Subscription", Brand = "Microsoft", Price = 350 },
            new Product { Id = 3, Name = "2019 BMW X7", Brand = "BMW", Price = 73900 }
        };

        public IEnumerable<Product> Get(Specification<Product> specification)
        {
            return _allProducts.Where(specification.IsSatisfiedBy).ToList();
        }
    }
}
