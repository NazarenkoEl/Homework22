namespace Coffee;

using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.IO; 
using System.Net.Http;
using System.Text; 
using System.Threading.Tasks;
using System.Collections.Generic;


[ApiController]
public class StoreController : ControllerBase
{
    
    private readonly ProductRepository _productRepository;

        public StoreController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        


        [HttpPost]
        [Route("/store/add")]
        public IActionResult Add([FromBody] Product newProduct)
        {
            _productRepository.AddProduct(newProduct);
            return Ok(_productRepository.GetAllProductswhithID());
        }


        [HttpGet]
        [Route("/store/showall")]
        public IActionResult Showall()
        {
            return Ok(_productRepository.GetAllProductswhithID());
        }


        [HttpPost]
        [Route("/store/delete")]
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product != null)
            {
                _productRepository.DeleteProduct(id);
                return Ok($"Продукт с Id {id} удален");
            }
            else
            {
                return NotFound($" Продукт с Id {id} не найден");
            }
        }


    }