namespace Coffee;
using System;
using System.ComponentModel.DataAnnotations;

public class Product
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

   [Key]
    public int Id { get; set; }

    [Range(0.01, 10000)]
    public double Price { get; set; }

    [Range(0, 10000)]
    public int Stock { get; set; }

  

    public Product(string name, int id, double price, int stock)
    {
        Name = name;
        Id = id;
        Price = price;
        Stock = stock;
    }
}